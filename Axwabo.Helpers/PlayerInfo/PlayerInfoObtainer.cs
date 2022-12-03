using Exiled.API.Features;

namespace Axwabo.Helpers.PlayerInfo {

    public readonly struct PlayerInfoObtainer {

        private static uint _id;

        public readonly PlayerCheck Check;
        public readonly PlayerInfoGetter Get;
        public readonly PlayerRoleSetter SetRole;
        public readonly uint Id;
        
        public bool IsValid => Check != null && Get != null;

        public PlayerInfoObtainer(PlayerCheck check, PlayerInfoGetter get, PlayerRoleSetter setRole = null) {
            Check = check;
            Get = get;
            SetRole = setRole;
            Id = ++_id;
        }

    }

    public delegate bool PlayerCheck(Player player);

    public delegate PlayerInfoBase PlayerInfoGetter(Player player);

    public delegate void PlayerRoleSetter(Player player);

}
