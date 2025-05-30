﻿using Axwabo.Helpers.PlayerInfo.Containers;
using PlayerRoles.PlayableScps.Scp049;

namespace Axwabo.Helpers.PlayerInfo.Vanilla;

/// <summary>
/// Contains information about SCP-049.
/// </summary>
/// <seealso cref="StandardPlayerInfo"/>
/// <seealso cref="PlayerInfoBase"/>
/// <seealso cref="Scp049Role"/>
public class Scp049Info : PlayerInfoBase
{

    /// <summary>
    /// Creates an <see cref="Scp049Info"/> instance using the given <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to get the information from.</param>
    /// <returns>The information about SCP-049.</returns>
    public static Scp049Info Get(Player player)
    {
        var routines = Scp049SubroutineContainer.Get(player.RoleAs<Scp049Role>());
        if (!routines.IsValid)
            return null;
        var call = routines.CallAbility;
        var sense = routines.SenseAbility;
        return new Scp049Info(
            call.Cooldown,
            call.Duration,
            call._serverTriggered,
            sense.Cooldown,
            sense.Duration,
            sense.Target,
            sense.DeadTargets.ToArray(),
            routines.AttackAbility.Cooldown,
            BasicRoleInfo.Get(player)
        );
    }

    /// <summary>
    /// Checks if the given player is SCP-049.
    /// </summary>
    /// <param name="p">The player to check.</param>
    /// <returns>Whether the given player is SCP-049.</returns>
    public static bool Is049(Player p) => p.RoleIs<Scp049Role>();

    /// <summary>
    /// Creates a new <see cref="Scp049Info"/> instance.
    /// </summary>
    /// <param name="callCooldown">The cooldown of "The Doctor's Call" ability.</param>
    /// <param name="callDuration">The remaining time of "The Doctor's Call" ability.</param>
    /// <param name="callActive">Whether "The Doctor's Call" ability is currently active.</param>
    /// <param name="senseCooldown">The cooldown of the "Good Sense of the Doctor" ability.</param>
    /// <param name="senseDuration">The remaining time of the "Good Sense of the Doctor" ability.</param>
    /// <param name="target">The current target of the "Good Sense of the Doctor" ability.</param>
    /// <param name="deadTargets">The targets killed with the "Good Sense of the Doctor" ability.</param>
    /// <param name="attackCooldown">The primary attack cooldown.</param>
    /// <param name="basicRoleInfo">Basic information about the player.</param>
    public Scp049Info(
        CooldownInfo callCooldown,
        CooldownInfo callDuration,
        bool callActive,
        CooldownInfo senseCooldown,
        CooldownInfo senseDuration,
        ReferenceHub target,
        ReferenceHub[] deadTargets,
        CooldownInfo attackCooldown,
        BasicRoleInfo basicRoleInfo
    ) : base(basicRoleInfo)
    {
        CallCooldown = callCooldown;
        CallDuration = callDuration;
        CallActive = callActive;
        SenseCooldown = senseCooldown;
        SenseDuration = senseDuration;
        Target = target;
        DeadTargets = deadTargets;
        AttackCooldown = attackCooldown;
    }

    #region Properties

    /// <summary>The cooldown of "The Doctor's Call" ability.</summary>
    public CooldownInfo CallCooldown { get; set; }

    /// <summary>The remaining time of "The Doctor's Call" ability.</summary>
    public CooldownInfo CallDuration { get; set; }

    /// <summary>Whether "The Doctor's Call" ability is currently active.</summary>
    public bool CallActive { get; set; }

    /// <summary>The cooldown of the "Good Sense of the Doctor" ability.</summary>
    public CooldownInfo SenseCooldown { get; set; }

    /// <summary>The remaining time of the "Good Sense of the Doctor" ability.</summary>
    public CooldownInfo SenseDuration { get; set; }

    /// <summary>The current target of the "Good Sense of the Doctor" ability.</summary>
    public ReferenceHub Target { get; set; }

    /// <summary>The targets killed with the "Good Sense of the Doctor" ability.</summary>
    public ReferenceHub[] DeadTargets { get; set; }

    /// <summary>The primary attack cooldown.</summary>
    public CooldownInfo AttackCooldown { get; set; }

    #endregion

    /// <inheritdoc />
    public override void ApplyTo(Player player)
    {
        if (!player.IsConnected())
            return;
        base.ApplyTo(player);
        var routines = Scp049SubroutineContainer.Get(player.RoleAs<Scp049Role>());
        if (!routines.IsValid)
            return;
        var call = routines.CallAbility;
        CallCooldown.ApplyTo(call.Cooldown);
        CallDuration.ApplyTo(call.Duration);
        call._serverTriggered = CallActive;

        var sense = routines.SenseAbility;
        SenseCooldown.ApplyTo(sense.Cooldown);
        SenseDuration.ApplyTo(sense.Duration);

        var hasTarget = Target != null;
        sense.HasTarget = hasTarget;
        if (hasTarget)
            sense.Target = Target;

        if (DeadTargets != null)
        {
            sense.DeadTargets.Clear();
            sense.DeadTargets.AddRange(DeadTargets);
        }

        var attack = routines.AttackAbility;
        AttackCooldown.ApplyTo(attack.Cooldown);

        call.Sync();
        sense.Sync();
        attack.Sync();
    }

}
