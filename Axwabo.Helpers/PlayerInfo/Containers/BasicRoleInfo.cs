using Axwabo.Helpers.PlayerInfo.Effect;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerStatsSystem;
using RelativePositioning;

namespace Axwabo.Helpers.PlayerInfo.Containers;

/// <summary>
/// Contains common information about a player.
/// </summary>
public readonly struct BasicRoleInfo
{

    #region Common Static

    /// <summary>The index of the <see cref="AhpStat"/> in <see cref="PlayerStats.StatModules"/>.</summary>
    public const int AhpIndex = 1;

    /// <summary>The index of the <see cref="StaminaStat"/> in <see cref="PlayerStats.StatModules"/>.</summary>
    public const int StaminaIndex = 2;

    /// <summary>The index of the <see cref="HumeShieldStat"/> in <see cref="PlayerStats.StatModules"/>.</summary>
    public const int HumeShieldIndex = 4;

    /// <summary>
    /// Gets the Hume Shield value of the player, or -1 if the player is not currently playing as an <see cref="IHumeShieldedRole"/>.
    /// </summary>
    /// <param name="player">The player to get the HS value of.</param>
    /// <returns>The HS value, or -1 if there's no Hume Shield.</returns>
    public static float GetHs(Player player) => player.Role() is not IHumeShieldedRole hs ? -1 : hs.HumeShieldModule.HsCurrent;

    /// <summary>
    /// Gets the AHP value of the player, or -1 if there are no currently active AHP processes.
    /// </summary>
    /// <param name="player">The player to get the AHP value from.</param>
    /// <returns>The AHP value, or -1 if there are no active processes.</returns>
    public static float GetAhp(Player player)
    {
        var ahp = player.ReferenceHub.playerStats.GetModule<AhpStat>();
        return ahp._activeProcesses.Count is 0 ? -1 : ahp.CurValue;
    }

    /// <summary>
    /// Gets the stamina amount of the player.
    /// </summary>
    /// <param name="player">The player to get the stamina of.</param>
    /// <returns>The stamina amount.</returns>
    public static float GetStamina(Player player) => player.ReferenceHub.playerStats.GetModule<StaminaStat>().CurValue;

    /// <summary>
    /// Validates the given position to ensure that it's not inside an elevator.
    /// </summary>
    /// <param name="position">The position to validate.</param>
    /// <returns>The validated position.</returns>
    public static Vector3 ValidatePosition(Vector3 position)
        => WaypointBase.TryGetWaypoint(new RelativePosition(position).WaypointId, out var waypoint) && waypoint is ElevatorWaypoint ewp
            ? ewp._elevator.DestinationDoor.transform.TransformPoint(Vector3.forward * 0.5f) + Vector3.up
            : position;

    #endregion

    /// <summary>
    /// Gets the basic information about the given <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to get the information from.</param>
    /// <returns>A <see cref="BasicRoleInfo"/> instance.</returns>
    public static BasicRoleInfo Get(Player player) => new(
        ValidatePosition(player.Position),
        player.Camera.eulerAngles,
        player.GetStatModule<HealthStat>().CurValue,
        GetAhp(player),
        GetStamina(player),
        GetHs(player),
        EffectInfoBase.EffectsToList(player.ReferenceHub.playerEffectsController.AllEffects),
        InventoryInfo.Get(player)
    );

    /// <summary>
    /// Creates a new <see cref="BasicRoleInfo"/> instance.
    /// <b>Obsolete: Use the constructor without additionalMaxHealth instead.</b>
    /// </summary>
    /// <param name="position">The position of the player.</param>
    /// <param name="rotation">The rotation of the player.</param>
    /// <param name="health">The base HP of the player.</param>
    /// <param name="additionalMaxHealth">The additional max HP of the player (applies to humans only).</param>
    /// <param name="ahp">The additional HP of the player.</param>
    /// <param name="stamina">The stamina of the player.</param>
    /// <param name="humeShield">The Hume Shield of the player.</param>
    /// <param name="effects">The effects on the player.</param>
    /// <param name="inventoryInfo">Information about the player's inventory.</param>
    [Obsolete("Use the constructor with additionalMaxHealth instead.")]
    public BasicRoleInfo(Vector3 position, Vector3 rotation, float health, float additionalMaxHealth, float ahp, float stamina, float humeShield, List<EffectInfoBase> effects, InventoryInfo inventoryInfo)
        : this(position, rotation, health, ahp, stamina, humeShield, effects, inventoryInfo)
    {
    }

    /// <summary>
    /// Creates a new <see cref="BasicRoleInfo"/> instance.<br/>
    /// </summary>
    /// <param name="position">The position of the player.</param>
    /// <param name="rotation">The rotation of the player.</param>
    /// <param name="health">The base HP of the player.</param>
    /// <param name="ahp">The additional HP of the player.</param>
    /// <param name="stamina">The stamina of the player.</param>
    /// <param name="humeShield">The Hume Shield of the player.</param>
    /// <param name="effects">The effects on the player.</param>
    /// <param name="inventoryInfo">Information about the player's inventory.</param>
    public BasicRoleInfo(Vector3 position, Vector3 rotation, float health, float ahp, float stamina, float humeShield, List<EffectInfoBase> effects, InventoryInfo inventoryInfo)
    {
        Position = position;
        Rotation = rotation;
        Health = health;
        Ahp = ahp;
        Stamina = stamina;
        HumeShield = humeShield;
        Effects = effects;
        Inventory = inventoryInfo;
        IsValid = true;
        AdditionalMaxHealth = 0;
    }

    #region Members

    /// <summary>The position of the player.</summary>
    public readonly Vector3 Position;

    /// <summary>The rotation of the player.</summary>
    public readonly Vector3 Rotation;

    /// <summary>The base HP of the player.</summary>
    public readonly float Health;

    /// <summary>The additional max HP of the player (applies to humans only).</summary>
    [Obsolete("No longer part of the game.")]
    public readonly float AdditionalMaxHealth;

    /// <summary>The additional HP of the player.</summary>
    public readonly float Ahp;

    /// <summary>The stamina of the player.</summary>
    public readonly float Stamina;

    /// <summary>The Hume Shield of the player.</summary>
    public readonly float HumeShield;

    /// <summary>The effects of the player.</summary>
    /// <seealso cref="EffectInfoBase"/>
    public readonly List<EffectInfoBase> Effects;

    /// <summary>Information about the player's inventory.</summary>
    public readonly InventoryInfo Inventory;

    /// <summary>True if this instance is valid (not empty).</summary>
    public readonly bool IsValid;

    #endregion

}
