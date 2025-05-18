namespace Axwabo.Helpers.Pools;

/// <summary>
/// A pool containing lists of type <typeparamref name="T"/>, allowing for renting from and returning objects to it, thus reducing the amount of object instantiation needed.
/// </summary>
/// <typeparam name="T">The type of list the pool contains.</typeparam>
public class ListPool<T> : PoolBase<List<T>>
{

    /// <summary>A shared instance of the pool.</summary>
    public static readonly ListPool<T> Shared = new();

    /// <inheritdoc />
    protected override Func<List<T>> DefaultSupplier { get; }

    /// <summary>The default size of lists created by this pool.</summary>
    public int DefaultCapacity { get; set; } = 128;

    /// <summary>Creates a pool with no size limit.</summary>
    public ListPool() => DefaultSupplier = () => new List<T>(DefaultCapacity);

    /// <summary>
    /// Creates a pool with the given size limit.
    /// </summary>
    /// <param name="maxSize">The maximum size of the pool.</param>
    public ListPool(uint maxSize) : base(maxSize) => DefaultSupplier = () => new List<T>(((Func<int>) (() => DefaultCapacity))());

    /// <summary>
    /// Clears the given list.
    /// </summary>
    /// <param name="list">The list to clear.</param>
    protected override void ResetObject(List<T> list)
    {
        list.Clear();
        list.Capacity = 0;
    }

    /// <summary>
    /// Rents a list with the given capacity. 
    /// </summary>
    /// <param name="capacity">The capacity of the list.</param>
    /// <returns>A list with the given capacity.</returns>
    public List<T> Rent(int capacity)
    {
        if (!TryDequeue(out var list))
            return new List<T>(capacity);
        list.Capacity = capacity;
        return list;
    }

    /// <summary>
    /// Rents a list with the elements in the given <paramref name="enumerable"/>.
    /// </summary>
    /// <param name="enumerable">The enumerable containing elements.</param>
    /// <returns>A list with the elements in the given enumerable.</returns>
    public List<T> Rent(IEnumerable<T> enumerable) => RentOrGet(DefaultSupplier, l =>
    {
        if (enumerable != null)
            l.AddRange(enumerable);
    });

    /// <summary>
    /// Clears the given list.
    /// </summary>
    /// <param name="list">The list to clear.</param>
    public override void DisposeOfObject(List<T> list) => ResetObject(list);

}
