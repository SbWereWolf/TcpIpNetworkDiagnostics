using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;

namespace RuskomDiagnostics
{
    /// <summary>
    /// A component which provides network route tracing functionality similar to tracert.exe
    /// </summary>
    public class Tracert : Component
    {
        /// <summary>
        /// </summary>
        private Ping _ping;

        /// <summary>
        /// </summary>
        private List<TracertNode> _nodesList;

        /// <summary>
        /// </summary>
        private bool _isDone;

        /// <summary>
        /// </summary>
        private IPAddress _destination;

        /// <summary>
        /// </summary>
        private PingOptions _tracerPingOptions;

        /// <summary>
        /// Fires when route tracing is done
        /// </summary>
        public event EventHandler Done;

        /// <summary>
        /// Fires when a node is found in the route
        /// </summary>
        public event EventHandler<RouteNodeFoundEventArgs> RouteNodeFound;

        /// <summary>
        /// 
        /// </summary>
        public Tracert()
        {
            this._timeout = 5000; //Default timeout of Ping
        }

        /// <summary>
        /// </summary>
        private int _maxHops = 40; // private int _maxHops = 30;

        /// <summary>
        /// 
        /// </summary>
        public int MaxHops { set { this._maxHops = value; } }

        /// <summary>
        /// </summary>
        private string _hostNameOrAddress;

        /// <summary>
        /// The host name or IpAddress of the destination node
        /// </summary>
        public string HostNameOrAddress
        {
            set { this._hostNameOrAddress = value; }
        }

        /// <summary>
        /// </summary>
        private int _timeout;

        /// <summary>
        /// The maximum amount of time to wait for the <c>Ping</c> request to an intermediate node
        /// </summary>
        public int TimeOut { set { this._timeout = value; } }

        /// <summary>
        /// Indicates whether the route tracing is complete
        /// </summary>
        private bool IsDone
        {
            get { return this._isDone; }
            set
            {
                this._isDone = value;

                if (value && this.Done != null)
                {
                    this.Done
                        (
                            this,
                            EventArgs.Empty);
                }

                if (this._isDone)
                {
                    this.Dispose();
                }
            }
        }

        /// <summary>
        /// </summary>
        private static byte[] OperativeBuffer;

        /// <summary>
        /// </summary>
        private static byte[] Buffer
        {
            get
            {
                if (Tracert.OperativeBuffer == null)
                {
                    Tracert.OperativeBuffer = new byte[32];

                    for (var i = 0;
                        i < Tracert.OperativeBuffer.Length;
                        i++)
                    {
                        Tracert.OperativeBuffer[i] = 0x65;
                    }
                }
                return Tracert.OperativeBuffer;
            }
        }

        /// <summary>
        /// Starts the route tracing process. The <c>HostNameOrAddress</c> field should already be set
        /// </summary>
        public void Trace()
        {
            if (this._ping != null)
            {
                throw new InvalidOperationException
                    ("This object is already in use");
            }

            this._nodesList = new List<TracertNode>();

            var hostNameOrAddress = this._hostNameOrAddress;
            if (hostNameOrAddress != null)
            {
                var addressList = Dns.GetHostEntry
                    (
                        hostNameOrAddress)
                                     .AddressList;
                if (addressList != null)
                {
                    this._destination = addressList[0];
                }
            }

            var ipAddress = this._destination;
            if (ipAddress != null
                && IPAddress.IsLoopback
                       (
                           ipAddress))
            {
                var destination = this._destination;
                if (destination != null)
                {
                    this.ProcessNode
                        (
                            destination,
                            IPStatus.Success);
                }
            }
            else
            {

                // ReSharper disable InconsistentlySynchronizedField
                this._ping = new Ping();
                // ReSharper restore InconsistentlySynchronizedField

                this._ping.PingCompleted += this.OnPingCompleted;
                this._tracerPingOptions = new PingOptions
                    (
                    1,
                    true);
                var destination = this._destination;
                if (destination != null)
                {
                    if (Tracert.Buffer != null)
                    {
                        // ReSharper disable InconsistentlySynchronizedField
                        this._ping.SendAsync
                            // ReSharper restore InconsistentlySynchronizedField
                            (
                                destination,
                                this._timeout,
                                Tracert.Buffer,
                                this._tracerPingOptions,
                                null);
                    }
                }

            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPingCompleted
            (
            object sender,
            PingCompletedEventArgs e)
        {
            if (e != null)
            {
                if (e.Reply != null)
                {
                    this.ProcessNode
                        (
                            e.Reply.Address,
                            e.Reply.Status);
                }
            }

            var tracerPingOptions = this._tracerPingOptions;
            if (tracerPingOptions != null)
            {
                tracerPingOptions.Ttl += 1;

                if (!this.IsDone)
                {
                    lock (this)
                    {
                        //The expectation was that SendAsync will throw an exception
                        if (this._ping == null)
                        {
                            this.ProcessNode
                                (
                                    this._destination,
                                    IPStatus.Unknown);
                        }
                        else
                        {
                            var ipAddress = this._destination;
                            if (ipAddress != null
                                && Tracert.Buffer != null)
                            {
                                this._ping.SendAsync
                                    (
                                        ipAddress,
                                        this._timeout,
                                        Tracert.Buffer,
                                        tracerPingOptions,
                                        null);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="status"></param>
        private void ProcessNode
            (
            IPAddress address,
            IPStatus status)
        {
            long roundTripTime = 0;

            if (status == IPStatus.TtlExpired
                || status == IPStatus.Success)
            {
                var pingIntermediate = new Ping();

                try
                {
                    //Compute roundtrip time to the IpAddress by pinging it
                    if (address != null)
                    {
                        var reply = pingIntermediate.Send
                            (
                                address,
                                this._timeout);
                        if (reply != null)
                        {
                            roundTripTime = reply.RoundtripTime;
                            status = reply.Status;
                        }
                    }
                }
                catch (PingException e)
                {
                    //Do nothing
                    System.Diagnostics.Trace.WriteLine
                        (
                            e);
                }
                finally
                {
                    pingIntermediate.Dispose();
                }
            }

            if (address != null)
            {
                var node = new TracertNode
                    (
                    address,
                    roundTripTime,
                    status);

                var tracertNodes = this._nodesList;
                if (tracertNodes != null)
                {
                    lock (tracertNodes)
                    {
                        tracertNodes.Add
                            (
                                node);
                    }
                }

                if (this.RouteNodeFound != null)
                {
                    this.RouteNodeFound
                        (
                            this,
                            new RouteNodeFoundEventArgs(node));
                }
            }

            if (address != null)
            {
                this.IsDone = address.Equals
                    (
                        this._destination);
            }

            var nodesList = this._nodesList;
            if (nodesList != null)
            {
                lock (nodesList)
                {
                    if (!this.IsDone
                        && nodesList.Count >= this._maxHops - 1)
                    {
                        this.ProcessNode
                            (
                                this._destination,
                                IPStatus.Success);
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose
            (
            bool disposing)
        {
            try
            {
                lock (this)
                {
                    if (this._ping != null)
                    {
                        this._ping.Dispose();
                        this._ping = null;
                    }
                }
            }
            finally
            {
                base.Dispose
                    (
                        disposing);
            }
        }
    }
}