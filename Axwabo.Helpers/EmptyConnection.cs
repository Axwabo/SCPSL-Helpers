using Mirror;

namespace Axwabo.Helpers;

/// <summary>
/// A network connection that does nothing.
/// </summary>
public sealed class EmptyConnection : NetworkConnectionToClient
{

    /// <summary>
    /// Creates a new instance of the <see cref="EmptyConnection"/> class.
    /// </summary>
    /// <param name="networkConnectionId">The network connection ID.</param>
    public EmptyConnection(int networkConnectionId) : base(networkConnectionId)
    {
    }

    /// <summary>
    /// Consumes the message.
    /// </summary>
    /// <param name="segment">The data.</param>
    /// <param name="channelId">The channel ID.</param>
    public override void Send(ArraySegment<byte> segment, int channelId = 0)
    {
    }

    /// <summary>
    /// Disconnects literally nothing lmfao.
    /// </summary>
    public override void Disconnect()
    {
    }

    /// <summary>
    /// The address of the connection, always localhost.
    /// </summary>
    public override string address => "localhost";

}
