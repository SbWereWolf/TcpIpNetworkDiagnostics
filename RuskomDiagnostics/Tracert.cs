using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.NetworkInformation;

namespace RuskomDiagnostics
{
    // ReSharper disable WordIsNotInDictionary
    /// <summary>
    /// A component which provides network route tracing functionality similar to tracert.exe
    /// </summary>
    /// // ReSharper restore WordIsNotInDictionary
    public class Tracert : Component
    {
        /// <summary>
        /// </summary>
        private Ping Ping1 { get ; set ; }

        /// <summary>
        /// </summary>
        private List < TracertNode > NodesList { get ; set ; }

        /// <summary>
        /// </summary>
        private bool _isDone;

        /// <summary>
        /// </summary>
        private IPAddress Destination { get ; set ; }

        /// <summary>
        /// </summary>
        private PingOptions TracerPingOptions { get ; set ; }

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

                if (value)
                {
                    this.Done?.Invoke
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
        private static byte [ ] OperativeBuffer { get ; set ; }

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
            if (this.Ping1 != null)
            {
                throw new InvalidOperationException
                    ("This object is already in use");
            }

            this.NodesList = new List<TracertNode>();

            var hostNameOrAddress = this._hostNameOrAddress;
            if (hostNameOrAddress != null)
            {
                var addressList = Dns.GetHostEntry
                    (
                        hostNameOrAddress)
                                     .AddressList;
                if (addressList != null)
                {
                    this.Destination = addressList[0];
                }
            }

            var ipAddress = this.Destination;
            if (( ipAddress != null )
                && IPAddress.IsLoopback
                       (
                           ipAddress))
            {
                var destination = this.Destination;
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
                this.Ping1 = new Ping();
                // ReSharper restore InconsistentlySynchronizedField

                this.Ping1.PingCompleted += this.OnPingCompleted;
                this.TracerPingOptions = new PingOptions
                    (
                    1,
                    true);
                var destination = this.Destination;
                if (destination != null)
                {
                    if (Tracert.Buffer != null)
                    {
                        // ReSharper disable InconsistentlySynchronizedField
                        this.Ping1.SendAsync
                            // ReSharper restore InconsistentlySynchronizedField
                            (
                                destination,
                                this._timeout,
                                Tracert.Buffer,
                                this.TracerPingOptions,
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
            [ Annotations.CanBeNull ] PingCompletedEventArgs e)
        {
            if ( e?.Reply?.Address != null )
            {
                this.ProcessNode
                    (
                        e.Reply.Address,
                        e.Reply.Status);
            }

            var tracerPingOptions = this.TracerPingOptions;
            if (tracerPingOptions != null)
            {
                tracerPingOptions.Ttl += 1;

                if (!this.IsDone)
                {
                    lock (this)
                    {
                        //The expectation was that SendAsync will throw an exception
                        if (this.Ping1 == null)
                        {
                            var ipAddress = this.Destination ;
                            if ( ipAddress != null )
                            {
                                this.ProcessNode
                                    (
                                        ipAddress,
                                        IPStatus.Unknown);
                            }
                        }
                        else
                        {
                            var ipAddress = this.Destination;
                            if (( ipAddress != null )
                                && ( Tracert.Buffer != null ))
                            {
                                this.Ping1.SendAsync
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
            [ Annotations.CanBeNull ] IPAddress address,
            IPStatus status)
        {
            long roundTripTime = 0;

            if (( status == IPStatus.TtlExpired )
                || ( status == IPStatus.Success ))
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

                var tracertNodes = this.NodesList;
                if (tracertNodes != null)
                {
                    lock (tracertNodes)
                    {
                        tracertNodes.Add
                            (
                                node);
                    }
                }

                this.RouteNodeFound?.Invoke
                    (
                        this,
                        new RouteNodeFoundEventArgs(node));
            }

            if (address != null)
            {
                this.IsDone = address.Equals
                    (
                        this.Destination);
            }

            var nodesList = this.NodesList;
            if (nodesList != null)
            {
                lock (nodesList)
                {
                    if (!this.IsDone
                        && ( nodesList.Count >= ( this._maxHops - 1 ) ))
                    {
                        this.ProcessNode
                            (
                                this.Destination,
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
                    if (this.Ping1 != null)
                    {
                        this.Ping1.Dispose();
                        this.Ping1 = null;
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