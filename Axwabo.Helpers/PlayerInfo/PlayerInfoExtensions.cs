using Exiled.API.Features;

namespace Axwabo.Helpers.PlayerInfo {

    public static class PlayerInfoExtensions {

        public static PlayerInfoBase GetInfo(this Player p) => PlayerInfoBase.CreateAutomatically(p);

        public static RoleTypeAndInfoWrapper GetInfoWithVanillaRole(this Player p) =>
            RoleTypeAndInfoWrapper.Get(p, PlayerInfoBase.CreateAutomatically);

        public static CustomRoleAndInfoWrapper GetInfoWithCustomRole(this Player p) =>
            GetInfoWithCustomRole(p, PlayerInfoBase.GetFirstMatchingObtainer(p));

        public static CustomRoleAndInfoWrapper GetInfoWithCustomRole(this Player p, PlayerInfoObtainer obtainer) =>
            !obtainer.IsValid ? CustomRoleAndInfoWrapper.Empty : CustomRoleAndInfoWrapper.Get(p, obtainer);

    }

}
