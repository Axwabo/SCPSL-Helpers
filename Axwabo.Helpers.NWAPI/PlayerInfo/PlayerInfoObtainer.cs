using System;

namespace Axwabo.Helpers.PlayerInfo {

    /// <summary>
    /// A struct containing various player info related methods.
    /// </summary>
    public readonly struct PlayerInfoObtainer {

        private static uint _id;

        /// <summary>
        /// An empty <see cref="PlayerInfoObtainer"/> instance.
        /// </summary>
        public static readonly PlayerInfoObtainer Empty = new();

        /// <summary>
        /// A method to check if the player is suitable for the player info getter.
        /// </summary>
        public readonly PlayerCheck Check;

        /// <summary>
        /// A method to get the player info.
        /// </summary>
        public readonly PlayerInfoGetter Get;

        /// <summary>
        /// A method to set the player's custom role.
        /// </summary>
        public readonly PlayerRoleSetter SetRole;

        /// <summary>
        /// The auto-incrementing id of this instance.
        /// </summary>
        public readonly uint Id;

        /// <summary>
        /// Returns true if this instance is valid (not empty).
        /// </summary>
        public bool IsValid => Id > 0 && Check != null && Get != null;

        /// <summary>
        /// Returns true if the <see cref="SetRole"/> method is not null.
        /// </summary>
        public bool CanSetRole => SetRole != null;

        /// <summary>
        /// Creates a new <see cref="PlayerInfoObtainer"/> instance.
        /// </summary>
        /// <param name="check">A method to check if the player is suitable for the player info getter.</param>
        /// <param name="get">A method to get the player info.</param>
        /// <param name="setRole">A method to set the player's custom role. Can be null.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="check"/> or <paramref name="get"/> is null.</exception>
        public PlayerInfoObtainer(PlayerCheck check, PlayerInfoGetter get, PlayerRoleSetter setRole = null) {
            Check = check ?? throw new ArgumentNullException(nameof(check));
            Get = get ?? throw new ArgumentNullException(nameof(get));
            SetRole = setRole;
            Id = ++_id;
        }

    }

}
