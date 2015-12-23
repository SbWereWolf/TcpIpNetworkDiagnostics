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
    public sealed partial class ShowNetworkRequisitesForm : Form
    {
        /// <summary>
        /// </summary>
        private bool _formShown;

        /// <summary>
        /// </summary>
        private readonly List<Control> _outputsControls;

        /// <summary>
        /// </summary>
        

        /// <summary>
        /// 
        /// </summary>
        public ShowNetworkRequisitesForm()
        {
            this.InitializeComponent();
            this.OnBatchFinish += this.WatchDog;
            this._outputsControls =
                new List<Control>
                {
                    this.RequisitesTextBox
                };
        }

        /// <summary>
        /// </summary>
        private long _completedTestsCount;

        /// <summary>
        /// 
        /// </summary>
        private delegate void TestingFinishDelegate();

        /// <summary>
        /// 
        /// </summary>
        private void TestingFinish()
        {
            var startButton = this.DoShowRequisitesButton;
            var stopButton = this.DoCloseFormButton;
            var progressBar = this.BatchProgressBar;

            Handler.EnableStartProgressStopControls
                (
                    startButton,
                    stopButton,
                    progressBar
                );
        }

        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        private readonly object _isTestCompleted = new object();

        /// <summary>
        /// </summary>
        private void WatchDog()
        {
            var isTestCompleted = this._isTestCompleted;
            lock (isTestCompleted)
            {
                this._completedTestsCount++;
                if (this._completedTestsCount < 1)
                {
                    return;
                }

                var enableCommand = new TestingFinishDelegate
                    (this.TestingFinish);
                this.BeginInvoke
                    (
                        enableCommand);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowNetworkRequisitesForm_Load
            (
            object sender,
            EventArgs e)
        {
            this._formShown = false;

            Handler.SetFormPositionsOnScreenCenter
                (
                    this);
        }

        /// <summary>
        /// </summary>
        private void ShowRequisites()
        {
            this._completedTestsCount = 0;
            var progressBar = this.BatchProgressBar;
            if (progressBar != null)
            {
                var startControl = this.DoShowRequisitesButton;
                var stopControl = this.DoCloseFormButton;

                Handler.DisableStarProgressStopControls
                    (
                        startControl,
                        stopControl,
                        progressBar
                    );
                var batchParameters = new BatchWithProgressbar
                    (progressBar);

                var networkRequisitesProgram =
                    new ProgramWithOutput
                    {
                        ArgumentsString = string.Empty,
                        TextOutputControl = this.RequisitesTextBox,
                        Program = Handler.NetshProgram
                    };

                if (batchParameters.ProgramsWithOutput != null)
                {
                    batchParameters.ProgramsWithOutput.Add
                        (
                            networkRequisitesProgram);
                }

                var testConnectionThread = new Thread(this.PerformBatch);
                testConnectionThread.Start
                    (
                        batchParameters);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="textValue"></param>
        private delegate void SetControlValueDelegate(
            Control control,
            string textValue);

        /// <summary>
        /// </summary>
        /// <param name="textControl"></param>
        /// <param name="someText"></param>
        private static void SetControlText
            (
            Control textControl,
            string someText)
        {
            if (textControl != null)
            {
                textControl.Text = someText;
                textControl.Refresh();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetBatchOutputToControl
            (
            object input
            )
        {
            var programWithOutput = (ProgramWithOutput) input;

            if (programWithOutput != null)
            {
                var program = programWithOutput.Program;
                var arguments = programWithOutput.ArgumentsString;
                var textControl = programWithOutput.TextOutputControl;

                var programOutput = Handler
                    .ExecuteProgramWithArguments
                    (
                        program,
                        arguments
                    );

                var setControlText = new SetControlValueDelegate
                    (ShowNetworkRequisitesForm.SetControlText);
                this.BeginInvoke
                    (
                        setControlText,
                        textControl,
                        programOutput);
            }

            var onOnBatchFinish = this.OnBatchFinish;
            if (onOnBatchFinish != null)
            {
                onOnBatchFinish();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="outputControlsObject"></param>
        private void PerformBatch
            (
            object outputControlsObject)
        {
            var parameters = (BatchWithProgressbar) outputControlsObject;

            if (parameters != null)
            {
                if (parameters.ProgramsWithOutput != null)
                {
                    var textBoxes = parameters.ProgramsWithOutput
                                              .Where
                        (
                            programWithOutput => programWithOutput != null)
                                              .Select
                        (
                            programWithOutput =>
                            programWithOutput.TextOutputControl)
                                              .ToList();

                    
                    ShowNetworkRequisitesForm.SetControlsText
                        (
                            Handler.NetworkRequisitesWillBeHere,
                            textBoxes
                        );
                }
            }

            if (parameters != null)
            {
                var progressIndicator = parameters.ProgressBar;
                if (progressIndicator != null)
                {
                    progressIndicator.Style = ProgressBarStyle.Marquee;
                }
                if (parameters.ProgramsWithOutput != null)
                {
                    foreach (
                        var programWithOutput in parameters.ProgramsWithOutput)
                    {
                        var thread = new Thread(this.SetBatchOutputToControl);
                        thread.Start
                            (
                                programWithOutput);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public delegate void MethodContainer();

        /// <summary>
        /// 
        /// </summary>
        public event MethodContainer OnBatchFinish;

        /// <summary>
        /// </summary>
        /// <param name="textValue"></param>
        /// <param name="textControls"></param>
        private static void SetControlsText
            (
            string textValue,
            IEnumerable<Control> textControls)
        {
            if (textControls != null)
            {
                foreach (var textBox in textControls)
                {
                    var setText = new SetControlValueDelegate
                        (ShowNetworkRequisitesForm.SetControlText);
                    var box = textBox;
                    if (box != null)
                    {
                        box.BeginInvoke
                            (
                                setText,
                                box,
                                textValue);
                    }
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoCloseFormButton_Click
            (
            object sender,
            EventArgs e)
        {
            Handler.CloseForm
                (
                    this);
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoCopyLog_Click
            (
            object sender,
            EventArgs e)
        {
            var outputsControls = this._outputsControls;
            if (outputsControls != null)
            {
                var outputText =
                    outputsControls.Where
                        (
                            outputsControl =>
                            outputsControl != null)
                                   .Aggregate
                        (
                            string.Empty,
                            (current,
                             outputsControl) =>
                            $"{current}{outputsControl.Text}{Environment.NewLine}" );
                if (outputText != null)
                {
                    Clipboard.SetText
                        (
                            outputText);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowNetworkRequisitesForm_Shown
            (
            object sender,
            EventArgs e)
        {
            if (this._formShown)
            {
                return;
            }

            this.ShowRequisites();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoShowRequisitesButton_Click
            (
            object sender,
            EventArgs e)
        {
            this.ShowRequisites();
        }
    }
}
