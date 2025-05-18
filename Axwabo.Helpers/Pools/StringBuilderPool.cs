using System.Text;

namespace Axwabo.Helpers.Pools;

/// <summary>
/// A pool containing StringBuilders, allowing for renting from and returning objects to it, thus reducing the amount of object instantiation needed.
/// </summary>
public class StringBuilderPool : PoolBase<StringBuilder>
{

    /// <summary>A shared instance of the pool.</summary>
    public static readonly StringBuilderPool Shared = new();

    /// <inheritdoc />
    protected override Func<StringBuilder> DefaultSupplier { get; }

    /// <summary>The default size of lists created by this pool.</summary>
    public int DefaultCapacity { get; set; } = 128;

    /// <summary>Creates a pool with no size limit.</summary>
    public StringBuilderPool() => DefaultSupplier = () => new StringBuilder(DefaultCapacity);

    /// <summary>
    /// Creates a pool with the given size limit.
    /// </summary>
    /// <param name="maxSize">The maximum size of the pool.</param>
    public StringBuilderPool(uint maxSize) : base(maxSize) => DefaultSupplier = () => new StringBuilder(((Func<int>) (() => DefaultCapacity))());

    /// <summary>Resets the string builder to its initial state.</summary>
    protected override void ResetObject(StringBuilder builder) => builder.Clear().Capacity = 0;

    /// <summary>
    /// Rents a StringBuilder with the given string value.
    /// </summary>
    /// <param name="value">Initial value of the builder.</param>
    /// <returns>A StringBuilder with the given value.</returns>
    public StringBuilder Rent(string value) => Rent().Append(value);

    /// <summary>
    /// Rents a StringBuilder with the given capacity. 
    /// </summary>
    /// <param name="capacity">The capacity of the StringBuilder.</param>
    /// <returns>A StringBuilder with the given capacity.</returns>
    public StringBuilder Rent(int capacity) => RentOrGet(DefaultSupplier, b => b.Capacity = capacity);

    /// <summary>
    /// Returns the given StringBuilder to the pool and gives back its value.
    /// </summary>
    /// <param name="obj">The StringBuilder to return.</param>
    /// <returns>The value contained within the StringBuilder.</returns>
    public string ToStringReturn(StringBuilder obj)
    {
        var str = obj.ToString();
        Return(obj);
        return str;
    }

}
