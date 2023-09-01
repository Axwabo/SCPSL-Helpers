using PlayerRoles.PlayableScps.Scp106;

namespace Axwabo.Helpers.PlayerInfo.Containers;

/// <summary>
/// Contains all main subroutines of the <see cref="Scp106Role"/>.
/// </summary>
public readonly struct Scp106SubroutineContainer
{

    /// <summary>An empty instance representing an invalid object.</summary>
    public static readonly Scp106SubroutineContainer Empty = new();

    /// <summary>SCP-106's attack ability.</summary>
    public readonly Scp106Attack Attack;

    /// <summary>SCP-106's stalk ability.</summary>
    public readonly Scp106StalkAbility StalkAbility;

    /// <summary>SCP-106's sinkhole controller.</summary>
    public readonly Scp106SinkholeController SinkholeController;

    /// <summary>True if this instance is valid (not empty).</summary>
    public readonly bool IsValid;

    /// <summary>
    /// Creates a new <see cref="Scp106SubroutineContainer"/> instance.
    /// </summary>
    /// <param name="attack">SCP-106's attack ability.</param>
    /// <param name="stalkAbility">SCP-106's stalk ability.</param>
    /// <param name="sinkholeController">SCP-106's sinkhole controller.</param>
    public Scp106SubroutineContainer(Scp106Attack attack, Scp106StalkAbility stalkAbility, Scp106SinkholeController sinkholeController)
    {
        Attack = attack;
        StalkAbility = stalkAbility;
        SinkholeController = sinkholeController;
        IsValid = true;
    }

    /// <summary>
    /// Gets all main subroutines of SCP-106.
    /// </summary>
    /// <param name="role">The role to get the main subroutines from.</param>
    /// <returns>An <see cref="Scp106SubroutineContainer"/> containing the subroutines.</returns>
    public static Scp106SubroutineContainer Get(Scp106Role role)
    {
        if (role == null)
            return Empty;
        Scp106Attack attack = null;
        Scp106StalkAbility stalkAbility = null;
        Scp106SinkholeController sinkholeController = null;
        var propertiesSet = 0;
        foreach (var sub in role.SubroutineModule.AllSubroutines)
            switch (sub)
            {
                case Scp106StalkAbility stalk:
                    stalkAbility = stalk;
                    propertiesSet++;
                    break;
                case Scp106SinkholeController sinkhole:
                    sinkholeController = sinkhole;
                    propertiesSet++;
                    break;
                case Scp106Attack a:
                    attack = a;
                    propertiesSet++;
                    break;
            }

        return propertiesSet != 3
            ? Empty
            : new Scp106SubroutineContainer(
                attack,
                stalkAbility,
                sinkholeController
            );
    }

}
