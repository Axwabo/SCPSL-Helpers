namespace Axwabo.Helpers;

/// <summary>
/// Sends the hints for the player from a queue.
/// </summary>
[DisallowMultipleComponent]
public sealed class HintQueue : MonoBehaviour
{

    /// <summary>
    /// A container for a hint message and its duration.
    /// </summary>
    public readonly struct HintItem
    {

        /// <summary>The hint message.</summary>
        public readonly string Message;

        /// <summary>The amount of time (in seconds) the hint should be displayed.</summary>
        public readonly float Duration;

        /// <summary>
        /// Creates a new <see cref="HintItem"/> instance.
        /// </summary>
        /// <param name="message">The hint message.</param>
        /// <param name="duration">The duration of the hint.</param>
        public HintItem(string message, float duration)
        {
            Message = message;
            Duration = duration;
        }

    }

    private readonly Queue<HintItem> _queue = new();

    /// <summary>The <see cref="Player"/> this component is attached to.</summary>
    public Player Player { get; private set; }

    /// <summary>The hint currently displayed to the player. Null if no hint is being displayed.</summary>
    public string CurrentHint { get; private set; }

    /// <summary>
    /// Adds a hint to the queue.
    /// </summary>
    /// <param name="message">The hint message.</param>
    /// <param name="duration">The duration of the hint.</param>
    public void Enqueue(string message, float duration = 5f) => _queue.Enqueue(new HintItem(message, duration));

    /// <summary>Clears the queue and current hint.</summary>
    public void Clear()
    {
        _queue.Clear();
        CurrentHint = null;
        Player.SendHint("", 0);
    }

    /// <summary>Clears the queue while keeping the current hint.</summary>
    public void ClearQueue() => _queue.Clear();

    private void Awake() => Player = Player.Get(gameObject) ?? throw new InvalidOperationException("HintQueue must be attached to a player.");

    private void Update()
    {
        /*
        if (Player.HasHint)
            return;
        */
        CurrentHint = null;
        if (!_queue.TryDequeue(out var item))
            return;
        CurrentHint = item.Message;
        Player.SendHint(item.Message, item.Duration);
    }

}
