namespace Axwabo.Helpers.PlayerInfo {

    public readonly struct PlayerInfoObtainer {

        private static uint _id;

        public static readonly PlayerInfoObtainer Empty = new();

        public readonly PlayerCheck Check;
        public readonly PlayerInfoGetter Get;
        public readonly PlayerRoleSetter SetRole;
        public readonly uint Id;

        public bool IsValid => Check != null && Get != null;
        public bool CanSetRole => SetRole != null;

        public PlayerInfoObtainer(PlayerCheck check, PlayerInfoGetter get, PlayerRoleSetter setRole = null) {
            Check = check;
            Get = get;
            SetRole = setRole;
            Id = ++_id;
        }

    }

}
