using Axwabo.Helpers.PlayerInfo.Containers;
using Exiled.API.Features;
using Mirror;
using PlayerRoles.PlayableScps.Scp173;

namespace Axwabo.Helpers.PlayerInfo.Vanilla;

/// <summary>
/// Contains information about SCP-173.
/// </summary>
/// <seealso cref="StandardPlayerInfo"/>
/// <seealso cref="PlayerInfoBase"/>
/// <seealso cref="Scp173Role"/>
public class Scp173Info : PlayerInfoBase
{

    /// <summary>
    /// Creates an <see cref="Scp173Info"/> instance using the given <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to get the information from.</param>
    /// <returns>The information about SCP-173.</returns>
    public static Scp173Info Get(Player player)
    {
        var routines = Scp173SubroutineContainer.Get(player.RoleAs<Scp173Role>());
        if (!routines.IsValid)
            return null;
        var time = NetworkTime.time;
        var blink = routines.BlinkTimer;
        var breakneck = routines.BreakneckSpeeds;
        return new Scp173Info(
            blink._totalCooldown,
            blink._initialStopTime - time,
            blink._endSustainTime - time,
            breakneck.IsActive,
            breakneck._disableTime - breakneck.Elapsed,
            breakneck.Cooldown,
            routines.Tantrum.Cooldown,
            BasicRoleInfo.Get(player)
        );
    }

    /// <summary>
    /// Checks if the given player is SCP-173.
    /// </summary>
    /// <param name="p">The player to check.</param>
    /// <returns>Whether the given player is SCP-173.</returns>
    public static bool Is173(Player p) => p.RoleIs<Scp173Role>();

    /// <summary>
    /// Creates a new <see cref="Scp173Info"/> instance.
    /// </summary>
    /// <param name="totalBlinkCooldown">The total cooldown of the Blink ability.</param>
    /// <param name="blinkInitialStop">The time when players stopped looking at SCP-173.</param>
    /// <param name="blinkEndSustainTime">The end time of the Blink ability.</param>
    /// <param name="breakneckSpeedsActive">Whether Breakneck Speeds is active.</param>
    /// <param name="breakneckSpeedsRemainingTime">The remaining time of Breakneck Speeds.</param>
    /// <param name="breakneckSpeedsCooldown">The Breakneck Speeds ability cooldown.</param>
    /// <param name="tantrumCooldown">The Tantrum ability cooldown.</param>
    /// <param name="basicRoleInfo">Basic information about the player.</param>
    public Scp173Info(
        float totalBlinkCooldown,
        double blinkInitialStop,
        double blinkEndSustainTime,
        bool breakneckSpeedsActive,
        float breakneckSpeedsRemainingTime,
        CooldownInfo breakneckSpeedsCooldown,
        CooldownInfo tantrumCooldown,
        BasicRoleInfo basicRoleInfo
    ) : base(basicRoleInfo)
    {
        TotalBlinkCooldown = totalBlinkCooldown;
        BlinkInitialStop = blinkInitialStop;
        BlinkEndSustainTime = blinkEndSustainTime;
        BreakneckSpeedsActive = breakneckSpeedsActive;
        BreakneckSpeedsRemainingTime = breakneckSpeedsRemainingTime;
        BreakneckSpeedsCooldown = breakneckSpeedsCooldown;
        TantrumCooldown = tantrumCooldown;
    }

    #region Properties

    /// <summary>The total cooldown of the Blink ability.</summary>
    public float TotalBlinkCooldown { get; set; }

    /// <summary>The time when players stopped looking at SCP-173.</summary>
    public double BlinkInitialStop { get; set; }

    /// <summary>The end time of the Blink ability.</summary>
    public double BlinkEndSustainTime { get; set; }

    /// <summary>Whether Breakneck Speeds is active.</summary>
    public bool BreakneckSpeedsActive { get; set; }

    /// <summary>The remaining time of Breakneck Speeds.</summary>
    public float BreakneckSpeedsRemainingTime { get; set; }

    /// <summary>The Breakneck Speeds ability cooldown.</summary>
    public CooldownInfo BreakneckSpeedsCooldown { get; set; }

    /// <summary>The Tantrum ability cooldown.</summary>
    public CooldownInfo TantrumCooldown { get; set; }

    #endregion

    /// <inheritdoc />
    public override void ApplyTo(Player player)
    {
        if (!player.IsConnected)
            return;
        base.ApplyTo(player);
        var routines = Scp173SubroutineContainer.Get(player.RoleAs<Scp173Role>());
        if (!routines.IsValid)
            return;

        var time = NetworkTime.time;

        var blink = routines.BlinkTimer;
        blink._totalCooldown = TotalBlinkCooldown;
        blink._initialStopTime = BlinkInitialStop + time;
        blink._endSustainTime = BlinkEndSustainTime + time;

        var breakneck = routines.BreakneckSpeeds;
        breakneck.IsActive = BreakneckSpeedsActive;
        if (BreakneckSpeedsActive && BreakneckSpeedsRemainingTime > 0)
        {
            breakneck._duration.Restart();
            breakneck._disableTime = BreakneckSpeedsRemainingTime;
        }

        BreakneckSpeedsCooldown.ApplyTo(breakneck.Cooldown);

        var tantrum = routines.Tantrum;
        TantrumCooldown.ApplyTo(tantrum.Cooldown);

        blink.Sync();
        breakneck.Sync();
        tantrum.Sync();
    }

}
