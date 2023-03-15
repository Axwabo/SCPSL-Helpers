using PlayerRoles.PlayableScps.Scp096;

namespace Axwabo.Helpers.PlayerInfo.Containers;

/// <summary>
/// Contains all main subroutines of the <see cref="Scp096Role"/>.
/// </summary>
public readonly struct Scp096SubroutineContainer
{

    /// <summary>An empty instance representing an invalid object.</summary>
    public static readonly Scp096SubroutineContainer Empty = new();

    /// <summary>SCP-096's Rage state controller.</summary>
    public readonly Scp096StateController State;

    /// <summary>SCP-096's target tracker.</summary>
    public readonly Scp096TargetsTracker TargetsTracker;

    /// <summary>SCP-096's Rage manager.</summary>
    public readonly Scp096RageManager RageManager;

    /// <summary>SCP-096's Charge ability.</summary>
    public readonly Scp096ChargeAbility Charge;

    /// <summary>SCP-096's Rage cycle ability.</summary>
    public readonly Scp096RageCycleAbility RageCycle;

    /// <summary>True if this instance is valid (not empty).</summary>
    public readonly bool IsValid;

    /// <summary>
    /// Creates a new <see cref="Scp096SubroutineContainer"/> instance.
    /// </summary>
    /// <param name="state">SCP-096's Rage state controller.</param>
    /// <param name="targetsTracker">SCP-096's target tracker.</param>
    /// <param name="rageManager">SCP-096's Rage manager.</param>
    /// <param name="charge">SCP-096's Charge ability.</param>
    /// <param name="rageCycle">SCP-096's Rage cycle ability.</param>
    public Scp096SubroutineContainer(Scp096StateController state, Scp096TargetsTracker targetsTracker, Scp096RageManager rageManager, Scp096ChargeAbility charge, Scp096RageCycleAbility rageCycle)
    {
        State = state;
        TargetsTracker = targetsTracker;
        RageManager = rageManager;
        Charge = charge;
        RageCycle = rageCycle;
        IsValid = true;
    }

    /// <summary>
    /// Gets all main subroutines of SCP-096.
    /// </summary>
    /// <param name="role">The role to get the main subroutines from.</param>
    /// <returns>An <see cref="Scp096SubroutineContainer"/> containing the subroutines.</returns>
    public static Scp096SubroutineContainer Get(Scp096Role role)
    {
        if (role == null)
            return Empty;
        Scp096StateController state = null;
        Scp096TargetsTracker targetsTracker = null;
        Scp096RageManager rageManager = null;
        Scp096ChargeAbility charge = null;
        Scp096RageCycleAbility rageCycle = null;
        var propertiesSet = 0;
        foreach (var sub in role.SubroutineModule.AllSubroutines)
            switch (sub)
            {
                case Scp096StateController s:
                    state = s;
                    propertiesSet++;
                    break;
                case Scp096TargetsTracker t:
                    targetsTracker = t;
                    propertiesSet++;
                    break;
                case Scp096RageManager rm:
                    rageManager = rm;
                    propertiesSet++;
                    break;
                case Scp096ChargeAbility c:
                    charge = c;
                    propertiesSet++;
                    break;
                case Scp096RageCycleAbility rc:
                    rageCycle = rc;
                    propertiesSet++;
                    break;
            }

        return propertiesSet != 5
            ? Empty
            : new Scp096SubroutineContainer(state, targetsTracker, rageManager, charge, rageCycle);
    }

}
