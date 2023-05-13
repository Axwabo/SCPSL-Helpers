using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using PlayerRoles.PlayableScps.Scp079.Rewards;

namespace Axwabo.Helpers.PlayerInfo.Containers;

/// <summary>
/// Contains all main subroutines of the <see cref="Scp079Role"/>.
/// </summary>
public readonly struct Scp079SubroutineContainer
{

    /// <summary>An empty instance representing an invalid object.</summary>
    public static readonly Scp079SubroutineContainer Empty = new();

    /// <summary>SCP-079's tier manager.</summary>
    public readonly Scp079TierManager TierManager;

    /// <summary>SCP-079's Zone-Wide Blackout ability.</summary>
    public readonly Scp079BlackoutZoneAbility ZoneBlackout;

    /// <summary>SCP-079's auxiliary power manager.</summary>
    public readonly Scp079AuxManager AuxManager;

    /// <summary>SCP-079's current camera synchronizer.</summary>
    public readonly Scp079CurrentCameraSync CurrentCameraSync;

    /// <summary>SCP-079's "Signal Lost" handler.</summary>
    public readonly Scp079LostSignalHandler LostSignalHandler;

    /// <summary>SCP-079's EXP reward manager.</summary>
    public readonly Scp079RewardManager RewardManager;

    /// <summary>SCP-079's Tesla Gate overcharge ability.</summary>
    public readonly Scp079TeslaAbility TeslaAbility;

    /// <summary>SCP-079's Door Lock ability.</summary>
    public readonly Scp079DoorLockChanger DoorLock;

    /// <summary>SCP-079's Lockdown ability.</summary>
    public readonly Scp079LockdownRoomAbility LockdownAbility;

    /// <summary>Returns true if this instance is valid (not empty).</summary>
    public readonly bool IsValid;

    /// <summary>
    /// Creates a new <see cref="Scp079SubroutineContainer"/> instance.
    /// </summary>
    /// <param name="tierManager">SCP-079's tier manager.</param>
    /// <param name="zoneBlackout">SCP-079's Zone-Wide Blackout ability.</param>
    /// <param name="currentCameraSync">SCP-079's current camera synchronizer.</param>
    /// <param name="auxManager">SCP-079's auxiliary power manager.</param>
    /// <param name="lostSignalHandler">SCP-079's "Signal Lost" handler.</param>
    /// <param name="rewardManager">SCP-079's EXP reward manager.</param>
    /// <param name="teslaAbility">SCP-079's Tesla Gate overcharge ability.</param>
    /// <param name="doorLock">SCP-079's Door Lock ability.</param>
    /// <param name="lockdownAbility">SCP-079's Lockdown ability.</param>
    public Scp079SubroutineContainer(
        Scp079TierManager tierManager,
        Scp079BlackoutZoneAbility zoneBlackout,
        Scp079CurrentCameraSync currentCameraSync,
        Scp079AuxManager auxManager,
        Scp079LostSignalHandler lostSignalHandler,
        Scp079RewardManager rewardManager,
        Scp079TeslaAbility teslaAbility,
        Scp079DoorLockChanger doorLock,
        Scp079LockdownRoomAbility lockdownAbility
    )
    {
        TierManager = tierManager;
        ZoneBlackout = zoneBlackout;
        CurrentCameraSync = currentCameraSync;
        AuxManager = auxManager;
        LostSignalHandler = lostSignalHandler;
        RewardManager = rewardManager;
        TeslaAbility = teslaAbility;
        DoorLock = doorLock;
        LockdownAbility = lockdownAbility;
        IsValid = true;
    }

    /// <summary>
    /// Gets all main subroutines of SCP-079.
    /// </summary>
    /// <param name="role">The role to get the main subroutines from.</param>
    /// <returns>An <see cref="Scp079SubroutineContainer"/> containing the subroutines.</returns>
    // ReSharper disable once CognitiveComplexity
    public static Scp079SubroutineContainer Get(Scp079Role role)
    {
        if (role == null)
            return Empty;
        Scp079TierManager tierManager = null;
        Scp079AuxManager auxManager = null;
        Scp079CurrentCameraSync cameraSync = null;
        Scp079LostSignalHandler signalHandler = null;
        Scp079RewardManager rewardManager = null;
        Scp079BlackoutZoneAbility zoneBlackout = null;
        Scp079TeslaAbility tesla = null;
        Scp079DoorLockChanger doorLock = null;
        Scp079LockdownRoomAbility lockdownRoom = null;
        var propertiesSet = 0;
        foreach (var sub in role.SubroutineModule.AllSubroutines)
            switch (sub)
            {
                case Scp079TierManager t:
                    tierManager = t;
                    propertiesSet++;
                    break;
                case Scp079AuxManager a:
                    auxManager = a;
                    propertiesSet++;
                    break;
                case Scp079CurrentCameraSync sync:
                    cameraSync = sync;
                    propertiesSet++;
                    break;
                case Scp079LostSignalHandler signal:
                    signalHandler = signal;
                    propertiesSet++;
                    break;
                case Scp079RewardManager r:
                    rewardManager = r;
                    propertiesSet++;
                    break;
                case Scp079BlackoutZoneAbility bz:
                    zoneBlackout = bz;
                    propertiesSet++;
                    break;
                case Scp079TeslaAbility t:
                    tesla = t;
                    propertiesSet++;
                    break;
                case Scp079DoorLockChanger d:
                    doorLock = d;
                    propertiesSet++;
                    break;
                case Scp079LockdownRoomAbility l:
                    lockdownRoom = l;
                    propertiesSet++;
                    break;
            }

        return propertiesSet != 9
            ? Empty
            : new Scp079SubroutineContainer(
                tierManager,
                zoneBlackout,
                cameraSync,
                auxManager,
                signalHandler,
                rewardManager,
                tesla,
                doorLock,
                lockdownRoom
            );
    }

}
