using System;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using RuskomDiagnostics.Annotations;

namespace RuskomDiagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TraceHostForm : Form
    {
        /// <summary>
        /// </summary>
        private bool _formShown ;

        /// <summary>
        /// 
        /// </summary>
        public TraceHostForm ( )
        {
            this.InitializeComponent( ) ;
            this._formShown = false ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoTraceRouteButton_Click
            (
            object sender ,
            EventArgs e )
        {
            this.TraceRoute( ) ;
        }

        /// <summary>
        /// </summary>
        private void TraceRoute ( )
        {
            var startControl = this.DoTraceRouteButton;
            var stopControl = this.Do—loseFormButton ;
            Handler.DisableStarProgressStopControls
                (
                 startControl
                 , stopControl
                 , null
                ) ;

            var tracert = this.Tracert ;
            if ( tracert != null )
            {
                var hostAddressTextBox = this.HostAddressTextBox ;
                if ( hostAddressTextBox != null )
                {
                    tracert.HostNameOrAddress = hostAddressTextBox.Text ;
                }
                var routeListView = this.RouteListView ;
                routeListView?.Items.Clear( ) ;
                try
                {
                    tracert.Trace( ) ;
                }
                catch ( Exception )
                {
                    Handler.EnableStartProgressStopControls
                        (
                         startControl ,
                         stopControl ,
                         null
                        ) ;
                }
            }
        }

        /// <summary>
        /// </summary>
        private delegate void ThreadSwitch ( ) ;

        /// <summary>
        /// </summary>
        /// <param name="ar"></param>
        private void OnGetHostEntry (
            [ CanBeNull ] IAsyncResult ar )
        {
            try
            {
                if ( ar != null )
                {
                    var hostNameItem =
                        ar.AsyncState as ListViewItem.ListViewSubItem ;
                    ThreadSwitch threadSwitchDelegate =
                        delegate
                        {
                            if ( hostNameItem != null )
                            {
                                var endGetHostEntry = Dns.EndGetHostEntry ( ar ) ;
                                if (
                                    endGetHostEntry != null )
                                {
                                    hostNameItem.Text = endGetHostEntry
                                        .HostName ;
                                }
                            }
                        } ;

                    this.Invoke ( threadSwitchDelegate ) ;
                }
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch ( Exception )
                // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tracert_RouteNodeFound
            (
            object sender ,
            [ CanBeNull ] RouteNodeFoundEventArgs e )
        {
            var routeListView = this.RouteListView ;
            if ( routeListView != null )
            {
                if ( e?.NodeProperty != null )
                {
                    {
                        var item = routeListView.Items.Add
                            ( e.NodeProperty.Address.ToString( ) ) ;
                        var listViewItem =
                            routeListView.Items[ routeListView.Items.Count - 1 ] ;
                        listViewItem?.EnsureVisible( ) ;

                        item.SubItems.Add
                            (
                                ( item.Index + 1 ).ToString
                                    ( CultureInfo.InvariantCulture ) ) ;
                        var hostNameItem = item.SubItems.Add ( string.Empty ) ;
                        item.SubItems.Add
                            (
                                e.NodeProperty.Status == IPStatus.Success
                                    ? e.NodeProperty.RoundTripTime.ToString
                                          ( CultureInfo.InvariantCulture )
                                    : "*" ) ;

                        if ( e.NodeProperty.Status == IPStatus.Success )
                        {
                            Dns.BeginGetHostEntry
                                (
                                    e.NodeProperty.Address ,
                                    this.OnGetHostEntry ,
                                    hostNameItem ) ;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tracert_Done
            (
            object sender ,
            EventArgs e )
        {
            var hostAddressTextBox = this.HostAddressTextBox ;
            if ( hostAddressTextBox != null )
            {
                var hostName = hostAddressTextBox.Text ;
                
                var routeListView = this.RouteListView ;
                if ( routeListView != null )
                {
                    var item = routeListView.Items.Add ( hostName ) ;
                    var listViewItem =
                        routeListView.Items[ routeListView.Items.Count - 1 ] ;
                    listViewItem?.EnsureVisible( ) ;
                    item.SubItems.Add
                        (
                         ( item.Index + 1 ).ToString
                             ( CultureInfo.InvariantCulture ) ) ;
                    item.SubItems.Add ( Handler.TraceComplete ) ;
                }
            }

            var startButton = this.DoTraceRouteButton ;
            var stopButton = this.Do—loseFormButton ;

            Handler.EnableStartProgressStopControls
                (
                 startButton ,
                 stopButton ,
                 null
                ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostAddressTextBox_KeyDown
            (
            object sender ,
            KeyEventArgs e )
        {
            if ( ( e.KeyCode == Keys.Enter ) )
            {
                var doTraceRouteButton = this.DoTraceRouteButton ;
                if ( ( doTraceRouteButton != null )
                     && doTraceRouteButton.Enabled )
                {
                    this.DoTraceRouteButton_Click
                        (
                         this ,
                         new EventArgs( ) ) ;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TraceToHostForm_FormClosing
            (
            object sender ,
            FormClosingEventArgs e )
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    Hide();
            //    e.Cancel = true;
            //}
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoCloseFormButton_Click
            (
            object sender ,
            EventArgs e )
        {
            Handler.CloseForm ( this ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoCopyAll_Click
            (
            object sender ,
            EventArgs e )
        {
            TraceHostForm.CopyListViewContentToClipboard
                (
                 this.RouteListView ,
                 Handler.DefaultColumnName ,
                 Handler.EmptyColumnValue,
                 Environment.NewLine
                ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="routeListView"></param>
        /// <param name="defaultColumnName"></param>
        /// <param name="defaultCellText"></param>
        /// <param name="rowsSeparator"></param>
        private static void CopyListViewContentToClipboard
            (
            [ CanBeNull ] ListView routeListView ,
            [ CanBeNull ] string defaultColumnName ,
            [ CanBeNull ] string defaultCellText ,
            [ CanBeNull ] string rowsSeparator
            )
        {
            var content = TraceHostForm.GetListViewContentAsString
                (
                 routeListView ,
                 defaultColumnName ,
                 defaultCellText ,
                 rowsSeparator
                ) ;

            Clipboard.Clear( ) ;
            Clipboard.SetText ( content ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="routeListView"></param>
        /// <param name="defaultColumnName"></param>
        /// <param name="defaultCellText"></param>
        /// <param name="rowsSeparator"></param>
        /// <returns></returns>
        [ NotNull ]
        private static string GetListViewContentAsString
            (
            [ CanBeNull ] ListView routeListView ,
            [CanBeNull] string defaultColumnName ,
            [CanBeNull] string defaultCellText ,
            [CanBeNull] string rowsSeparator
            )
        {
            var contentAsText = string.Empty ;
            var tryAction = routeListView != null ;
            if ( tryAction )
            {
                contentAsText = TraceHostForm.GetListViewText(
                    routeListView,
                    defaultColumnName,
                    defaultCellText,
                    rowsSeparator);
            }
            return contentAsText;
        }

        /// <summary>
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="defaultColumnName"></param>
        /// <param name="defaultCellText"></param>
        /// <param name="rowsSeparator"></param>
        /// <returns></returns>
        private static string GetListViewText
            (
            [CanBeNull] ListView listView,
            [CanBeNull] string defaultColumnName,
            [CanBeNull] string defaultCellText,
            [CanBeNull] string rowsSeparator)
        {
            var listViewContentBuilder = new StringBuilder();

            if ( listView != null )
            {
                var columncount = listView.Items.Count ;

                for (var rowsCounter = 0;
                     rowsCounter < columncount;
                     rowsCounter++)
                {
                    TraceHostForm.GetListViewRowText
                        (
                            listView,
                            defaultColumnName,
                            defaultCellText,
                            rowsCounter,
                            listViewContentBuilder);
                    listViewContentBuilder.Append
                        (
                            rowsSeparator);
                }
            }
            var contentAsText = listViewContentBuilder.ToString();
            return contentAsText;
        }

        /// <summary>
        /// </summary>
        /// <param name="routeListView"></param>
        /// <param name="defaultColumnName"></param>
        /// <param name="defaultCellText"></param>
        /// <param name="rowsCounter"></param>
        /// <param name="listViewContentBuilder"></param>
        private static void GetListViewRowText
            (
            [ CanBeNull ] ListView routeListView ,
            [ CanBeNull ] string defaultColumnName ,
            [ CanBeNull ] string defaultCellText ,
            int rowsCounter ,
            [ CanBeNull ] StringBuilder listViewContentBuilder )
        {
            if ( routeListView != null )
            {
                var columnsCount = routeListView.Columns.Count ;
                for ( var columnsCounter = 0 ;
                      columnsCounter < columnsCount ;
                      columnsCounter++ )
                {
                    if ( ( defaultColumnName != null )
                         && ( defaultCellText != null ))
                    {
                        var columnName = TraceHostForm.GetColumnName
                            (
                                routeListView ,
                                defaultColumnName ,
                                columnsCounter ) ;

                        var cellText = TraceHostForm.GetCellText
                            (
                                routeListView ,
                                defaultCellText ,
                                rowsCounter ,
                                columnsCounter ) ;

                        var text = $" {columnName} : '{cellText}' ;" ;
                        listViewContentBuilder?.Append
                            (
                                text);
                    }



                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="listView"></param>
        /// <param name="defaultCellText"></param>
        /// <param name="rowsCounter"></param>
        /// <param name="columnsCounter"></param>
        /// <returns></returns>
        [ CanBeNull ]
        private static string GetCellText
            (
            [ CanBeNull ] ListView listView,
            [ CanBeNull ] string defaultCellText,
            int rowsCounter,
            int columnsCounter)
        {
            var cellText = string.Empty;
            try
            {
                if ( listView != null )
                {
                    var listViewItem =
                        listView.Items[rowsCounter];
                    var listViewSubItem = listViewItem?.SubItems[columnsCounter];
                    if (listViewSubItem != null)
                    {
                        cellText =
                            listViewSubItem
                                .Text;
                    }
                }
            }
            catch (Exception)
            {
                cellText = defaultCellText;
            }
            return cellText;
        }

        /// <summary>
        /// </summary>
        /// <param name="routeListView"></param>
        /// <param name="defaultColumnName"></param>
        /// <param name="columnsCounter"></param>
        /// <returns></returns>
        [ CanBeNull ]
        private static string GetColumnName
            (
            [ CanBeNull ] ListView routeListView,
            string defaultColumnName,
            int columnsCounter)
        {
            var columnName = string.Empty;
            try
            {
                var columnHeader =
                    routeListView?.Columns[columnsCounter];
                if (columnHeader != null)
                {
                    columnName = columnHeader.Text;
                }
            }
            catch (Exception)
            {
                columnName = defaultColumnName;
            }
            return columnName;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TraceHostForm_Load
            (
            object sender ,
            EventArgs e )
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TraceHostForm_Shown
            (
            object sender ,
            EventArgs e )
        {
            if ( this._formShown )
            {
                return ;
            }
            this._formShown = true ;
            this.TraceRoute( ) ;
        }
    }
}