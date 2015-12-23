using System;
using System.Windows.Forms;
using Microsoft.Win32;
using RuskomDiagnostics.Properties;

namespace RuskomDiagnostics
{
    // ReSharper disable WordIsNotInDictionary
    /// <summary>
    /// Форма диагностики связи с сайтом Рускома
    /// </summary>
    /// // ReSharper restore WordIsNotInDictionary
    public sealed partial class RuskomDiagnosticsForm : Form
    {
        /// <summary>
        /// </summary>
        // ReSharper disable IdentifierWordIsNotInDictionary
        // ReSharper disable IdentifierWordIsNotInDictionary
        private const int C_CpNoCloseButton = 0x200 ;
        // ReSharper restore IdentifierWordIsNotInDictionary
        // ReSharper restore IdentifierWordIsNotInDictionary

        /// <summary>
        /// </summary>
        private readonly int _communicationPeriodMilliseconds ;
        /* 86400000 */

        /// <summary>
        /// </summary>
        private const int C_DayInMilliseconds = 86400000 ;
        /* 900000 = 1000 * 60 * 15; */

        /// <summary>
        /// </summary>
        private const int C_CommunicationPeriodMilliseconds = 900000 ;

        /// <summary>
        /// </summary>
        private const int C_ShowBalloonTipWithShortMilliseconds = 1000 ;

        /// <summary>
        /// </summary>
        private const int C_ShowBalloonTipWithGreatMilliseconds = 5000 ;

        // ReSharper disable StringLiteralsWordIsNotInDictionary

        /// <summary>
        /// </summary>
        private const string C_DiagnosticsApplicationRunning =
            
            
            "Приложение проверки связи начинает работу" ;
        
        

        /// <summary>
        /// </summary>
        private const string C_DisableDiagnosticsAutorun =
            "Убрать Диагностику из Автозапуска ?" ;
         

        /// <summary>
        /// </summary>
        private const string C_ApplicationFilename = "RuskomDiagnostics" ;

        /// <summary>
        /// </summary>
        private const string C_ApplicationFileExtension = ".exe" ;

        // ReSharper restore StringLiteralsWordIsNotInDictionary

        /// <summary>
        /// </summary>
        private const string C_PostDiagnosticsResponceFormat =
            "Status=<{0}> , Message=<{1}>" ;

        /// <summary>
        /// </summary>
        private const string C_StartupRegistryKeyName =
            "RuskomDiagnosticsRunPath" ;

        /// <summary>
        /// </summary>
        private const string C_UserStartupRegistryPath =
            "SOFTWARE\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Run" ;

        /// <summary>
        /// </summary>
        private readonly RegistryKey _userStartUpRegistryKey ;

        /// <summary>
        /// </summary>
        private string ApplicationExecutablePath { get ; } = Application.ExecutablePath ;

        /// <summary>
        /// </summary>
        private string ApplicationName { get ; } = Program.ApplicationIdentifier ;

        /// <summary>
        /// </summary>
        private readonly string _applicationProductVersion =
            Application.ProductVersion ;

        /// <summary>
        /// </summary>
        private string StartupRegistryKeyName { get ; }

        /// <summary>
        /// </summary>
        private bool _initializeComplete ;

        /// <summary>
        /// </summary>
        private int _checkUpdateTimerCounter ;

        /// <summary>
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var myCp = base.CreateParams ;
                myCp.ClassStyle = myCp.ClassStyle | RuskomDiagnosticsForm.C_CpNoCloseButton ;
                return myCp ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public RuskomDiagnosticsForm ( )
        {
            if ( Settings.Default != null )
            {
                this._communicationPeriodMilliseconds =
                    ( Settings.Default.CommunicationPeriodMilliseconds > 0 )
                        ? Settings.Default.CommunicationPeriodMilliseconds
                        : RuskomDiagnosticsForm.C_CommunicationPeriodMilliseconds ;
                this.ShowBalloonTipWithShortMilliseconds =
                    ( Settings.Default.ShowBalloonTipWithShortMilliseconds > 0 )
                        ? Settings.Default.ShowBalloonTipWithShortMilliseconds
                        : RuskomDiagnosticsForm.C_ShowBalloonTipWithShortMilliseconds ;
                this.ShowBalloonTipWithGreatMilliseconds =
                    ( Settings.Default.ShowBalloonTipWithGreatMilliseconds > 0 )
                        ? Settings.Default.ShowBalloonTipWithGreatMilliseconds
                        : RuskomDiagnosticsForm.C_ShowBalloonTipWithGreatMilliseconds ;
                this.DayInMilliseconds = ( Settings.Default.DayInMilliseconds
                                           > 0 )
                                             ? Settings.Default
                                                       .DayInMilliseconds
                                             : RuskomDiagnosticsForm.C_DayInMilliseconds ;

                this.StartupRegistryKeyName =
                    Settings.Default.RuskomDiagnosticsRunPath
                    ?? RuskomDiagnosticsForm.C_StartupRegistryKeyName ;
                this.UserStartupRegistryPath =
                    Settings.Default.UserStartupRegistryPath
                    ?? RuskomDiagnosticsForm.C_UserStartupRegistryPath ;
                this.DiagnosticsApplicationRunning =
                    Settings.Default.DiagnosticsApplicationRunning
                    ?? RuskomDiagnosticsForm.C_DiagnosticsApplicationRunning ;
                this.ApplicationFilename =
                    Settings.Default.ApplicationFilename ??
                    RuskomDiagnosticsForm.C_ApplicationFilename ;
                this.ApplicationFileExtension =
                    Settings.Default.ApplicationFileExtension
                    ?? RuskomDiagnosticsForm.C_ApplicationFileExtension ;
                this.PostDiagnosticsResponceFormat =
                    Settings.Default.PostDiagnosticsResponceFormat
                    ?? RuskomDiagnosticsForm.C_PostDiagnosticsResponceFormat ;
                this.DisableDiagnosticsAutorun =
                    Settings.Default.DisableDiagnosticsAutorun ??
                    RuskomDiagnosticsForm.C_DisableDiagnosticsAutorun ;
            }
            else
            {
                this._communicationPeriodMilliseconds =
                    RuskomDiagnosticsForm.C_CommunicationPeriodMilliseconds ;
                this.ShowBalloonTipWithShortMilliseconds =
                    RuskomDiagnosticsForm.C_ShowBalloonTipWithShortMilliseconds ;
                this.ShowBalloonTipWithGreatMilliseconds =
                    RuskomDiagnosticsForm.C_ShowBalloonTipWithGreatMilliseconds ;
                this.DayInMilliseconds = RuskomDiagnosticsForm.C_DayInMilliseconds ;

                this.StartupRegistryKeyName =
                    RuskomDiagnosticsForm.C_StartupRegistryKeyName ;
                this.UserStartupRegistryPath = RuskomDiagnosticsForm.C_UserStartupRegistryPath ;
                this.DiagnosticsApplicationRunning =
                    RuskomDiagnosticsForm.C_DiagnosticsApplicationRunning ;
                this.ApplicationFilename = RuskomDiagnosticsForm.C_ApplicationFilename ;
                this.ApplicationFileExtension = RuskomDiagnosticsForm.C_ApplicationFileExtension ;
                this.PostDiagnosticsResponceFormat =
                    RuskomDiagnosticsForm.C_PostDiagnosticsResponceFormat ;
                this.DisableDiagnosticsAutorun = RuskomDiagnosticsForm.C_DisableDiagnosticsAutorun ;
            }

            //this.DayInMilliseconds = 1000*60*3;
            
            //this._communicationPeriodMilliseconds = 1000*60*2 ;

            if ( Registry.CurrentUser != null )
            {
                this._userStartUpRegistryKey =
                    Registry.CurrentUser.OpenSubKey
                        ( this.UserStartupRegistryPath , true ) ;
            }

            this.InitializeComponent( ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        private long DayInMilliseconds { get ; }

        /// <summary>
        /// 
        /// </summary>
        private string DisableDiagnosticsAutorun { get ; }

        /// <summary>
        /// 
        /// </summary>
        private string PostDiagnosticsResponceFormat { get ; }

        /// <summary>
        /// 
        /// </summary>
        private string ApplicationFileExtension { get ; }

        /// <summary>
        /// 
        /// </summary>
        private string ApplicationFilename { get ; }

        /// <summary>
        /// 
        /// </summary>
        private string DiagnosticsApplicationRunning { get ; }

        /// <summary>
        /// 
        /// </summary>
        private int ShowBalloonTipWithShortMilliseconds { get ; }

        /// <summary>
        /// 
        /// </summary>
        private string UserStartupRegistryPath { get ; }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RuskomDiagnosticsForm_Load
            (
            object sender ,
            EventArgs e )
        {
            var communicationTimer = this.CommunicationTimer ;
            communicationTimer ? .Stop( ) ;

            var updateTimer = this.UpdateTimer;
            updateTimer ? .Stop();

            this.WindowState = FormWindowState.Minimized ;
            this.ProcessWindowState( ) ;
            this.Refresh( ) ;

            var hostMenuNotifyIcon = this.HostMenuNotifyIcon ;
            if ( hostMenuNotifyIcon != null )
            {
                var diagnosticsApplicationRunning =
                    this.DiagnosticsApplicationRunning ;
                if ( diagnosticsApplicationRunning != null )
                {
                    hostMenuNotifyIcon.ShowBalloonTip
                        (
                         this.ShowBalloonTipWithShortMilliseconds ,
                         this.ApplicationName ,
                         diagnosticsApplicationRunning ,
                         ToolTipIcon.Info ) ;
                }

                try
                {
                    this.Post( ) ;
                }
                catch ( Exception )
                {
                    // ignored
                }

                if ( communicationTimer != null )
                {
                    communicationTimer.Interval =
                        this._communicationPeriodMilliseconds ;
                    communicationTimer.Enabled = true ;
                    communicationTimer.Start( ) ;
                }
                if (updateTimer != null)
                {
                    updateTimer.Interval =
                        this._communicationPeriodMilliseconds;
                    updateTimer.Enabled = true;
                    updateTimer.Start();
                }

                this.WindowState = FormWindowState.Normal ;
                this.ProcessWindowState( ) ;

                try
                {
                    var registryKeyName = this.StartupRegistryKeyName ;
                    if ( registryKeyName != null )
                    {
                        this.CheckUpdate
                            (
                                true ,
                                registryKeyName ) ;
                    }
                }
                    // ReSharper disable EmptyGeneralCatchClause
                catch ( Exception )
                    // ReSharper restore EmptyGeneralCatchClause
                {
                }

                var toolStripMenuItem = this.autorunToolStripMenuItem ;
                var userStartUpRunApplicationRegistryKey =
                    this._userStartUpRegistryKey ;
                var startupRegistryKeyName = this.StartupRegistryKeyName ;
                if ( toolStripMenuItem != null
                     && userStartUpRunApplicationRegistryKey != null
                     && startupRegistryKeyName != null )
                {
                    var applicationsExecutablePath = this.ApplicationExecutablePath ;
                    var applicationName = this.ApplicationName;
                    if ( applicationsExecutablePath != null
                        && (applicationName != null))
                    {
                        Handler.ProcessAutoRunOption
                            (
                                toolStripMenuItem ,
                                userStartUpRunApplicationRegistryKey ,
                                applicationsExecutablePath ,
                                applicationName ,
                                startupRegistryKeyName
                            ) ;
                    }
                }

                this.WindowState = FormWindowState.Minimized ;
                this.ProcessWindowState( ) ;
                this.Refresh( ) ;

                if ( hostMenuNotifyIcon.Text != null )
                {
                    hostMenuNotifyIcon.ShowBalloonTip
                        (
                         this.ShowBalloonTipWithGreatMilliseconds ,
                         this.ApplicationName ,
                         hostMenuNotifyIcon.Text ,
                         ToolTipIcon.Info ) ;
                }
                this._initializeComplete = true ;
                //this._checkUpdateTimerCounter = 1000;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private int ShowBalloonTipWithGreatMilliseconds { get ; }

        /// <summary>
        /// </summary>
        private void Post ( )
        {
            Handler.PerformDiagnostic( ) ;
            var balanceString = Handler.GetBalance( ) ;

            if ( ! string.IsNullOrEmpty ( balanceString ) )
            {
                var hostMenuNotifyIcon = this.HostMenuNotifyIcon ;
                var applicationName = this.ApplicationName;
                if ( hostMenuNotifyIcon != null 
                    && (applicationName != null))
                {
                    Handler.SetBalanceMessage
                        (
                            hostMenuNotifyIcon ,
                            balanceString ,
                            this.ShowBalloonTipWithShortMilliseconds ,
                            applicationName
                        ) ;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="isSilent"></param>
        /// <param name="startupRegistryKeyName"></param>
        /// <returns></returns>
        private bool CheckUpdate
            (
            bool isSilent ,
            string startupRegistryKeyName
            )
        {
            var isUpdateChecked = Handler.CheckUpdate
                (
                 this.ApplicationName,
                 Program.ApplicationStartupPath,
                 this._userStartUpRegistryKey ,
                 this.autorunToolStripMenuItem ,
                 this.ApplicationFilename ,
                 this.ApplicationFileExtension ,
                 isSilent ,
                 this._applicationProductVersion ,
                 startupRegistryKeyName,
                 Handler.UpdateStorageLocation);

            return isUpdateChecked ;
        }

        /// <summary>
        /// </summary>
        private void ProcessWindowState ( )
        {
            switch ( this.WindowState )
            {
                case FormWindowState.Maximized :
                    this.ShowInTaskbar = true ;
                    this.Visible = true ;
                    break ;
                case FormWindowState.Minimized :
                    this.ShowInTaskbar = false ;
                    this.Hide( ) ;
                    break ;
                case FormWindowState.Normal :
                    this.ShowInTaskbar = true ;
                    this.Visible = true ;
                    break ;
                default :
                    this.ShowInTaskbar = false ;
                    this.Hide( ) ;
                    break ;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // ReSharper disable IdentifierWordIsNotInDictionary
        // ReSharper disable IdentifierWordIsNotInDictionary
        private void ВыходToolStripMenuItemClick ( object sender , EventArgs e )
            // ReSharper restore IdentifierWordIsNotInDictionary
            // ReSharper restore IdentifierWordIsNotInDictionary
        {
            this.CloseApplication( ) ;
        }

        /// <summary>
        /// </summary>
        private void CloseApplication ( )
        {
            Handler.CloseForm ( this ) ;
            Application.Exit( ) ;
            Environment.Exit
                (
                    Program.NormalExitCode);
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void requisitesToolStripMenuItem_Click
            ( object sender , EventArgs e )
        {
            this.ShowRequisites( ) ;
        }

        /// <summary>
        /// </summary>
        private void ShowRequisites ( )
        {
            if ( this._initializeComplete )
            {
                var showRequisitesForm = new ShowNetworkRequisitesForm( ) ;
                showRequisitesForm.ShowDialog ( this ) ;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pingToolStripMenuItem1_Click
            ( object sender , EventArgs e )
        {
            this.TestConnection( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoShowCurrentSpeedButton_Click
            ( object sender , EventArgs e )
        {
            this.PerformSpeedTest( ) ;
        }

        /// <summary>
        /// </summary>
        private void PerformSpeedTest ( )
        {
            if ( this._initializeComplete )
            {
                var speedTestForm = new SpeedTestForm( ) ;
                speedTestForm.ShowDialog ( this ) ;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoShowRequisitesButton_Click
            ( object sender , EventArgs e )
        {
            this.ShowRequisites( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoTestConnectionButton_Click
            ( object sender , EventArgs e )
        {
            this.TestConnection( ) ;
        }

        /// <summary>
        /// </summary>
        private void TestConnection ( )
        {
            if ( this._initializeComplete )
            {
                var testConnectionForm = new TestConnectionForm( ) ;
                testConnectionForm.ShowDialog ( this ) ;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doSendDiagnosticInfoButton_Click
            ( object sender , EventArgs e )
        {
            this.SendDiagnostic( ) ;
        }

        /// <summary>
        /// </summary>
        private void SendDiagnostic ( )
        {
            if ( this._initializeComplete )
            {
                var responce = Handler.PerformDiagnostic( ) ;

                var postDiagnosticsResponceFormat =
                    this.PostDiagnosticsResponceFormat ;
                if ( responce != null
                     && postDiagnosticsResponceFormat != null )
                {
                    var serverResponce = string.Format
                        (
                         postDiagnosticsResponceFormat ,
                         responce.Status , responce.Message ) ;

                    MessageBox.Show ( serverResponce ) ;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommunicationTimer_Tick ( object sender , EventArgs e )
        {
                this.Post( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoCloseApplicationButton_Click
            ( object sender , EventArgs e )
        {
            this.CloseApplication( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RuskomDiagnosticsForm_Resize
            ( object sender , EventArgs e )
        {
            this.ProcessWindowState( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void autorunToolStripMenuItem_Click
            ( object sender , EventArgs e )
        {
            this.ProcessAutoRunOption( ) ;
        }

        /// <summary>
        /// </summary>
        private void ProcessAutoRunOption ( )
        {
            var userStartUpRunApplicationRegistryKey =
                this._userStartUpRegistryKey ;
            var applicationExecutablePath = this.ApplicationExecutablePath ;
            var startupRegistryKeyName = this.StartupRegistryKeyName ;
            var toolStripMenuItem = this.autorunToolStripMenuItem ;
            var isMenuItemChecked = toolStripMenuItem != null
                                    && toolStripMenuItem.Checked ;

            if ( isMenuItemChecked )
            {
                if ( userStartUpRunApplicationRegistryKey != null
                     && ( startupRegistryKeyName != null ) )
                {
                    Handler.ProcessAutoRunOption
                        (
                         toolStripMenuItem ,
                         userStartUpRunApplicationRegistryKey
                         , this.ApplicationExecutablePath
                         , this.ApplicationName ,
                         startupRegistryKeyName ) ;
                }
            }
            else
            {
                var userDecision = MessageBox.Show
                    (
                     this.DisableDiagnosticsAutorun , this.ApplicationName ,
                     MessageBoxButtons.YesNo , MessageBoxIcon.Question ) ;
                if ( userDecision == DialogResult.Yes )
                {
                    if ( userStartUpRunApplicationRegistryKey != null
                         && applicationExecutablePath != null
                         && startupRegistryKeyName != null )
                    {
                        Handler.ProcessAutorun
                            (
                             Handler.C_DisableAutorun ,
                             userStartUpRunApplicationRegistryKey ,
                             applicationExecutablePath ,
                             startupRegistryKeyName ) ;
                    }
                }
                else
                {
                    if ( toolStripMenuItem != null )
                    {
                        toolStripMenuItem.Checked = true ;
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckUpdateButton_Click ( object sender , EventArgs e )
        {
            this.CheckUpdate ( false , this.StartupRegistryKeyName ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void проверитьОбновлениеToolStripMenuItem_Click
            ( object sender , EventArgs e )
        {
            this.CheckUpdate ( false , this.StartupRegistryKeyName ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTracerButton_Click ( object sender , EventArgs e )
        {
            this.ShowTraceToHostForm( ) ;
        }

        /// <summary>
        /// </summary>
        private void ShowTraceToHostForm ( )
        {
            if ( this._initializeComplete )
            {
                using ( var traceHostForm = new TraceHostForm( ) )
                {
                    traceHostForm.ShowDialog ( this ) ;
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // ReSharper disable IdentifierWordIsNotInDictionary
        // ReSharper disable IdentifierWordIsNotInDictionary
        private void проверитьМаршрутToolStripMenuItem1_Click
            // ReSharper restore IdentifierWordIsNotInDictionary
            // ReSharper restore IdentifierWordIsNotInDictionary
            ( object sender , EventArgs e )
        {
            this.ShowTraceToHostForm( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HostMenuNotifyIcon_DoubleClick
            ( object sender , EventArgs e )
        {
            //if (InitializeComplete)
            //{
            //    Visible = true;
            //    WindowState = FormWindowState.Normal;
            //    ProcessWindowState();
            //}
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // ReSharper disable IdentifierWordIsNotInDictionary
        // ReSharper disable IdentifierWordIsNotInDictionary
        private void балансToolStripMenuItem_Click
            // ReSharper restore IdentifierWordIsNotInDictionary
            // ReSharper restore IdentifierWordIsNotInDictionary
            ( object sender , EventArgs e )
        {
            this.ShowBalance( ) ;
        }

        /// <summary>
        /// </summary>
        private void ShowBalance ( )
        {
            if ( this._initializeComplete )
            {
                Handler.ShowBalance
                    ( this.HostMenuNotifyIcon , this , this.ApplicationName ) ;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // ReSharper disable IdentifierWordIsNotInDictionary
        // ReSharper disable IdentifierWordIsNotInDictionary
        private void оплатитьToolStripMenuItem_Click
            // ReSharper restore IdentifierWordIsNotInDictionary
            // ReSharper restore IdentifierWordIsNotInDictionary
            ( object sender , EventArgs e )
        {
            this.OpenPayWebPage( ) ;
        }

        /// <summary>
        /// </summary>
        private void OpenPayWebPage ( )
        {
            if ( this._initializeComplete )
            {
                Handler.OpenPayWebPage( ) ;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoViewBalanceButton_Click ( object sender , EventArgs e )
        {
            this.ShowBalance( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoOpenPayWebPageButton_Click
            ( object sender , EventArgs e )
        {
            this.OpenPayWebPage( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // ReSharper disable IdentifierWordIsNotInDictionary
        private void проверитьСкоростиToolStripMenuItem_Click
            // ReSharper restore IdentifierWordIsNotInDictionary
            ( object sender , EventArgs e )
        {
            this.PerformSpeedTest( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RuskomDiagnosticsForm_FormClosing
            ( object sender , FormClosingEventArgs e )
        {
            var hostMenuNotifyIcon = this.HostMenuNotifyIcon ;
            if ( hostMenuNotifyIcon != null
                 && Settings.Default != null )
            {
                Settings.Default.BalanceString = hostMenuNotifyIcon.Text ;
                Settings.Default.Save( ) ;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            this._checkUpdateTimerCounter++;
            var updateTimer = this.UpdateTimer;
            if (updateTimer != null)
            {
                updateTimer.Stop();
            
                var elapsedTime = this._checkUpdateTimerCounter
                                  * this._communicationPeriodMilliseconds;
                if (elapsedTime > this.DayInMilliseconds)
                {
                    var isUpdateChecked = this.CheckUpdate
                        (
                            isSilent: true,
                            startupRegistryKeyName: this.StartupRegistryKeyName);

                    if (isUpdateChecked)
                    {
                        this._checkUpdateTimerCounter = 0;
                    }
                }
                updateTimer.Start();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void контактыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowContacts();
        }

        /// <summary>
        /// </summary>
        private void ShowContacts()
        {
            if (this._initializeComplete)
            {
                Handler.ShowContacts
                    (
                        this);
            }
        }
    }
}
