namespace Axwabo.Helpers {

    /// <summary>Adds a property to the implementing struct to determine whether the struct is not empty.</summary>
    public interface IIsValid {

        /// <summary>Returns true if this instance is valid (not empty).</summary>
        bool IsValid { get; }

    }

}
