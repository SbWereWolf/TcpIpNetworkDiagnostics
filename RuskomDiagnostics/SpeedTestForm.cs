using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using RuskomDiagnostics.Annotations;

namespace RuskomDiagnostics
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SpeedTestForm : Form
    {
        /// <summary>
        /// </summary>
        private const string C_IperfWithSerialTestArguments = "-y C -c 89.106.248.22";

        /// <summary>
        /// </summary>
        private const string C_IperfWithParallelTestArguments = "-c 89.106.248.22 -y C -P 5 ";
        
        /// <summary>
        /// </summary>
        private bool _resultShown ;

        /// <summary>
        /// </summary>
        private SpeedTestInput.TestType SpeedTestType { get ; }

        /// <summary>
        /// 
        /// </summary>
        private ProcessExecuteParameters SpeedTestProgram { get ; }

        /// <summary>
        /// </summary>
        private static string SpeedTestProgramArguments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private delegate void TestingFinishDelegate ( ) ;


        /// <summary>
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="setting"></param>
        /// <param name="constant"></param>
        /// <returns></returns>
        [NotNull]
        private static string InitializeWithString
            (
            [CanBeNull]  Properties.Settings configuration,
            [CanBeNull] string setting,
            [CanBeNull] string constant)
        {
            string result;
            if (configuration != null)
            {
                result = setting ?? constant;
            }
            else
            {
                result = constant;
            }
            result = result ?? string.Empty;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private void TestingFinish ( )
        {
            var startButton = this.DoTestSpeedButton ;
            var stopButton = this.DoCloseButton ;
            var progressBar = this.TestingProcessProgressBar ;

            Handler.EnableStartProgressStopControls
                (
                 startButton ,
                 stopButton ,
                 progressBar
                ) ;
        }

        /// <summary>
        /// </summary>
        private void WatchDog ( )
        {
            var enableCommand = new TestingFinishDelegate ( this.TestingFinish ) ;
            this.BeginInvoke ( enableCommand ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        public SpeedTestForm 
            (
            [ CanBeNull ] string speedTestExecutable ,
            [ CanBeNull ] List<string> speedTestProgrammFilesList
            )
        {
            this.InitializeComponent( ) ;
            this.OnExecutableFinish += this.WatchDog ;

            var settings = Properties.Settings.Default ;

            if ( settings != null )
            {

                this.SpeedTestType = settings.UseSerialSpeedTestType
                            ? SpeedTestInput.TestType.Serial
                            : SpeedTestInput.TestType.Parallel;

                switch ( this.SpeedTestType )
                {
                    case SpeedTestInput.TestType.Parallel :

                        SpeedTestForm.SpeedTestProgramArguments =
                            SpeedTestForm.InitializeWithString
                                (
                                    settings ,
                                    settings.SpeedTestProgramParallelTypeArguments ,
                                    SpeedTestForm.C_IperfWithParallelTestArguments
                                ) ;
                        break ;
                    case SpeedTestInput.TestType.Serial :
                        SpeedTestForm.SpeedTestProgramArguments =
                            SpeedTestForm.InitializeWithString
                                (
                                    settings ,
                                    settings.SpeedTestProgramSerialTypeArguments ,
                                    SpeedTestForm.C_IperfWithSerialTestArguments
                                ) ;

                        break ;
                    default :
                        goto case SpeedTestInput.TestType.Parallel ;
                }
            }

            this.SpeedTestProgram = new ProcessExecuteParameters
                (
                speedTestExecutable,
                SpeedTestForm.SpeedTestProgramArguments,
                speedTestProgrammFilesList
                );
        }

        /// <summary>
        /// </summary>
        /// <param name="outputControl"></param>
        /// <param name="textValue"></param>
        private delegate void SetControlTextDelegate (
            Control outputControl , string textValue ) ;

        /// <summary>
        /// </summary>
        /// <param name="textControl"></param>
        /// <param name="someString"></param>
        private static void SetControlText
            (
            [ CanBeNull ] Control textControl ,
            string someString )
        {
            if ( textControl != null )
            {
                textControl.Text = someString ;
                textControl.Refresh( ) ;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoCopyResultButton_Click
            (
            object sender ,
            EventArgs e )
        {
            this.CopyResult( ) ;
        }

        // ReSharper disable UnusedMethodReturnValue.Local

        /// <summary>
        /// </summary>
        /// <returns></returns>
        [ CanBeNull ]
        private string CopyResult ( )
            // ReSharper restore UnusedMethodReturnValue.Local
        {
            var overallString = string.Empty ;
            var speedTestResultTextBox = this.SpeedTestResulTextBox ;
            if ( speedTestResultTextBox != null )
            {
                overallString = speedTestResultTextBox.Text ;
                if ( overallString != null )
                {
                    Clipboard.SetText ( overallString ) ;
                }
            }
            return overallString ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoCloseButton_Click
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
        private void SpeedTestForm_Load
            (
            object sender ,
            EventArgs e )
        {
            this._resultShown = false ;
            Handler.SetFormPositionsOnScreenCenter ( this ) ;
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpeedTestForm_Shown
            (
            object sender ,
            EventArgs e )
        {
            if ( this._resultShown )
            {
                return ;
            }
            this._resultShown = true ;
            SpeedTestForm.ViewSpeedTestResult
                (
                 this.SpeedTestResulTextBox ,
                 this.DoTestSpeedButton ,
                 this.DoCloseButton ,
                 this.TestingProcessProgressBar ,
                 this
                ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        public event TestConnectionForm.MethodContainer OnExecutableFinish ;

        /// <summary>
        /// </summary>
        /// <param name="speedTestResultTextBox"></param>
        /// <param name="doTestSpeedButton"></param>
        /// <param name="doCloseButton"></param>
        /// <param name="testingProcessProgressBar"></param>
        /// <param name="outputForm"></param>
        private static void ViewSpeedTestResult
            (
            [ CanBeNull ] Control speedTestResultTextBox ,
            [ CanBeNull ] Button doTestSpeedButton ,
            [ CanBeNull ] Button doCloseButton ,
            [ CanBeNull ] ProgressBar testingProcessProgressBar ,
            [ CanBeNull ] SpeedTestForm outputForm
            )
        {
            if ( ( outputForm != null )
                 && ( speedTestResultTextBox != null ) )
            {
                speedTestResultTextBox.Text = Handler.TestingNetworkSpeed;
                speedTestResultTextBox.Refresh( ) ;

                var startControl = doTestSpeedButton ;
                var stopControl = doCloseButton ;
                var progressBar = testingProcessProgressBar ;

                Handler.DisableStarProgressStopControls
                    (
                     startControl
                     , stopControl
                     , progressBar
                    ) ;

                var networkAdaptersParameters =
                    Handler.GetNetworkAdaptersParameters( ) ;
                {
                    var interfacesNumber = networkAdaptersParameters.Count ;

                    if ( interfacesNumber <= 0 )
                    {
                        speedTestResultTextBox.Text =
                            Handler.DisconnectOrInvalidConnection ;
                        var onOnExecutableFinish = outputForm.OnExecutableFinish ;
                        onOnExecutableFinish?.Invoke( ) ;
                    }
                    else
                    {
                        var progressControl = testingProcessProgressBar ;
                        if ( progressControl != null )
                        {
                            progressControl.Value = progressControl.Minimum ;
                        }

                        var testParameters = new SpeedTestsInput
                            (
                            speedTestResultTextBox ,
                            outputForm ) ;

                        testParameters.MethodsInputs?.Add
                            (
                                new SpeedTestInput
                                {
                                    Program =
                                        outputForm.SpeedTestProgram,
                                    SpeedTestType = outputForm.SpeedTestType
                                } ) ;

                        var testConnectionThread = new Thread ( SpeedTestForm.GetSpeedTestResult ) ;
                        testConnectionThread.Start ( testParameters ) ;
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="speedTestInput"></param>
        private static void GetSpeedTestResult (
            [ CanBeNull ] object speedTestInput )
        {
            var speedTestsInput = ( SpeedTestsInput ) speedTestInput ;
            if ( speedTestsInput != null )
            {
                var testsResults = Handler.PerformSpeedTest
                    ( speedTestsInput ) ;

                var testsResult = string.Empty ;

                foreach ( var testResult in testsResults )
                {
                    var statusCaption = string.Empty ;
                    if ( testResult != null )
                    {
                        switch ( testResult.TestStatus )
                        {
                            case TaskStatus.ActionStatus.Error :
                                statusCaption =
                                    Handler.SpeedTestError ;
                                break ;
                            case TaskStatus.ActionStatus.Fail :
                                statusCaption =
                                    Handler.SpeedTestFail ;
                                break ;
                            case TaskStatus.ActionStatus.Ok :
                                statusCaption = string.Empty ;
                                break ;
                            case TaskStatus.ActionStatus.Unknown :
                                statusCaption =
                                    Handler
                                        .DisconnectOrInvalidConnection ;
                                break ;
                        }

                        var testResultAsString =
                            $"{statusCaption}{testResult.ToColumn( )}" ;

                        testsResult =
                            $"{testsResult}{testResultAsString}{Environment.NewLine}" ;
                    }
                }

                var setControlText = new SetControlTextDelegate
                    ( SpeedTestForm.SetControlText ) ;

                var textControl = speedTestsInput.ResultOutputControl ;
                textControl?.BeginInvoke
                    (
                        setControlText ,
                        textControl ,
                        testsResult ) ;
            }

            var onExecutableFinish = speedTestsInput?.SpeedTestForm?.OnExecutableFinish;
            onExecutableFinish?.Invoke();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoTestSpeedButton_Click
            (
            object sender ,
            EventArgs e )
        {
            SpeedTestForm.ViewSpeedTestResult
                (
                 this.SpeedTestResulTextBox ,
                 this.DoTestSpeedButton ,
                 this.DoCloseButton ,
                 this.TestingProcessProgressBar ,
                 this
                ) ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class SpeedTestsInput
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly List < SpeedTestInput > MethodsInputs ;

        /// <summary>
        /// 
        /// </summary>
        public readonly Control ResultOutputControl ;

        /// <summary>
        /// 
        /// </summary>
        public readonly SpeedTestForm SpeedTestForm ;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultOutputControl"></param>
        /// <param name="speedTestForm"></param>
        public SpeedTestsInput
            (
            Control resultOutputControl ,
            SpeedTestForm speedTestForm )
        {
            this.MethodsInputs = new List < SpeedTestInput >( ) ;
            this.ResultOutputControl = resultOutputControl ;
            this.SpeedTestForm = speedTestForm ;
        }
    }

}
