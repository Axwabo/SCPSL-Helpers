using PlayerRoles.PlayableScps.Scp049;

namespace Axwabo.Helpers.PlayerInfo.Containers {

    public readonly struct Scp049SubroutineContainer : IIsValid {

        public static readonly Scp049SubroutineContainer Empty = new();

        public readonly Scp049CallAbility CallAbility;
        public readonly Scp049SenseAbility SenseAbility;
        public readonly Scp049AttackAbility AttackAbility;

        /// <inheritdoc />
        public bool IsValid { get; }

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
            Scp049CallAbility call = null;
            Scp049SenseAbility sense = null;
            Scp049AttackAbility attack = null;
            if (role == null)
                return Empty;
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
