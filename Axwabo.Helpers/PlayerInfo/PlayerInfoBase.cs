using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Axwabo.Helpers.PlayerInfo.Containers;
using Axwabo.Helpers.PlayerInfo.Effect;
using Axwabo.Helpers.PlayerInfo.Vanilla;
using Exiled.API.Features;
using PlayerRoles.FirstPersonControl;
using PlayerStatsSystem;
using UnityEngine;

namespace Axwabo.Helpers.PlayerInfo;

/// <summary>
/// A base class for storing gameplay information about a player.
/// </summary>
/// <seealso cref="StandardPlayerInfo"/>
public abstract class PlayerInfoBase
{

    #region Obtainers

    private static readonly List<PlayerInfoObtainer> CustomObtainers = new();

    private static readonly PlayerInfoObtainer[] DefaultObtainers =
    {
        new(Scp049Info.Is049, Scp049Info.Get),
        new(Scp079Info.Is079, Scp079Info.Get),
        new(Scp096Info.Is096, Scp096Info.Get),
        new(Scp106Info.Is106, Scp106Info.Get),
        new(Scp173Info.Is173, Scp173Info.Get),
        new(Scp939Info.Is939, Scp939Info.Get),
        new(Scp3114Info.Is3114, Scp3114Info.Get)
    };

    /// <summary>
    /// Registers a custom player info obtainer.
    /// </summary>
    /// <param name="check">The check to determine if the player matches the given role.</param>
    /// <param name="getter">A method to get the player info.</param>
    /// <param name="roleSetter">A method to set the player's role.</param>
    /// <returns>The id of the registered obtainer.</returns>
    public static uint RegisterCustomObtainer(PlayerCheck check, PlayerInfoGetter getter, PlayerRoleSetter roleSetter = null)
    {
        var obtainer = new PlayerInfoObtainer(check, getter, roleSetter);
        CustomObtainers.Add(obtainer);
        return obtainer.Id;
    }

    /// <summary>
    /// Unregisters a custom player info obtainer.
    /// </summary>
    /// <param name="id">The id of the obtainer to unregister.</param>
    /// <returns>Whether the obtainer was unregistered.</returns>
    public static bool UnregisterCustomObtainer(uint id) => CustomObtainers.RemoveAll(obtainer => obtainer.Id == id) > 0;

    /// <summary>
    /// Gets the first matching info obtainer for the given <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to get the obtainer from.</param>
    /// <returns>The first matching obtainer, or <see cref="PlayerInfoObtainer.Empty"/> if none were found.</returns>
    public static PlayerInfoObtainer GetFirstMatchingObtainer(Player player)
    {
        foreach (var obtainer in CustomObtainers)
            if (obtainer.IsValid && obtainer.Check(player))
                return obtainer;

        foreach (var obtainer in DefaultObtainers)
            if (obtainer.IsValid && obtainer.Check(player))
                return obtainer;

        return PlayerInfoObtainer.Empty;
    }

    /// <summary>
    /// Creates a <see cref="PlayerInfoBase"/> from a player based on the registered obtainers.
    /// </summary>
    /// <param name="player">The player to obtain the info from.</param>
    /// <returns>The player info. If no special obtainers were found, it will be a <see cref="StandardPlayerInfo"/>.</returns>
    /// <seealso cref="StandardPlayerInfo.Get"/>
    /// <seealso cref="RegisterCustomObtainer"/>
    /// <seealso cref="UnregisterCustomObtainer"/>
    /// <seealso cref="GetFirstMatchingObtainer"/>
    public static PlayerInfoBase CreateAutomatically(Player player)
    {
        var obtainer = GetFirstMatchingObtainer(player);
        return obtainer.IsValid ? obtainer.Get(player) : StandardPlayerInfo.Get(player);
    }

    #endregion

    #region Properties

    /// <summary>The position of the player.</summary>
    public Vector3 Position { get; set; }

    /// <summary>The rotation of the player.</summary>
    public Vector3 Rotation { get; set; }

    /// <summary>The base HP of the player.</summary>
    public float Health { get; set; }

    /// <summary>The additional max HP of the player (applies to humans only).</summary>
    [Obsolete("No longer part of the game.")]
    public float AdditionalMaxHealth { get; set; }

    /// <summary>The additional HP of the player.</summary>
    public float Ahp { get; set; }

    /// <summary>The stamina of the player.</summary>
    public float Stamina { get; set; }

    /// <summary>The Hume Shield of the player.</summary>
    public float HumeShield { get; set; }

    /// <summary>The effects of the player.</summary>
    /// <seealso cref="EffectInfoBase"/>
    public ReadOnlyCollection<EffectInfoBase> Effects { get; set; }

    /// <summary>Information about the player's inventory.</summary>
    public InventoryInfo Inventory { get; set; }

    #endregion

    /// <summary>
    /// Creates a base object for storing information about a player.
    /// </summary>
    /// <param name="roleInfo">Basic information about the player.</param>
    /// <seealso cref="EffectInfoBase"/>
    /// <seealso cref="StandardPlayerInfo"/>
    protected PlayerInfoBase(BasicRoleInfo roleInfo)
    {
        if (!roleInfo.IsValid)
            return;
        Position = roleInfo.Position;
        Rotation = roleInfo.Rotation;
        Health = roleInfo.Health;
        Ahp = roleInfo.Ahp;
        Stamina = roleInfo.Stamina;
        HumeShield = roleInfo.HumeShield;
        Effects = roleInfo.Effects?.AsReadOnly();
        Inventory = roleInfo.Inventory;
    }

    /// <summary>
    /// Applies the gameplay data to the given <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to apply the data to.</param>
    public virtual void ApplyTo(Player player)
    {
        if (!player.IsConnected)
            return;
        player.ReferenceHub.TryOverridePosition(Position, Rotation - player.Rotation.eulerAngles);
        player.Health = Health;
        var stats = player.ReferenceHub.playerStats;
        if (Ahp >= 0)
            stats.GetModule<AhpStat>().CurValue = Ahp;
        stats.GetModule<StaminaStat>().CurValue = Stamina;
        if (HumeShield >= 0)
            stats.GetModule<HumeShieldStat>().CurValue = HumeShield;
        foreach (var effect in Effects ?? Enumerable.Empty<EffectInfoBase>())
            effect?.ApplyTo(player);
        Inventory.ApplyTo(player);
    }

}
