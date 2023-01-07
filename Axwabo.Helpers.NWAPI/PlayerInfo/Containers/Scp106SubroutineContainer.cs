using PlayerRoles.PlayableScps.Scp106;

namespace Axwabo.Helpers.PlayerInfo.Containers {

    public readonly struct Scp106SubroutineContainer : IIsValid {

        public static readonly Scp106SubroutineContainer Empty = new();

        public readonly Scp106Vigor Vigor;
        public readonly Scp106Attack Attack;
        public readonly Scp106StalkAbility StalkAbility;
        public readonly Scp106SinkholeController SinkholeController;

        /// <inheritdoc />
        public bool IsValid { get; }

        public Scp106SubroutineContainer(Scp106Vigor vigor, Scp106Attack attack, Scp106StalkAbility stalkAbility, Scp106SinkholeController sinkholeController) {
            Vigor = vigor;
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
        public static Scp106SubroutineContainer Get(Scp106Role role) {
            Scp106Vigor vigor = null;
            Scp106Attack attack = null;
            Scp106StalkAbility stalkAbility = null;
            Scp106SinkholeController sinkholeController = null;
            if (role == null)
                return Empty;
            var propertiesSet = 0;
            foreach (var sub in role.SubroutineModule.AllSubroutines)
                switch (sub) {
                    case Scp106Vigor v:
                        vigor = v;
                        propertiesSet++;
                        break;
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

            return propertiesSet != 4
                ? Empty
                : new Scp106SubroutineContainer(
                    vigor,
                    attack,
                    stalkAbility,
                    sinkholeController
                );
        }

    }

}
