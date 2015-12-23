using System.Net;
using System.Net.NetworkInformation;
using RuskomDiagnostics.Annotations;

namespace RuskomDiagnostics
{
    /// <summary>
    /// Contains data about a node encountered using <c>Tracert</c>
    /// </summary>
    public class TracertNode
    {
        /// <summary>
        /// The time taken to go to the node and come back to the originating node in milliseconds.
        /// </summary>
        public long RoundTripTime { get ; }

        /// <summary>
        /// The <c>IPStatus</c> of request send to the node
        /// </summary>
        public IPStatus Status { get ; }

        /// <summary>
        /// The <c>IPAddress</c> of the node
        /// </summary>
        [NotNull]
        public IPAddress Address { get; private set; }

        /// <summary>
        /// Constructs a new <c>object</c> from the IpAddress of the node and the round trip time taken
        /// </summary>
        /// <param name="address"></param>
        /// <param name="roundTripTime"></param>
        /// <param name="status"></param>
        internal TracertNode
            (
            [NotNull] IPAddress address,
            long roundTripTime,
            IPStatus status
            )
        {
            this.Address = address;
            this.RoundTripTime = roundTripTime;
            this.Status = status;
        }
    }
}
