using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using RuskomDiagnostics.Annotations;

namespace RuskomDiagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class TestConnectionForm : Form
    {
        /// <summary>
        /// </summary>
        private bool FormShown { get ; }

        /// <summary>
        /// </summary>
        private List < Control > OutputsControls { get ; }

        /// <summary>
        /// 
        /// </summary>
        public TestConnectionForm ( )
        {
            this.InitializeComponent( ) ;
            this.OnExecutableFinish += this.WatchDog ;

            this.FormShown = false ;

            this.OutputsControls = new List < Control >
                                    {
                                        this.PingGatewayTextBox ,
                                        this.PingDnsTextBox ,
                                        this.PingHomeTextBox ,
                                        this.PingCityTextBox ,
                                        this.PingCountryTextBox ,
                                        this.TraceHomeTextBox ,
                                        this.TraceCityTextBox ,
                                        this.TraceCountryTextBox
                                    } ;


        }

        /// <summary>
        /// </summary>
        private long _completedTestsCount ;

        /// <summary>
        /// 
        /// </summary>
        private delegate void TestingFinishDelegate ( ) ;

        /// <summary>
        /// 
        /// </summary>
        private void TestingFinish ( )
        {
            var startButton = this.DoTestConnectionButton ;
            var stopButton = this.DoCloseFormButton ;
            var progressBar = this.TestingProgressBar ;

            Handler.EnableStartProgressStopControls
                (
                 startButton ,
                 stopButton ,
                 progressBar
                ) ;

            if (
                ( Handler.ConnectionLoses != null )
                && ( Handler.ConnectionFineQuality != null )
                )
            {
                var testParameters = this.InitializeTestParameters
                    (
                     null
                     , null
                     , null
                    );

                var mayReportBadQuality = false;

                if (testParameters.ProgramsWithOutput != null)
                {
                    foreach (var programsComponent in testParameters.ProgramsWithOutput)
                    {
                        if (
                            programsComponent?.TextOutputControl?.Text != null )
                        {
                            mayReportBadQuality =
                                programsComponent.TextOutputControl.Text
                                                 .Contains
                                    (Handler.ConnectionLoses)
                                && ! programsComponent.TextOutputControl.Text
                                                      .Contains
                                         (Handler.ConnectionFineQuality)
                                ;
                        }

                        if (mayReportBadQuality)
                        {
                            break;
                        }
                    }
                }

                var doReportProblemButton = this.DoReportProblemButton ;
                if ( doReportProblemButton != null )
                {
                    doReportProblemButton.Visible = mayReportBadQuality;
                }
            }


        }

        /// <summary>
        /// 
        /// </summary>
        private readonly object _isTestCompleted = new object( ) ;

        /// <summary>
        /// </summary>
        private void WatchDog ( )
        {
            var isTestCompleted = this._isTestCompleted ;
            if ( isTestCompleted != null )
            {
                lock ( isTestCompleted )
                {
                    this._completedTestsCount++ ;
                    if ( this._completedTestsCount >= 8 )
                    {
                        var enableCommand = new TestingFinishDelegate
                            (this.TestingFinish);
                        this.BeginInvoke(enableCommand);
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestConnectionForm_Load
            (
            object sender ,
            EventArgs e )
        {
        }

        /// <summary>
        /// </summary>
        private void TestConnection ( )
        {
            var startControl = this.DoTestConnectionButton ;
            var stopControl = this.DoCloseFormButton ;
            var progressBar = this.TestingProgressBar ;

            Handler.DisableStarProgressStopControls
                (
                 startControl
                 , stopControl
                 , progressBar
                ) ;

            this._completedTestsCount = 0 ;

            var networkAdaptersParameters =
                Handler.GetNetworkAdaptersParameters( ) ;
            {
                var interfacesNumber = networkAdaptersParameters.Count ;

                if ( interfacesNumber <= 0 )
                {
                    if ( Handler.DisconnectOrInvalidConnection != null )
                    {
                        TestConnectionForm.SetTextboxesText
                            (
                                Handler.DisconnectOrInvalidConnection
                                , this.OutputsControls
                            ) ;
                    }
                    this._completedTestsCount = 8 ;
                    var onExecutableFinish = this.OnExecutableFinish ;
                    onExecutableFinish?.Invoke( ) ;
                }
                else
                {
                    var networkAdapterParameters =
                        networkAdaptersParameters.FirstOrDefault( )
                        ?? new NetworkAdapterParameters( ) ;
                    var gatewayAddress = networkAdapterParameters.Gateway ;
                    var dnsAddress = networkAdapterParameters.Dns ;

                    var progressControl = this.TestingProgressBar ;

                    if ( progressControl != null )
                    {
                        progressControl.Value = progressControl.Minimum ;

                        var testParameters = this.InitializeTestParameters
                            (
                             progressControl
                             , gatewayAddress
                             , dnsAddress
                            ) ;

                        var testConnectionThread = new Thread
                            ( this.PerformTestConnection ) ;
                        testConnectionThread.Start ( testParameters ) ;
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="progressControl"></param>
        /// <param name="gatewayAddress"></param>
        /// <param name="dnsAddress"></param>
        /// <returns></returns>
        [ NotNull ]
        private BatchWithProgressbar InitializeTestParameters
            (
            [ CanBeNull ] ProgressBar progressControl ,
            [ CanBeNull ] string gatewayAddress ,
            [ CanBeNull ] string dnsAddress )
        {
            var testParameters = new BatchWithProgressbar
                ( progressControl ) ;

            var pingGateway =
                new ProgramWithOutput
                {
                    ArgumentsString = gatewayAddress ,
                    TextOutputControl = this.PingGatewayTextBox ,
                    Program = Handler.PingProgram
                } ;
            var pingDns =
                new ProgramWithOutput
                {
                    ArgumentsString = dnsAddress ,
                    TextOutputControl = this.PingDnsTextBox ,
                    Program = Handler.PingProgram
                } ;
            var pingHome =
                new ProgramWithOutput
                {
                    ArgumentsString = Handler.C_HomeNetworkHost ,
                    TextOutputControl = this.PingHomeTextBox ,
                    Program = Handler.PingProgram
                } ;
            var pingCity =
                new ProgramWithOutput
                {
                    ArgumentsString = Handler.C_CityNetworkHost ,
                    TextOutputControl = this.PingCityTextBox ,
                    Program = Handler.PingProgram
                } ;
            var pingCountry =
                new ProgramWithOutput
                {
                    ArgumentsString = Handler.C_CountryNetworkHost ,
                    TextOutputControl =
                        this.PingCountryTextBox ,
                    Program = Handler.PingProgram
                } ;

            var tracertHome =
                new ProgramWithOutput
                {
                    ArgumentsString = Handler.C_HomeNetworkHost ,
                    TextOutputControl = this.TraceHomeTextBox ,
                    Program = Handler.TracertProgram
                } ;
            var tracertCity =
                new ProgramWithOutput
                {
                    ArgumentsString = Handler.C_CityNetworkHost ,
                    TextOutputControl = this.TraceCityTextBox ,
                    Program = Handler.TracertProgram
                } ;
            var tracertCountry =
                new ProgramWithOutput
                {
                    ArgumentsString = Handler.C_CountryNetworkHost ,
                    TextOutputControl = this.TraceCountryTextBox ,
                    Program = Handler.TracertProgram
                } ;

            if ( testParameters.ProgramsWithOutput != null )
            {
                testParameters.ProgramsWithOutput.Add ( pingGateway ) ;
                testParameters.ProgramsWithOutput.Add ( pingDns ) ;
                testParameters.ProgramsWithOutput.Add ( pingHome ) ;
                testParameters.ProgramsWithOutput.Add ( pingCity ) ;
                testParameters.ProgramsWithOutput.Add ( pingCountry ) ;
                testParameters.ProgramsWithOutput.Add ( tracertHome ) ;
                testParameters.ProgramsWithOutput.Add ( tracertCity ) ;
                testParameters.ProgramsWithOutput.Add ( tracertCountry ) ;
            }
            return testParameters ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textValue"></param>
        private delegate void SetTextBoxValueDelegate (
            TextBoxBase outputControl, string textValue);

        /// <summary>
        /// </summary>
        /// <param name="textOutputControl"></param>
        /// <param name="someString"></param>
        private static void SetControlText
            (
            [ CanBeNull ] TextBoxBase textOutputControl,
            string someString)
        {
            if ( textOutputControl != null )
            {
                textOutputControl.Text = someString ;
                textOutputControl.SelectionStart = textOutputControl.TextLength;
                textOutputControl.ScrollToCaret();

                textOutputControl.Refresh( ) ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetProgramOutputToControl
            (
            [ CanBeNull ] object input
            )
        {
            var programOutputToControl = ( ProgramWithOutput ) input ;

            if ( programOutputToControl != null )
            {
                var program = programOutputToControl.Program ;
                var hostName = programOutputToControl.ArgumentsString ;
                var textControl = programOutputToControl.TextOutputControl ;

                if ( hostName != null )
                {
                    var programOutput = Handler.ExecuteProgramWithArguments
                        (
                            program ,
                            hostName
                        ) ;
                    if ( ( program != null )
                         && ( Handler.PingProgram != null ) )
                    {
                        if ( program.ExecutableFile
                             == Handler.PingProgram.ExecutableFile )
                        {
                            var connectionsFineQualitySymptom =
                                Handler.ConnectionsFineQualitySymptom ;
                            if ( connectionsFineQualitySymptom != null )
                            {
                                {
                                    var substringExists = programOutput.IndexOf
                                        (
                                            connectionsFineQualitySymptom ,
                                            StringComparison.Ordinal ) ;

                                    programOutput +=
                                        (
                                            substringExists > 0
                                                ? $"{Handler.ConnectionFineQuality}{Environment.NewLine}"
                                                : $"{Handler.ConnectionLoses}{Environment.NewLine}" ) ;
                                }
                            }
                        }
                    }

                    var setControlText = new SetTextBoxValueDelegate
                        ( TestConnectionForm.SetControlText ) ;
                    this.BeginInvoke
                        (
                            setControlText ,
                            textControl ,
                            programOutput ) ;
                }
            }

            var onExecutableFinish = this.OnExecutableFinish ;
            onExecutableFinish?.Invoke( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="outputControlsObject"></param>
        private void PerformTestConnection (
            [ CanBeNull ] object outputControlsObject )
        {
            var parameters = ( BatchWithProgressbar ) outputControlsObject ;

            if ( parameters?.ProgramsWithOutput != null )
            {
                var textBoxes = parameters.ProgramsWithOutput
                                          .Where
                    (
                        methodParameters => methodParameters != null)
                                          .Where
                    (
                        methodParameters =>
                        methodParameters.TextOutputControl != null)
                                          .Select
                    (
                        methodParameters =>
                        methodParameters.TextOutputControl)
                                          .ToList();

                if ( Handler.WaitWhileTestProcessed != null )
                {
                    TestConnectionForm.SetTextboxesText
                        (
                            Handler.WaitWhileTestProcessed ?? string.Empty,
                            textBoxes
                        ) ;
                }
            }

            if ( parameters?.ProgramsWithOutput != null )
            {
                foreach (
                    var methodParameters in parameters.ProgramsWithOutput )
                {
                    var thread = new Thread
                        ( this.SetProgramOutputToControl ) ;
                    thread.Start ( methodParameters ) ;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public delegate void MethodContainer ( ) ;

        /// <summary>
        /// 
        /// </summary>
        public event MethodContainer OnExecutableFinish ;

        /// <summary>
        /// </summary>
        /// <param name="setValue"></param>
        /// <param name="textBoxes"></param>
        private static void SetTextboxesText
            (
            string setValue , [ CanBeNull ] IEnumerable < Control > textBoxes )
        {
            if ( textBoxes != null )
            {
                foreach ( var textBox in textBoxes )
                {
                    var setText = new SetTextBoxValueDelegate ( TestConnectionForm.SetControlText ) ;
                    var box = textBox ;
                    box?.BeginInvoke
                        (
                            setText ,
                            box ,
                            setValue ) ;
                }
            }
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
        private void DoCopyLog_Click
            (
            object sender ,
            EventArgs e )
        {
            var outputsControls = this.OutputsControls ;
            var outputText = outputsControls?.Where
                ( outputsControl => outputsControl != null )
                                             .Aggregate
                (
                    string.Empty ,
                    ( current ,
                      outputsControl ) =>
                    $"{current}{Environment.NewLine}{outputsControl.Text}"
                ) ;
            if ( outputText != null )
            {
                Clipboard.SetText ( outputText ) ;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TestConnectionForm_Shown
            (
            object sender ,
            EventArgs e )
        {
            if ( this.FormShown )
            {
                return ;
            }
            this.WindowState = FormWindowState.Maximized ;
            this.Refresh( ) ;
            this.TestConnection( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoTestConnectionButton_Click
            (
            object sender ,
            EventArgs e )
        {
            this.TestConnection( ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoReportProblemButton_Click(object sender, EventArgs e)
        {

        }
    }
}
