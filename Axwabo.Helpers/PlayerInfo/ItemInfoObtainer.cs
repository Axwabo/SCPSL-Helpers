using System;

namespace Axwabo.Helpers.PlayerInfo {

    /// <summary>
    /// A struct containing various item info related methods.
    /// </summary>
    public readonly struct ItemInfoObtainer : IIsValid {

        private static uint _id;

        /// <summary>
        /// An empty <see cref="ItemInfoObtainer"/> instance.
        /// </summary>
        public static readonly ItemInfoObtainer Empty = new();

        /// <summary>
        /// A method to check if the item is suitable for the item info getter.
        /// </summary>
        public readonly ItemCheck Check;

        /// <summary>
        /// A method to get the item info.
        /// </summary>
        public readonly ItemInfoGetter Get;

        /// <summary>
        /// The auto-incrementing id of this instance.
        /// </summary>
        public readonly uint Id;

        /// <inheritdoc />
        public bool IsValid => Id > 0 && Check != null && Get != null;

        /// <summary>
        /// Creates a new <see cref="ItemInfoObtainer"/> instance.
        /// </summary>
        /// <param name="check">A method to check if the item is suitable for the item info getter.</param>
        /// <param name="get">A method to get the item info.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="check"/> or <paramref name="get"/> is null.</exception>
        public ItemInfoObtainer(ItemCheck check, ItemInfoGetter get) {
            Check = check ?? throw new ArgumentNullException(nameof(check));
            Get = get ?? throw new ArgumentNullException(nameof(get));
            Id = ++_id;
        }

    }

}
