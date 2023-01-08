using PlayerRoles.PlayableScps.Scp173;

namespace Axwabo.Helpers.PlayerInfo.Containers {

    /// <summary>
    /// Contains all main subroutines of the <see cref="Scp173Role"/>.
    /// </summary>
    public readonly struct Scp173SubroutineContainer {

        /// <summary>An empty instance representing an invalid object.</summary>
        public static readonly Scp173SubroutineContainer Empty = new();

        /// <summary>SCP-173's Blink timer.</summary>
        public readonly Scp173BlinkTimer BlinkTimer;

        /// <summary>SCP-173's Breakneck Speeds ability.</summary>
        public readonly Scp173BreakneckSpeedsAbility BreakneckSpeeds;

        /// <summary>SCP-173's Tantrum ability.</summary>
        public readonly Scp173TantrumAbility Tantrum;

        /// <summary>True if this instance is valid (not empty).</summary>
        public readonly bool IsValid;

        /// <summary>
        /// Creates a new <see cref="Scp173SubroutineContainer"/> instance.
        /// </summary>
        /// <param name="blinkTimer">SCP-173's Blink timer.</param>
        /// <param name="breakneckSpeeds">SCP-173's Breakneck Speeds ability.</param>
        /// <param name="tantrum">SCP-173's Tantrum ability.</param>
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
            if (role == null)
                return Empty;
            Scp173BlinkTimer blinkTimer = null;
            Scp173BreakneckSpeedsAbility breakneckSpeeds = null;
            Scp173TantrumAbility tantrum = null;
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
