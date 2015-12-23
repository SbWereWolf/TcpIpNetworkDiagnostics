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
        /// </summary>
        private readonly long _roundTripTime;

        /// <summary>
        /// </summary>
        private readonly IPStatus _status;

        /// <summary>
        /// The time taken to go to the node and come back to the originating node in milliseconds.
        /// </summary>
        public long RoundTripTime { get { return this._roundTripTime; } }

        /// <summary>
        /// The <c>IPStatus</c> of request send to the node
        /// </summary>
        public IPStatus Status { get { return this._status; } }

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
            this._roundTripTime = roundTripTime;
            this._status = status;
        }
    }
}
