using PlayerRoles.PlayableScps.Scp096;

namespace Axwabo.Helpers.PlayerInfo.Containers {

    public readonly struct Scp096SubroutineContainer : IIsValid {

        public static readonly Scp096SubroutineContainer Empty = new();

        public readonly Scp096StateController State;
        public readonly Scp096TargetsTracker TargetsTracker;
        public readonly Scp096RageManager RageManager;
        public readonly Scp096ChargeAbility Charge;

        public bool IsValid { get; }

        public Scp096SubroutineContainer(Scp096StateController state, Scp096TargetsTracker targetsTracker, Scp096RageManager rageManager, Scp096ChargeAbility charge) {
            State = state;
            TargetsTracker = targetsTracker;
            RageManager = rageManager;
            Charge = charge;
            IsValid = true;
        }

        /// <summary>
        /// Gets all main subroutines of SCP-096.
        /// </summary>
        /// <param name="role">The role to get the main subroutines from.</param>
        /// <returns>An <see cref="Scp096SubroutineContainer"/> containing the subroutines.</returns>
        public static Scp096SubroutineContainer Get(Scp096Role role) {
            Scp096StateController state = null;
            Scp096TargetsTracker targetsTracker = null;
            Scp096RageManager rageManager = null;
            Scp096ChargeAbility charge = null;
            if (role == null)
                return Empty;
            var propertiesSet = 0;
            foreach (var sub in role.SubroutineModule.AllSubroutines)
                switch (sub) {
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
                }

            return propertiesSet != 4
                ? Empty
                : new Scp096SubroutineContainer(state, targetsTracker, rageManager, charge);
        }

    }

}
