using PlayerRoles.PlayableScps.Scp173;

namespace Axwabo.Helpers.PlayerInfo.Containers {

    public readonly struct Scp173SubroutineContainer : IIsValid {

        public static readonly Scp173SubroutineContainer Empty = new();

        public readonly Scp173BlinkTimer BlinkTimer;
        public readonly Scp173BreakneckSpeedsAbility BreakneckSpeeds;
        public readonly Scp173TantrumAbility Tantrum;

        /// <inheritdoc />
        public bool IsValid { get; }

        public Scp173SubroutineContainer(Scp173BlinkTimer blinkTimer, Scp173BreakneckSpeedsAbility breakneckSpeeds, Scp173TantrumAbility tantrum) {
            BlinkTimer = blinkTimer;
            BreakneckSpeeds = breakneckSpeeds;
            Tantrum = tantrum;
            IsValid = true;
        }

        /// <summary>
        /// Gets all main subroutines of SCP-173.
        /// </summary>
        /// <param name="role">The role to get the main subroutines from.</param>
        /// <returns>An <see cref="Scp173SubroutineContainer"/> containing the subroutines.</returns>
        public static Scp173SubroutineContainer Get(Scp173Role role) {
            Scp173BlinkTimer blinkTimer = null;
            Scp173BreakneckSpeedsAbility breakneckSpeeds = null;
            Scp173TantrumAbility tantrum = null;
            if (role == null)
                return Empty;
            var propertiesSet = 0;
            foreach (var sub in role.SubroutineModule.AllSubroutines)
                switch (sub) {
                    case Scp173BlinkTimer blink:
                        blinkTimer = blink;
                        propertiesSet++;
                        break;
                    case Scp173BreakneckSpeedsAbility b:
                        breakneckSpeeds = b;
                        propertiesSet++;
                        break;
                    case Scp173TantrumAbility t:
                        tantrum = t;
                        propertiesSet++;
                        break;
                }

            return propertiesSet != 3
                ? Empty
                : new Scp173SubroutineContainer(
                    blinkTimer,
                    breakneckSpeeds,
                    tantrum
                );
        }

    }

}
