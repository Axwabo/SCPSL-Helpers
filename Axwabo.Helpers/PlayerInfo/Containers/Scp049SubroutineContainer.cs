using PlayerRoles.PlayableScps.Scp049;

namespace Axwabo.Helpers.PlayerInfo.Containers {

    /// <summary>
    /// Contains all main subroutines of the <see cref="Scp049Role"/>.
    /// </summary>
    public readonly struct Scp049SubroutineContainer {

        /// <summary>An empty instance representing an invalid object.</summary>
        public static readonly Scp049SubroutineContainer Empty = new();

        /// <summary>"The Doctor's Call" ability.</summary>
        public readonly Scp049CallAbility CallAbility;

        /// <summary>The "Good Sense of the Doctor" ability.</summary>
        public readonly Scp049SenseAbility SenseAbility;

        /// <summary>SCP-049's attack ability.</summary>
        public readonly Scp049AttackAbility AttackAbility;

        /// <summary>True if this instance is valid (not empty).</summary>
        public readonly bool IsValid;

        /// <summary>
        /// Creates a new <see cref="Scp049SubroutineContainer"/> instance.
        /// </summary>
        /// <param name="callAbility">"The Doctor's Call" ability.</param>
        /// <param name="senseAbility">The "Good Sense of the Doctor" ability.</param>
        /// <param name="attackAbility">SCP-049's attack ability.</param>
        public Scp049SubroutineContainer(Scp049CallAbility callAbility, Scp049SenseAbility senseAbility, Scp049AttackAbility attackAbility) {
            CallAbility = callAbility;
            SenseAbility = senseAbility;
            AttackAbility = attackAbility;
            IsValid = true;
        }

        /// <summary>
        /// Gets all main subroutines of SCP-049.
        /// </summary>
        /// <param name="role">The role to get the main subroutines from.</param>
        /// <returns>An <see cref="Scp049SubroutineContainer"/> containing the subroutines.</returns>
        public static Scp049SubroutineContainer Get(Scp049Role role) {
            if (role == null)
                return Empty;
            Scp049CallAbility call = null;
            Scp049SenseAbility sense = null;
            Scp049AttackAbility attack = null;
            var propertiesSet = 0;
            foreach (var sub in role.SubroutineModule.AllSubroutines)
                switch (sub) {
                    case Scp049CallAbility c:
                        call = c;
                        propertiesSet++;
                        break;
                    case Scp049SenseAbility s:
                        sense = s;
                        propertiesSet++;
                        break;
                    case Scp049AttackAbility a:
                        attack = a;
                        propertiesSet++;
                        break;
                }

            return propertiesSet != 3
                ? Empty
                : new Scp049SubroutineContainer(
                    call,
                    sense,
                    attack
                );
        }

    }

}
