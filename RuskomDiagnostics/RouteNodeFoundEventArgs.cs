using System;

namespace RuskomDiagnostics
{
    /// <summary>
    // ReSharper disable WordCanBeSurroundedWithMetaTags
    /// Provides data for the RouteCompleted event of Tracert
    // ReSharper restore WordCanBeSurroundedWithMetaTags
    /// </summary>
    public class RouteNodeFoundEventArgs : EventArgs
    {
        /// <summary>
        /// </summary>
        private readonly TracertNode _node ;

        /// <summary>
        /// A node encountered during the route tracing.
        /// </summary>
        public TracertNode NodeProperty
        {
            get { return this._node ; }
        }

        /// <summary>
        /// </summary>
        /// <param name="node"></param>
        protected internal RouteNodeFoundEventArgs ( TracertNode node )
        {
            this._node = node ;
        }
    }
}
