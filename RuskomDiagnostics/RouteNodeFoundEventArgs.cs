using System;

namespace RuskomDiagnostics
{
    /// <summary>
    /// Provides data for the RouteCompleted event of <see cref="Tracert"/>
    /// </summary>
    public class RouteNodeFoundEventArgs : EventArgs
    {
        /// <summary>
        /// A node encountered during the route tracing.
        /// </summary>
        public TracertNode NodeProperty { get ; }

        /// <summary>
        /// </summary>
        /// <param name="node"></param>
        protected internal RouteNodeFoundEventArgs ( TracertNode node )
        {
            this.NodeProperty = node ;
        }
    }
}
