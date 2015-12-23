using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using RuskomDiagnostics.Annotations;
using RuskomDiagnostics.Properties;

namespace RuskomDiagnostics
{
    // ReSharper disable StringLiteralsWordIsNotInDictionary
    /*
http://stackoverflow.com/questions/4015324/http-request-with-post             
     */
    // ReSharper restore StringLiteralsWordIsNotInDictionary

    /// <summary>
    /// 
    /// </summary>
    public class InterfaceIpV4Address
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly IPAddress InterfaceIpAddress ;

        /// <summary>
        /// 
        /// </summary>
        public readonly string AddressSubnetMask ;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceIpAddress"></param>
        /// <param name="addressSubnetMask"></param>
        public InterfaceIpV4Address
            (
            IPAddress interfaceIpAddress ,
            string addressSubnetMask
            )
        {
            this.InterfaceIpAddress = interfaceIpAddress ;
            this.AddressSubnetMask = addressSubnetMask ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ProgramWithOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public Control TextOutputControl ;

        /// <summary>
        /// 
        /// </summary>
        public string ArgumentsString ;

        /// <summary>
        /// 
        /// </summary>
        public ProcessExecuteParameters Program ;
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class BatchWithProgressbar
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly List < ProgramWithOutput > ProgramsWithOutput ;

        /// <summary>
        /// 
        /// </summary>
        public readonly ProgressBar ProgressBar ;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressBar"></param>
        public BatchWithProgressbar ( ProgressBar progressBar )
        {
            this.ProgramsWithOutput = new List < ProgramWithOutput >( ) ;
            this.ProgressBar = progressBar ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [ DataContract ]
    public sealed class BalanceTypeResponse
    {
        /// <summary>
        /// 
        /// </summary>
        [ DataMember ( Name = "status" ) ]
        [ CanBeNull ]
        public string Status { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        [ DataMember ( Name = "message" ) ]
        [ CanBeNull ]
        public string Message { get ; set ; }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class PingParameters
    {
        /// <summary>
        /// 
        /// </summary>
        public string Host ;

        /// <summary>
        /// 
        /// </summary>
        private readonly int _pingTimeout ;

        /// <summary>
        /// 
        /// </summary>
        private readonly int _pingSize ;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="pingTimeout"></param>
        /// <param name="pingSize"></param>
        public PingParameters
            (
            [ CanBeNull ] string host ,
            int pingTimeout ,
            int pingSize
            )
        {
            this.Host = host ;
            this._pingTimeout = pingTimeout ;
            this._pingSize = pingSize ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ NotNull ]
        public PingParameters Copy ( )
        {
            var result = new PingParameters
                (
                this.Host ,
                this._pingTimeout ,
                this._pingSize ) ;
            return result ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public long PerformPing ( )
        {
            var ping = new Ping( ) ;

            PingReply reply ;
            var bytesToSend = new byte[this._pingSize] ;

            for ( long index = 0 ;
                  index < this._pingSize ;
                  index++ )
            {
                bytesToSend[ index ] = ( byte ) index ;
            }

            try
            {
                var hostNameOrAddress = this.Host ;

                reply = ( string.IsNullOrEmpty
                            (
                             hostNameOrAddress ) )
                            ? null
                            : ping.Send
                                  (
                                   hostNameOrAddress ,
                                   this._pingTimeout ,
                                   bytesToSend ) ;
            }
            catch ( Exception )
            {
                reply = null ;
            }

            var pingSuccess = reply?.Status == IPStatus.Success ;

            var millisecondsSpend =
                pingSuccess
                    ? reply.RoundtripTime
                    : PingResult.C_PingFailRoundtripDuration ;

            return millisecondsSpend ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class PingResult
    {
        /// <summary>
        /// 
        /// </summary>
        public const long C_PingFailRoundtripDuration = long.MaxValue ;

        /// <summary>
        /// </summary>
        private const double C_FailRatioMaximum = 1 ;

        /// <summary>
        /// 
        /// </summary>
        public long AverageRoundtripTime ;

        /// <summary>
        /// 
        /// </summary>
        public double LostRatio ;

        /// <summary>
        /// </summary>
        private readonly long _sendCount ;

        /// <summary>
        /// </summary>
        private readonly long _failCount ;

        /// <summary>
        /// </summary>
        private readonly List < long > _roundtripTimes ;

        /// <summary>
        /// 
        /// </summary>
        public PingResult ( )
        {
            this.AverageRoundtripTime = PingResult.C_PingFailRoundtripDuration ;
            this.LostRatio = PingResult.C_FailRatioMaximum ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PingResult
            (
            [ NotNull ] PingParameters pingParameters ,
            int attemptNumber )
        {
            this._roundtripTimes = new List < long >( ) ;

            this._sendCount = 0 ;
            this._failCount = 0 ;

            for ( var attemptCounter = 0 ;
                  attemptCounter < attemptNumber ;
                  attemptCounter++ )
            {
                {
                    var roundtripTime = pingParameters.PerformPing( ) ;

                    this._sendCount++ ;
                    if ( roundtripTime == PingResult.C_PingFailRoundtripDuration )
                    {
                        this._failCount++ ;
                    }

                    this._roundtripTimes.Add
                        (
                            roundtripTime ) ;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Calculate ( )
        {
            long roundtripTimesSumma = 0 ;
            var finishedTripCount = 0 ;
            var averageRoundtripTime = PingResult.C_PingFailRoundtripDuration ;

            if ( this._roundtripTimes != null )
            {
                foreach ( var roundtripTime in this._roundtripTimes )
                {
                    if ( roundtripTime != PingResult.C_PingFailRoundtripDuration )
                    {
                        roundtripTimesSumma += roundtripTime ;
                        finishedTripCount++ ;
                    }

                    if ( finishedTripCount > 0 )
                    {
                        averageRoundtripTime = roundtripTimesSumma
                                               / finishedTripCount ;
                    }
                }
            }

            this.AverageRoundtripTime = averageRoundtripTime ;
            var failRatio = ( ( double ) this._failCount ) / this._sendCount ;
            this.LostRatio = failRatio ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class DiagnosticResult
    {
        /// <summary>
        // ReSharper disable WordIsNotInDictionary
        /// Состояние подключения 
        // ReSharper restore WordIsNotInDictionary
        /// </summary>
        public enum ClientConnectionState
        {
            /// <summary>
            /// Не известно
            /// </summary>
            Unknown ,

            /// <summary>
            /// Поднят
            /// </summary>
            Up ,

            /// <summary>
            /// Не найден
            /// </summary>
            NotFound
        }

        /// <summary>
        /// 
        /// </summary>
        public string SmallGatewayResponseTime ;

        /// <summary>
        /// 
        /// </summary>
        public string SmallHomeResponseTime ;

        /// <summary>
        /// 
        /// </summary>
        public string SmallWordResponseTime ;

        /// <summary>
        /// 
        /// </summary>
        public string SmallGatewayFailRatio ;

        /// <summary>
        /// 
        /// </summary>
        public string SmallHomeFailRatio ;

        /// <summary>
        /// 
        /// </summary>
        public string SmallWordFailRatio ;

        /// <summary>
        /// 
        /// </summary>
        public string BigGatewayResponseTime ;

        /// <summary>
        /// 
        /// </summary>
        public string BigHomeResponseTime ;

        /// <summary>
        /// 
        /// </summary>
        public string BigWordResponseTime ;

        /// <summary>
        /// 
        /// </summary>
        public string BigGatewayFailRatio ;

        /// <summary>
        /// 
        /// </summary>
        public string BigHomeFailRatio ;

        /// <summary>
        /// 
        /// </summary>
        public string BigWordFailRatio ;

        /// <summary>
        /// 
        /// </summary>
        public string ClientSpeedTestIp ;

        /// <summary>
        /// 
        /// </summary>
        public string LanSpeed ;

        /// <summary>
        /// 
        /// </summary>
        public string ClientIp ;

        /// <summary>
        /// 
        /// </summary>
        public string ClientMac ;

        /// <summary>
        /// 
        /// </summary>
        public string AdapterName ;

        /// <summary>
        /// 
        /// </summary>
        public ClientConnectionState ConnectionState ;

        /// <summary>
        /// 
        /// </summary>
        public DiagnosticResult ( )
        {
            this.ConnectionState = ClientConnectionState.Unknown ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ProcessExecuteParameters
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly string ExecutableFile ;

        /// <summary>
        /// 
        /// </summary>
        private readonly string _programArguments ;

        /// <summary>
        /// 
        /// </summary>
        private readonly List < string > _programsFiles ;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="executableFile"></param>
        /// <param name="programArguments"></param>
        /// <param name="programsFiles"></param>
        public ProcessExecuteParameters
            (
            string executableFile ,
            string programArguments ,
            List < string > programsFiles
            )
        {
            this.ExecutableFile = executableFile ;
            this._programArguments = programArguments ;
            this._programsFiles = programsFiles ;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        private bool Verify ( )
        {
            var isFilesValid = true ;
            var applicationStartupPath = Program.ApplicationStartupPath ?? string.Empty;

            var programsFiles = this._programsFiles ;
            if ( programsFiles == null )
            {
                return true ;
            }
            foreach ( var fullFilename in programsFiles )
            {
                var isfileExists = false ;
                if ( fullFilename != null )
                {
                    var instanceProgramsFile = Path.Combine
                        (
                         applicationStartupPath ,
                         fullFilename ) ;
                    isfileExists = File.Exists
                        (
                         instanceProgramsFile ) ;

                    if ( ! isfileExists )
                    {
                        isfileExists = ProcessExecuteParameters.IsFileExistsInsidePaths
                            (
                             fullFilename ) ;
                    }
                }
                isFilesValid &= isfileExists ;
                if ( ! isFilesValid )
                {
                    break ;
                }
            }
            return isFilesValid ;
        }

        /// <summary>
        /// </summary>
        /// <param name="programsFile"></param>
        /// <returns></returns>
        private static bool IsFileExistsInsidePaths
            (
            [ CanBeNull ] string programsFile )
        {
            var fileExists = ( programsFile == null ) ;
            if ( ! fileExists )
            {
                fileExists = Settings.Default == null ;
            }
            if ( ! fileExists )
            {
                fileExists = ( Handler.PathEnvironmentVariableName
                               == null ) ;
            }
            if ( ! fileExists )
            {
                var environmentVariable = Environment.GetEnvironmentVariable
                    (
                     Handler.PathEnvironmentVariableName ) ;
                if ( environmentVariable != null )
                {
                    var paths = environmentVariable
                        .Split
                        (
                         new[]
                         {
                             ';'
                         } ,
                         StringSplitOptions.RemoveEmptyEntries ) ;

                    var sortedPaths = from s in paths
                                      orderby s descending
                                      select s ;

                    foreach ( var path in sortedPaths )
                    {
                        if ( path != null )
                        {
                            var fullFilename = Path.Combine
                                (
                                 path ,
                                 programsFile
                                ) ;
                            fileExists = File.Exists
                                (
                                 fullFilename ) ;
                        }

                        if ( fileExists )
                        {
                            break ;
                        }
                    }
                }
            }
            return fileExists ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ NotNull ]
        public ProcessExecuteResult Execute
            (
            [ CanBeNull ] string executeWithArguments = "" )
        {
            var processExecuteResult = new ProcessExecuteResult( ) ;
            var executable = this.ExecutableFile ;

            if ( string.IsNullOrEmpty
                (
                 executable ) )
            {
                processExecuteResult.ExecuteStatus =
                    TaskStatus.ActionStatus.Fail ;
            }
            var isParametersValid = false ;
            if ( ! string.IsNullOrEmpty
                       (
                        executable ) )
            {
                isParametersValid = this.Verify( ) ;
                if ( ! isParametersValid )
                {
                    processExecuteResult.ExecuteStatus =
                        TaskStatus.ActionStatus.Fail ;
                }
            }

            if ( isParametersValid )
            {
                var arguments =
                    $"{this._programArguments} {executeWithArguments}" ;
                var programsProcess =
                    new Process
                    {
                        StartInfo =
                        {
                            FileName = executable ,
                            Arguments = arguments ,
                            UseShellExecute = false ,
                            RedirectStandardOutput = true ,
                            CreateNoWindow = true ,
                            StandardOutputEncoding = Handler.OutputEncoding
                        }
                    } ;
                try
                {
                    programsProcess.Start( ) ;
                    processExecuteResult.ReadToEndsResult =
                        programsProcess.StandardOutput.ReadToEnd( ) ;
                    processExecuteResult.ExecuteStatus =
                        TaskStatus.ActionStatus.Ok ;
                }
                catch ( Exception )
                {
                    processExecuteResult.ExecuteStatus =
                        TaskStatus.ActionStatus.Error ;
                }
            }
            return processExecuteResult ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class TaskStatus
    {
        /// <summary>
        /// 
        /// </summary>
        public enum ActionStatus
        {
            /// <summary>
            /// Неизвестен
            /// </summary>            
            Unknown ,

            /// <summary>
            /// Сбой
            /// </summary>            
            Fail ,

            /// <summary>
            /// Ошибка
            /// </summary>
            Error ,

            /// <summary>
            /// Ок
            /// </summary>
            Ok
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="speedTestStatus"></param>
        /// <returns></returns>
        [ NotNull ]
        public static string GetStatusDescription
            (
            ActionStatus speedTestStatus )
        {
            string statusString = string.Empty ;
            if ( Settings.Default != null )
            {
                switch ( speedTestStatus )
                {
                    case ActionStatus.Error :
                        statusString = Settings.Default.ActionStatusError ;
                        break ;
                    case ActionStatus.Fail :
                        statusString = Settings.Default.ActionStatusFail ;
                        break ;
                    case ActionStatus.Ok :
                        statusString = Settings.Default.ActionStatusOk ;
                        break ;
                    case ActionStatus.Unknown :
                        statusString = Settings.Default.ActionStatusUnknown ;
                        break ;
                    default :
                        statusString = string.Empty ;
                        break ;
                }
            }
            statusString = statusString ?? string.Empty ;
            return statusString ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class ProcessExecuteResult
    {
        /// <summary>
        /// 
        /// </summary>
        public TaskStatus.ActionStatus ExecuteStatus ;

        /// <summary>
        /// 
        /// </summary>
        public string ReadToEndsResult ;

        /// <summary>
        /// 
        /// </summary>
        public ProcessExecuteResult ( )
        {
            this.ExecuteStatus = TaskStatus.ActionStatus.Unknown ;
            this.ReadToEndsResult = string.Empty ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class SpeedTestResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string TimeMark ;

        /// <summary>
        /// 
        /// </summary>
        private string _formatedTimeMark = string.Empty ;

        /// <summary>
        /// 
        /// </summary>
        public string TestIp ;
        
        /// <summary>
        /// 
        /// </summary>
        public string DataSentVolumeBits ;

        /// <summary>
        /// 
        /// </summary>
        private double _dataSentVolumeMb ;

        /// <summary>
        /// 
        /// </summary>
        public string NetworkSpeedBitsPerSecond ;

        /// <summary>
        /// 
        /// </summary>
        private double _networkSpeedMbitPerS ;

        /// <summary>
        /// 
        /// </summary>
        private double _networkSpeedMbPerS ;

        /// <summary>
        /// 
        /// </summary>
        private string _statusDescription ;

        /// <summary>
        /// 
        /// </summary>
        public TaskStatus.ActionStatus TestStatus ;

        /// <summary>
        /// </summary>
        private const long C_BitsInByte = 8 ;

        /// <summary>
        /// </summary>
        private const long C_BytesInKilobyte = 1024 ;

        /// <summary>
        /// </summary>
        private const long C_BytesInMegabyte =
            SpeedTestResult.C_BytesInKilobyte * SpeedTestResult.C_BytesInKilobyte ;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void CalculateIndicators ( )
        {
            try
            {
                if ( Settings.Default != null )
                {
                    var testDatetime = DateTime.ParseExact
                        (
                         this.TimeMark ,
                         Settings.Default.SpeedTestParseTimeFormat ,
                         CultureInfo.InvariantCulture ) ;
                    this._formatedTimeMark = testDatetime.ToString
                        (
                         Settings.Default.SpeedTestTimeFormat ,
                         CultureInfo.InvariantCulture ) ;
                }
            }
            catch ( Exception )
            {
                this._formatedTimeMark = string.Empty ;
            }

            long dataSentVolumeBitsNumber ;
            try
            {
                long.TryParse
                    (
                     this.DataSentVolumeBits ,
                     out dataSentVolumeBitsNumber ) ;
            }
            catch ( Exception )
            {
                dataSentVolumeBitsNumber = 0 ;
            }
            this._dataSentVolumeMb = ( double ) dataSentVolumeBitsNumber
                                     / SpeedTestResult.C_BitsInByte / SpeedTestResult.C_BytesInMegabyte ;

            try
            {
                long networkSpeed ;
                long.TryParse
                    (
                     this.NetworkSpeedBitsPerSecond ,
                     out networkSpeed ) ;
                var networkSpeedMbitPerS = ( double ) networkSpeed
                                           / SpeedTestResult.C_BytesInMegabyte ;
                var networkSpeedMbPerS = ( double ) networkSpeed / SpeedTestResult.C_BitsInByte
                                         / SpeedTestResult.C_BytesInMegabyte ;
                this._networkSpeedMbitPerS = networkSpeedMbitPerS ;
                this._networkSpeedMbPerS = networkSpeedMbPerS ;
            }
            catch ( Exception )
            {
                this._networkSpeedMbitPerS = 0 ;
                this._networkSpeedMbPerS = 0 ;
            }

            this._statusDescription = TaskStatus.GetStatusDescription
                (
                 this.TestStatus ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        public SpeedTestResult ( )
        {
            this.TimeMark = string.Empty ;
            this.TestIp = string.Empty ;
            this.DataSentVolumeBits = string.Empty ;
            this.NetworkSpeedBitsPerSecond = string.Empty ;
            this.TestStatus = TaskStatus.ActionStatus.Unknown ;
            this._statusDescription = TaskStatus.GetStatusDescription
                (
                 this.TestStatus ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ NotNull ]
        public string ToColumn ( )
        {
            var resultAsString = string.Empty ;
            var tryConvert = ( Settings.Default != null ) ;

            if ( tryConvert )
            {
                if ( Settings.Default.SpeedTestResultAsStringFormat == null )
                {
                    tryConvert = false ;
                }
            }

            if ( tryConvert )
            {
                resultAsString = string.Format
                    (
                     Settings.Default.SpeedTestResultAsStringFormat ,
                     Environment.NewLine ,
                     this._statusDescription ,
                     this._formatedTimeMark ,
                     this.TestIp ,
                     this._dataSentVolumeMb.ToString
                         (
                          Handler.TestResultNumericFormat ,
                          CultureInfo.InvariantCulture ) ,
                     this._networkSpeedMbitPerS.ToString
                         (
                          Handler.TestResultNumericFormat ,
                          CultureInfo.InvariantCulture ) ,
                     this._networkSpeedMbPerS.ToString
                         (
                          Handler.TestResultNumericFormat ,
                          CultureInfo.InvariantCulture ) ) ;
            }

            return resultAsString ;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string ToString ( )
        {
            var resultAsString = string.Empty ;
            var tryConvert = ( Settings.Default != null ) ;

            if ( tryConvert )
            {
                if ( Settings.Default.SpeedTestResultAsColumnFormat == null )
                {
                    tryConvert = false ;
                }
            }

            if ( tryConvert )
            {
                resultAsString = string.Format
                    (
                     Settings.Default.SpeedTestResultAsColumnFormat ,
                     this._statusDescription ,
                     this._formatedTimeMark ,
                     this.TestIp ,
                     this._dataSentVolumeMb.ToString
                         (
                          Handler.TestResultNumericFormat ,
                          CultureInfo.InvariantCulture ) ,
                     this._networkSpeedMbitPerS.ToString
                         (
                          Handler.TestResultNumericFormat ,
                          CultureInfo.InvariantCulture ) ,
                     this._networkSpeedMbPerS.ToString
                         (
                          Handler.TestResultNumericFormat ,
                          CultureInfo.InvariantCulture ) ) ;
            }

            return resultAsString ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NetworkAdapterParameters
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly string Address ;

        /// <summary>
        /// 
        /// </summary>
        public readonly string Gateway ;

        /// <summary>
        /// 
        /// </summary>
        public string AdapterName ;

        /// <summary>
        /// 
        /// </summary>
        public readonly string MacAddress ;

        /// <summary>
        /// 
        /// </summary>
        public readonly string Dns ;

        /// <summary>
        /// 
        /// </summary>
        public NetworkAdapterParameters ( )
        {
            this.Address = string.Empty ;
            this.Gateway = string.Empty ;
            this.AdapterName = string.Empty ;
            this.MacAddress = string.Empty ;
            this.Dns = string.Empty ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientAddress"></param>
        /// <param name="gateway"></param>
        /// <param name="macAddress"></param>
        /// <param name="dns"></param>
        public NetworkAdapterParameters
            (
            string clientAddress ,
            string gateway ,
            string macAddress ,
            string dns )
        {
            this.Address = clientAddress ;
            this.Gateway = gateway ;
            this.AdapterName = string.Empty ;
            this.MacAddress = macAddress ;
            this.Dns = dns ;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class Handler
    {
        /// <summary>
        /// </summary>
        private const int C_FirstIndex = 0;

        /// <summary>
        /// 
        /// </summary>
        public const bool C_DisableAutorun = false;

        // ReSharper disable StringLiteralsWordIsNotInDictionary

        /// <summary>
        /// </summary>
        private const string C_DiagnosticApiWebAddress =
            "https://ticket.rk1.ru/api/rk_stat.php";

        /// <summary>
        /// </summary>
        private const string C_PayWebPageAddress =
            "https://istat.rk1.ru/index.php?id=18";

        /// <summary>
        /// 
        /// </summary>
        public const string C_HomeNetworkHost = "rk1.ru";

        /// <summary>
        /// 
        /// </summary>
        public const string C_CityNetworkHost = "e1.ru";

        /// <summary>
        /// 
        /// </summary>
        public const string C_CountryNetworkHost = "vk.com";

        /// <summary>
        /// </summary>
        private const string C_WordNetworkHost = "google.com";

        // ReSharper restore StringLiteralsWordIsNotInDictionary

        /// <summary>
        /// </summary>
        private const bool C_SetAutorun = true;

        /// <summary>
        /// </summary>
        private const MessageBoxButtons C_MessageBoxButtons =
            MessageBoxButtons.YesNo;

        /// <summary>
        /// </summary>
        private const MessageBoxIcon C_MessageBoxIcon = MessageBoxIcon.Question;

        /// <summary>
        /// </summary>
        private const DialogResult C_UserAnswerYes = DialogResult.Yes;

        /// <summary>
        /// 
        /// </summary>
        public static readonly string TestResultNumericFormat;

        /// <summary>
        /// 
        /// </summary>
        private static readonly PingParameters[] SmallPingSettings;

        /// <summary>
        /// 
        /// </summary>
        public static string SpeedTestError { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private static readonly PingParameters[] BigPingSettings;

        /// <summary>
        /// </summary>
        private const int C_HomeParameterIndex = 0;

        /// <summary>
        /// </summary>
        private const int C_WordParameterIndex = 1;

        /// <summary>
        /// 
        /// </summary>
        public static readonly ProcessExecuteParameters SpeedTestProgram;

        /// <summary>
        /// 
        /// </summary>
        public static readonly ProcessExecuteParameters PingProgram;

        /// <summary>
        /// 
        /// </summary>
        public static readonly ProcessExecuteParameters NetshProgram ;

        /// <summary>
        /// </summary>
        private static string NetshProgramArguments { get; set; }

        /// <summary>
        /// </summary>
        private static string NetshExecutable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static readonly ProcessExecuteParameters TracertProgram ;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startControl"></param>
        /// <param name="stopControl"></param>
        /// <param name="progressBar"></param>
        public static void DisableStarProgressStopControls
            (
            [ CanBeNull ] Control startControl ,
            [ CanBeNull ] Control stopControl ,
            [ CanBeNull ] ProgressBar progressBar
            )
        {
            if ( startControl != null )
            {
                startControl.Enabled = false ;
            }
            if ( stopControl != null )
            {
                stopControl.Enabled = false ;
            }
            if ( progressBar != null )
            {
                progressBar.Value = progressBar.Minimum ;
                progressBar.Style = ProgressBarStyle.Marquee ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startControl"></param>
        /// <param name="stopControl"></param>
        /// <param name="progressBar"></param>
        public static void EnableStartProgressStopControls
            (
            [ CanBeNull ] Control startControl ,
            [ CanBeNull ] Control stopControl ,
            [ CanBeNull ] ProgressBar progressBar
            )
        {
            if ( startControl != null )
            {
                startControl.Enabled = true ;
            }
            if ( stopControl != null )
            {
                stopControl.Enabled = true ;
            }
            if ( progressBar != null )
            {
                progressBar.Value =
                    progressBar.Maximum ;
                progressBar.Style = ProgressBarStyle.Blocks ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        public static void SetFormPositionsOnScreenCenter
            (
            [ CanBeNull ] Form form )
        {
            if ( form != null )
            {
                var top = Screen.GetBounds ( form )
                                .Height / 2 - form.Height / 2 ;
                var left = Screen.GetBounds ( form )
                                 .Width / 2 - form.Width / 2 ;

                form.Top = top ;
                form.Left = left ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string TestingNetworkSpeed { get; private set; }

        // ReSharper disable FunctionComplexityOverflow
        static Handler ( )
            // ReSharper restore FunctionComplexityOverflow
        {
            // ReSharper disable StringLiteralsWordIsNotInDictionary
            const int C_CmdCodePage = 866;
            const string C_PathEnvironmentVariableName = "PATH";
            const string C_NetshProgramArguments = "interface ipv4 show config";
            const string C_NetshExe = "netsh.exe";
            const string C_Cygwin1Dll = "cygwin1.dll" ;
            const string C_CygstdcDll = "cygstdc++-6.dll" ;
            const string C_CyggccSDll = "cyggcc_s-1.dll" ;
            const string C_IperfExe = "iperf.exe" ;
            const string C_IperfArguments = "-y C -c" ;
            const string C_PingExe = "ping.exe" ;
            const string C_TracertExe = "tracert.exe" ;
            const string C_TracertArguments = "-w 1000" ;
            const string C_TestResultNumericFormat = "F" ;
            const string C_BalanceTypeRequestFail = "FAIL" ;
            const string C_SpeedTestServerAddress = "89.106.248.22" ;
            const int C_MeasuresCount = 5 ;
            const string C_PostDiagnosticsResultFormat =
                "type=lan_diag&gwrs={0}&rk1rs={1}&ggrs={2}&gwlost={3}&rk1lost={4}&gglost={5}&biggw"
                + "rs={6}&bigrk1rs={7}&bigggrs={8}&biggwlost={9}&bigrk1lost={10}&biggglost={11}&lan"
                + "iperfip={12}&laniperfspeed={13}&osv={14}&d_ip={15}&d_mac={16}&d_t={17}" ;

            const string C_QuestionPerformRestart = "Обновление установлено, перезапустить приложение ?";
            const string C_AutorunEnablingError =
                "Ошибка добавления в Автозагрузку" ;
            const string C_AutorunDisablingError =
                "Ошибка удаления из Автозагрузки" ;
            const string C_DisconnectOrInvalidConnection =
                "Отсутствует подключение к сети или неверные настройки сети" ;
            const string C_UpdateStorageLocation = "ftp://89.106.251.215/" ;
            const string C_UpdateVersionFilename = "version.info" ;
            const string C_AnonimusFtpUserName = "anonymous" ;
            const string C_AnonimusPassword = "rk1@rk1.ru" ;
            const string C_RequestBalanceFail =
                "Баланс лицевого счёта не известен" ;
            const string C_ShowBalanceTitle = "Баланс" ;

            const string C_ShowContactsTitle = "РусКом Контакты";
            const string C_ShowContactsMessage =
@"Абон.отдел:
Адрес: Щорса, д. 7
Телефон: 251-93-00
e-mail: rk1@rk1.ru
Время работы:
С ПОНЕДЕЛЬНИКА ПО ПЯТНИЦУ 
С 09:00 ДО 20:00 
СУББОТА, ВОСКРЕСЕНЬЕ – ВЫХОДНОЙ


Тех.поддержка:
Телефон: 221-32-50
e-mail: support@rk1.ru
Время работы:
С 08:00 до 23:00
КАЖДЫЙ ДЕНЬ

";

            const string C_BalanceTypeRequestOk = "ok" ;
            const string C_BalanceRequest = "type=balance" ;
            const string C_SpeedTestError =
                "При выполнении проверки скорости произошла ошибка" ;
            const string C_SpeedTestFail =
                "При выполнении проверки скорости произошёл сбой" ;
            const string C_NewVersionAvailable = "Вышла новая версия, обновить ?";
            const string C_ActualVersionInstalled = "Установлена актуальная версия, обновления не требуется";
            const string C_DeletingFileError = "Файл \'{0}\' существует, при его удалении произошла ошибка";
            const string C_NetworkRequisitesWillBeHere = "Здесь будут написаны реквизиты сетевых соединений . Пожалуйста ждите ";
            const string C_DownloadFail = "Ошибка загрузки обновления";
                    const string C_TestingNetworkSpeed =
            "Происходит измерение скорости . Не закрывайте это окно ." ;
        const string C_ConnectionsFineQualitySymptom = "(0%" ;
       const string C_ConnectionFineQuality = "Качество связи отличное" ;
        const string C_ConnectionLoses =
            "Обнаружины потери, вы можете связаться со службой технической подержки по телефон"
            + "у 8.8.8.8" ;
        const string C_WaitWhileTestProcessed =
            "Происходит проверка связи . Пожалуйста, ждите ." ;
            const string C_TraceComplite = "Построение маршрута завершенно";
        const string C_DefaultColumnName = "Колонка" ;

      const string C_EmptyColumnValue = "<нет значения>" ;
            // ReSharper enable StringLiteralsWordIsNotInDictionary

            const int C_ShowBalloonTipWithShortMilliseconds = 1000 ;
            const int C_PingPackageSize = 32 ;
            const int C_BigPingPackageSize = 1200 ;
            const int C_Timeout = 500 ;
            const int C_PostRequestTimeout = 999 ;

            // ReSharper disable PossibleNullReferenceException

            Handler.QuestionPerformRestart =
                Settings.Default.InitializeWithString
                    (
                        Settings.Default.QuestionPerformRestart,
                        C_QuestionPerformRestart);

            Handler.PathEnvironmentVariableName =
                Settings.Default.InitializeWithString
                    (
                        Settings.Default.PathEnvironmentVariableName ,
                        C_PathEnvironmentVariableName ) ;
            Handler.DefaultColumnName=
                Settings.Default.InitializeWithString
                    (
                        Settings.Default.DefaultColumnName,
                        C_DefaultColumnName);
                Handler.EmptyColumnValue =
            Handler.TraceComplete =
                Settings.Default.InitializeWithString
                    (
                        Settings.Default.EmptyColumnValue,
                        C_EmptyColumnValue);
            Handler.TraceComplete =
                Settings.Default.InitializeWithString
                    (
                        Settings.Default.TraceComplite,
                        C_TraceComplite);
            //TraceComplete
            Handler.NetshExecutable =
                Settings.Default.InitializeWithString
                    (
                        Settings.Default.NetshExecutable,
                        C_NetshExe);
            Handler.NetshProgramArguments =
                Settings.Default.InitializeWithString
                    (
                        Settings.Default.NetshProgramArguments,
                        C_NetshProgramArguments);
            Handler.ConnectionLoses =
                Settings.Default.InitializeWithString
                    (
                        Settings.Default.ConnectionLoses,
                        C_ConnectionLoses);
                Handler.ConnectionsFineQualitySymptom =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.ConnectionsFineQualitySymptom,
                            C_ConnectionsFineQualitySymptom);
            Handler.ConnectionFineQuality =
                Settings.Default.InitializeWithString
                    (
                        Settings.Default.ConnectionFineQuality,
                        C_ConnectionFineQuality);
            Handler.WaitWhileTestProcessed =
                Settings.Default.InitializeWithString
                    (
                        Settings.Default.WaitWhileTestProcessed,
                        C_WaitWhileTestProcessed);
                Handler.TestingNetworkSpeed =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.TestingNetworkSpeed,
                            C_TestingNetworkSpeed);
                Handler.NetworkRequisitesWillBeHere =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.NetworkRequisitesWillBeHere,
                            C_NetworkRequisitesWillBeHere);
                Handler.DeletingFileError =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.DeletingFileError,
                            C_DeletingFileError);
                Handler.ActualVersionInstalled =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.ActualVersionInstalled,
                            C_ActualVersionInstalled);
                Handler.NewVersionAvailable =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.NewVersionAvailable,
                            C_NewVersionAvailable);
                Handler.TestResultNumericFormat =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.TestResultNumericFormat,
                            C_TestResultNumericFormat);
                Handler.BalanceTypeRequestFail =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.BalanceTypeRequestFail,
                            C_BalanceTypeRequestFail);
                Handler.SpeedTestServerAddress =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.SpeedTestServerAddress,
                            C_SpeedTestServerAddress);
                Handler.PostDiagnosticsResultFormat =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.PostDiagnosticsResultFormat,
                            C_PostDiagnosticsResultFormat);

                Handler.AutorunEnablingError =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.AutorunEnablingError,
                            C_AutorunEnablingError);
                Handler.AutorunDisablingError =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.AutorunDisablingError,
                            C_AutorunDisablingError);
                Handler.DisconnectOrInvalidConnection =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.DisconnectOrInvalidConnection,
                            C_DisconnectOrInvalidConnection);
                Handler.UpdateStorageLocation =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.UpdateStorageLocation,
                            C_UpdateStorageLocation);
                Handler.UpdateVersionFilename =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.UpdateVersionFilename,
                            C_UpdateVersionFilename);
                Handler.AnonymousFtpUserName =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.AnonimusFtpUserName,
                            C_AnonimusFtpUserName);
                Handler.AnonymousPassword =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.AnonimusPassword,
                            C_AnonimusPassword);
                Handler.RequestBalanceFail =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.RequestBalanceFail,
                            C_RequestBalanceFail);
                Handler.ShowBalanceTitle =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.ShowBalanceTitle,
                            C_ShowBalanceTitle);
                Handler.ShowContactsTitle =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.ShowContactsTitle,
                            C_ShowContactsTitle);
                Handler.ShowContactsMessage =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.ShowContactsMessage,
                            C_ShowContactsMessage);
                Handler.BalanceTypeRequestOk =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.BalanceTypeRequestOk,
                            C_BalanceTypeRequestOk);
                Handler.BalanceRequest = Settings.Default.InitializeWithString
                    (
                        Settings.Default.BalanceRequest,
                        C_BalanceRequest);
                Handler.SpeedTestError =
                    Settings.Default.InitializeWithString
                        (
                            Settings.Default.SpeedTestError,
                            C_SpeedTestError
                        );
                Handler.SpeedTestFail = Settings.Default.InitializeWithString
                    (
                        Settings.Default.SpeedTestFail,
                        C_SpeedTestFail);
                Handler.DownloadFail = Settings.Default.InitializeWithString
                    (
                        Settings.Default.DownloadFail,
                        C_DownloadFail);

            var cmdCodePage =
                (int) Settings.Default.InitializeWithLong
                          (
                              Settings.Default.CmdCodePage,
                              C_CmdCodePage);

            Handler.DeletingFileError = string.Empty;

            Handler.MeasuresCount =
                (int) Settings.Default.InitializeWithLong
                          (
                              Settings.Default.MeasuresCount,
                              C_MeasuresCount);
            var smallPingPackageSize =
                (int) Settings.Default.InitializeWithLong
                          (
                              Settings.Default.SmallPingPackageSize,
                              C_PingPackageSize);
            var bigPingPackageSize =
                (int) Settings.Default.InitializeWithLong
                          (
                              Settings.Default.BigPingPackageSize,
                              C_BigPingPackageSize);
            var pingTimeout =
                (int) Settings.Default.InitializeWithLong
                          (
                              Settings.Default.PingTimeout,
                              C_Timeout);
            Handler.ShowBalloonTipWithShortMilliseconds =
                (int) Settings.Default.InitializeWithLong
                          (
                              Settings.Default
                                      .ShowBalloonTipWithShortMilliseconds,
                              C_ShowBalloonTipWithShortMilliseconds);
            Handler.PostRequestTimeout =
                (int) Settings.Default.InitializeWithLong
                          (
                              Settings.Default.PostRequestTimeout,
                              C_PostRequestTimeout);
            Handler.PostRequestTimeout =
                (int) Settings.Default.InitializeWithLong
                          (
                              Settings.Default.PostRequestTimeout,
                              C_PostRequestTimeout
                          );

            Handler.SpeedTestExecutable =
                Settings.Default.InitializeWithString
                          (
                              Settings.Default.SpeedTestExecutable,
                              C_IperfExe
                          );
            Handler.SpeedTestProgramArguments =
                Settings.Default.InitializeWithString
                          (
                              Settings.Default.SpeedTestProgramArguments,
                              C_IperfArguments
                          );
            Handler.SpeedTestLibrary1 =
                Settings.Default.InitializeWithString
                          (
                              Settings.Default.SpeedTestLibrary1,
                              C_Cygwin1Dll
                          );
            Handler.SpeedTestLibrary2 =
                Settings.Default.InitializeWithString
                          (
                              Settings.Default.SpeedTestLibrary2,
                              C_CygstdcDll
                          );
            Handler.SpeedTestLibrary3 =
                Settings.Default.InitializeWithString
                          (
                              Settings.Default.SpeedTestLibrary3,
                              C_CyggccSDll
                          );
            Handler.PingExecutable =
                Settings.Default.InitializeWithString
                    (
                        Settings.Default.PingExecutable,
                        C_PingExe
                    );
    Handler.TracertExecutable =
                Settings.Default.InitializeWithString
                          (
                              Settings.Default.TracertExecutable,
                              C_TracertExe
                          );
        Handler.TracertArguments =
                Settings.Default.InitializeWithString
                          (
                              Settings.Default.TracertArguments,
                              C_TracertArguments
                          );
            // ReSharper restore PossibleNullReferenceException

        Handler.OutputEncoding = Encoding.GetEncoding(cmdCodePage);

                Handler.PingProgram = new ProcessExecuteParameters
                    (
                    Handler.PingExecutable ,
                    string.Empty ,
                    new List < string >
                    {
                        Handler.PingExecutable
                    } ) ;
                Handler.TracertProgram = new ProcessExecuteParameters
                    (
                    Handler.TracertExecutable ,
                    Handler.TracertArguments ,
                    new List < string >
                    {
                        Handler.TracertExecutable
                    } ) ;

            Handler.NetshProgram =
                new ProcessExecuteParameters
                    (
                    Handler.NetshExecutable, // NetshExecutable
                    Handler.NetshProgramArguments, // NetshProgramArguments
                    new List<string>
                    {
                        Handler.NetshExecutable // NetshExecutable
                    });

            Handler.SpeedTestProgram = new ProcessExecuteParameters
                (
                Handler.SpeedTestExecutable,
                Handler.SpeedTestProgramArguments,
                new List<string>
                    {
                        Handler.SpeedTestExecutable ,
                        Handler.SpeedTestLibrary1 ,
                        Handler.SpeedTestLibrary2 ,
                        Handler.SpeedTestLibrary3
                    }
                );

            var homeSmallPing = new PingParameters
                (
                Handler.C_HomeNetworkHost ,
                pingTimeout ,
                smallPingPackageSize ) ;
            var wordSmallPing = new PingParameters
                (
                Handler.C_WordNetworkHost ,
                pingTimeout ,
                smallPingPackageSize ) ;
            var homeBigPing = new PingParameters
                (
                Handler.C_HomeNetworkHost ,
                pingTimeout ,
                bigPingPackageSize ) ;
            var wordBigPing = new PingParameters
                (
                Handler.C_WordNetworkHost ,
                pingTimeout ,
                bigPingPackageSize ) ;
            Handler.SmallPingSettings =
                new[]
                {
                    homeSmallPing ,
                    wordSmallPing
                } ;

            Handler.BigPingSettings =
                new[]
                {
                    homeBigPing ,
                    wordBigPing
                } ;
        }

        /// <summary>
        /// 
        /// </summary>
        private static string QuestionPerformRestart { get ; set ; }


        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        public static Encoding OutputEncoding { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public static string PathEnvironmentVariableName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private static string TracertArguments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static string TracertExecutable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static string PingExecutable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string EmptyColumnValue { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static string TraceComplete { get ; private set ; }

        /// <summary>
        /// 
        /// </summary>
        public static string DefaultColumnName { get ; private set ; }

        /// <summary>
        /// 
        /// </summary>
        private static string SpeedTestLibrary3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static string SpeedTestLibrary2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static string SpeedTestLibrary1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static string SpeedTestProgramArguments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static string SpeedTestExecutable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string ConnectionLoses { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static string WaitWhileTestProcessed { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static string ConnectionFineQuality { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static string ConnectionsFineQualitySymptom { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="program"></param>
        /// <param name="hostName"></param>
        /// <returns></returns>
        [NotNull]
        public static string ExecuteProgramWithArguments
            (
            [ CanBeNull ] ProcessExecuteParameters program,
            string hostName)
        {
            string tracertResultText = string.Empty;
            if (program != null)
            {
                var textResult = program.Execute(hostName);
                tracertResultText =
                    $"{( textResult.ExecuteStatus == TaskStatus.ActionStatus.Ok ? textResult.ReadToEndsResult : TaskStatus.GetStatusDescription ( textResult.ExecuteStatus ) )}{Environment.NewLine}" ;
            }
            return tracertResultText;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string NetworkRequisitesWillBeHere { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private static string DownloadFail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        private static string DeletingFileError { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static string ActualVersionInstalled { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="setting"></param>
        /// <param name="constant"></param>
        /// <returns></returns>
        [NotNull]
        private static string InitializeWithString
            (
            [CanBeNull] this Settings configuration,
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
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="setting"></param>
        /// <param name="constant"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private static long InitializeWithLong
            (
            [CanBeNull] this Settings configuration,
            long setting,
            long constant,
            long limit = 0 )
        {
            long result;
            if (configuration != null)
            {
                result = setting > limit
                             ? setting
                             : constant;
            }
            else
            {
                result = constant;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private static string NewVersionAvailable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string SpeedTestFail { get ; private set ; }

        /// <summary>
        /// 
        /// </summary>
        private static string BalanceRequest { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        private static int PostRequestTimeout { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        private static string BalanceTypeRequestOk { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        private static int ShowBalloonTipWithShortMilliseconds { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        private static string ShowBalanceTitle { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        private static string ShowContactsTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static string ShowContactsMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static string AnonymousPassword { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        private static string AnonymousFtpUserName { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        public static string UpdateStorageLocation { get ; private set ; }

        /// <summary>
        /// 
        /// </summary>
        private static string AutorunDisablingError { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        private static string AutorunEnablingError { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        private static string PostDiagnosticsResultFormat { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        private static int MeasuresCount { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        public static string SpeedTestServerAddress { get ; private set ; }

        /// <summary>
        /// 
        /// </summary>
        private static string BalanceTypeRequestFail { get ; set ; }

        /// <summary>
        /// </summary>
        /// <param name="speedTestProgramWithArgument"></param>
        /// <returns></returns>
        [ NotNull ]
        private static SpeedTestResult GetSpeedTestResult
            (
            [ CanBeNull ] SpeedTestInput speedTestProgramWithArgument )
        {
            int timeIndex ;
            int ipIndex ;
            int volumeIndex ;
            int speedIndex ;
            string newLinesSymptom ;
            string componentsDelimiter ;

            if ( Settings.Default == null )
            {
                timeIndex = 0 ;
                ipIndex = 1 ;
                volumeIndex = 7 ;
                speedIndex = 8 ;
                newLinesSymptom = "\n" ;
                componentsDelimiter = "," ;
            }
            else
            {
                var ifAbove = Settings.Default.TimeComponentIndex ;
                const int C_ElseAbove = 0 ;
                var index = ifAbove.SetValueIfNotAbove (
                    C_ElseAbove ) ;
                timeIndex = index ;
                ipIndex = Settings.Default.SpeedTestIpComponentIndex ;
                volumeIndex = Settings.Default.DataSentVolumeComponentIndex ;
                speedIndex = Settings.Default.NetworkSpeedComponentIndex ;
                newLinesSymptom =
                    Settings.Default.NewLinesSymptom.SetValueIfNull ( "\n" ) ;
                componentsDelimiter =
                    Settings.Default.ResultComponentsDelimiter.SetValueIfNull
                        ( "," ) ;
            }

            const string C_Empty = "<нет данных>" ;

            var speedTestResult = new SpeedTestResult( ) ;
            var textLines = string.Empty ;
            var resultComponents = new string[0] ;

            if ( speedTestProgramWithArgument?.Program != null )
            {
                var executeResult =
                    speedTestProgramWithArgument
                        .Program
                        .Execute
                        (
                         speedTestProgramWithArgument.HostName ) ;
                if (executeResult.ExecuteStatus
                    == TaskStatus.ActionStatus.Ok)
                {
                    textLines = executeResult.ReadToEndsResult;
                }

                if ( ! string.IsNullOrEmpty
                           (
                            textLines ) )
                {
                    {
                        var fileContentWithoutEol =
                            textLines.Replace
                                (
                                 Environment.NewLine ,
                                 " " )
                                     .Replace
                                (
                                 newLinesSymptom ,
                                 " " ) ;

                        var delimiters = new[]
                                         {
                                             componentsDelimiter
                                         } ;

                        resultComponents = fileContentWithoutEol.Split
                            (
                             delimiters ,
                             StringSplitOptions.None ) ;
                    }
                }

                if ( resultComponents.Length > 0 )
                {
                    var lowerBound = resultComponents.GetLowerBound
                        (
                         Handler.C_FirstIndex ) ;
                    var upperBound = resultComponents.GetUpperBound
                        (
                         Handler.C_FirstIndex ) ;

                    var time = timeIndex.Between
                                   (
                                    lowerBound ,
                                    upperBound )
                                   ? resultComponents[ timeIndex ]
                                   : C_Empty ;
                    var speedTestIp = ipIndex.Between
                                          (
                                           lowerBound ,
                                           upperBound )
                                          ? resultComponents[ ipIndex ]
                                          : C_Empty ;

                    var dataSentVolume = volumeIndex.Between
                                             (
                                              lowerBound ,
                                              upperBound )
                                             ? resultComponents[ volumeIndex
                                                   ]
                                             : C_Empty ;
                    var networkSpeed = speedIndex.Between
                                           (
                                            lowerBound ,
                                            upperBound )
                                           ? resultComponents[ speedIndex ]
                                           : C_Empty ;

                    speedTestResult.TestStatus = executeResult.ExecuteStatus ;
                    speedTestResult.TimeMark = time ;
                    speedTestResult.TestIp = speedTestIp ;
                    speedTestResult.DataSentVolumeBits = dataSentVolume ;
                    speedTestResult.NetworkSpeedBitsPerSecond = networkSpeed ;
                    speedTestResult.CalculateIndicators( ) ;
                }
            }
            return speedTestResult ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifAbove"></param>
        /// <param name="elseAbove"></param>
        /// <param name="zero"></param>
        /// <returns></returns>
        private static int SetValueIfNotAbove
            ( this int ifAbove , int elseAbove , int zero = 0 )
        {
            return ifAbove > zero
                       ? ifAbove
                       : elseAbove ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ifNull"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [ NotNull ]
        private static string SetValueIfNull
            (
            [ CanBeNull ] this string ifNull ,
            [ NotNull ] string value
            )
        {
            return ifNull ?? value ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <param name="inclusive"></param>
        /// <returns></returns>
        private static bool Between
            (
            this int num ,
            int lower ,
            int upper ,
            bool inclusive = true )
        {
            return inclusive
                       ? lower <= num && num <= upper
                       : lower < num && num < upper ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ NotNull ]
        public static IEnumerable < SpeedTestResult > PerformSpeedTest
            (
            [ NotNull ] SpeedTestsInput speedTestsInput )
        {
            var result = new List < SpeedTestResult >( ) ;
            var networkAdaptersParameters = Handler.GetNetworkAdaptersParameters( ) ;
            {
                var interfacesNumber = networkAdaptersParameters.Count ;
                if (interfacesNumber > 0
                    && speedTestsInput.MethodsInputs != null)
                {
                    // ReSharper disable LoopCanBeConvertedToQuery
                    foreach (var methodsInput in speedTestsInput.MethodsInputs)
                        // ReSharper restore LoopCanBeConvertedToQuery
                    {
                        var testResult = Handler.GetSpeedTestResult
                            (
                                methodsInput);
                        result.Add
                            (
                                testResult);
                    }
                }
            }

            return result ;
        }

        /// <summary>
        /// </summary>
        /// <param name="smallPingSettings"></param>
        /// <param name="bigPingSettings"></param>
        /// <param name="speedTestProgramWithArguments"></param>
        /// <param name="homeParameterIndex"></param>
        /// <param name="wordParameterIndex"></param>
        /// <param name="attemptNumber"></param>
        /// <returns></returns>
        [ NotNull ]
        private static DiagnosticResult GetDiagnosticResult
            (
            [ NotNull ] IList < PingParameters > smallPingSettings ,
            [ NotNull ] IList < PingParameters > bigPingSettings ,
            SpeedTestInput speedTestProgramWithArguments ,
            int homeParameterIndex ,
            int wordParameterIndex ,
            int attemptNumber )
        {
            var diagnosticResult = new DiagnosticResult( ) ;

            var networkAdaptersParameters = Handler.GetNetworkAdaptersParameters( ) ;
            {
                var interfacesNumber = networkAdaptersParameters.Count ;

                if ( interfacesNumber <= 0 )
                {
                    diagnosticResult.ConnectionState =
                        DiagnosticResult.ClientConnectionState.NotFound ;
                }
                else
                {
                    diagnosticResult.ConnectionState =
                        DiagnosticResult.ClientConnectionState.Up ;

                    var networkAdapterParameters =
                        networkAdaptersParameters.FirstOrDefault( )
                        ?? new NetworkAdapterParameters( ) ;

                    var gateway = networkAdapterParameters.Gateway ;

                    var gatewaySmallPingResult = new PingResult( ) ;
                    var homeSmallPingResult = new PingResult( ) ;

                    if ( smallPingSettings[ homeParameterIndex ] != null )
                    {
                        var smallHomePingSettings =
                            smallPingSettings[ homeParameterIndex ] ;
                        var smallGatewayPingSettings = smallHomePingSettings.Copy( ) ;
                        smallGatewayPingSettings.Host = gateway ;
                        gatewaySmallPingResult = Handler.GetPingResult
                            (
                                attemptNumber ,
                                smallGatewayPingSettings ) ;

                        homeSmallPingResult = Handler.GetPingResult
                            (
                                attemptNumber ,
                                smallPingSettings[ homeParameterIndex ] ) ;
                    }

                    var gatewayBigPingResult = new PingResult( ) ;
                    var homeBigPingResult = new PingResult( ) ;

                    if ( bigPingSettings[ homeParameterIndex ] != null )
                    {
                        var bigHomePingSettings =
                            bigPingSettings[ homeParameterIndex ] ;
                        var bigGatewayPingSettings = bigHomePingSettings.Copy( ) ;

                        bigGatewayPingSettings.Host = gateway ;
                        gatewayBigPingResult = Handler.GetPingResult
                            (
                                attemptNumber ,
                                bigGatewayPingSettings ) ;

                        homeBigPingResult = Handler.GetPingResult
                            (
                                attemptNumber ,
                                bigHomePingSettings ) ;
                    }

                    var wordSmallPingResult = new PingResult( ) ;
                    var wordBigPingResult = new PingResult( ) ;

                    if ( smallPingSettings[ wordParameterIndex ] != null )
                    {
                        var smallWordPingSettings =
                            smallPingSettings[ wordParameterIndex ] ;
                        wordSmallPingResult = Handler.GetPingResult
                            (
                                attemptNumber ,
                                smallWordPingSettings ) ;
                    }

                    if ( bigPingSettings[ wordParameterIndex ] != null )
                    {
                        var bigWordPingSettings =
                            bigPingSettings[ wordParameterIndex ] ;
                        wordBigPingResult = Handler.GetPingResult
                            (
                                attemptNumber ,
                                bigWordPingSettings ) ;
                    }

                    var speedTestResult = Handler.GetSpeedTestResult
                        (
                            speedTestProgramWithArguments ) ;

                    diagnosticResult.AdapterName =
                        networkAdapterParameters.AdapterName ;
                    diagnosticResult.BigGatewayFailRatio =
                        gatewayBigPingResult.LostRatio.ToString
                            (
                                CultureInfo.InvariantCulture ) ;
                    diagnosticResult.BigGatewayResponseTime =
                        gatewayBigPingResult.AverageRoundtripTime.ToString
                            (
                                CultureInfo.InvariantCulture ) ;
                    diagnosticResult.BigHomeFailRatio =
                        homeBigPingResult.LostRatio.ToString
                            (
                                CultureInfo.InvariantCulture ) ;
                    diagnosticResult.BigHomeResponseTime =
                        homeBigPingResult.AverageRoundtripTime.ToString
                            (
                                CultureInfo.InvariantCulture ) ;
                    diagnosticResult.BigWordFailRatio =
                        wordBigPingResult.LostRatio.ToString
                            (
                                CultureInfo.InvariantCulture ) ;
                    diagnosticResult.BigWordResponseTime =
                        wordBigPingResult.AverageRoundtripTime.ToString
                            (
                                CultureInfo.InvariantCulture ) ;
                    diagnosticResult.ClientIp = networkAdapterParameters.Address ;
                    diagnosticResult.ClientMac = networkAdapterParameters.MacAddress ;
                    diagnosticResult.ClientSpeedTestIp = speedTestResult.TestIp ;
                    diagnosticResult.LanSpeed =
                        speedTestResult.NetworkSpeedBitsPerSecond ;
                    diagnosticResult.SmallGatewayFailRatio = gatewaySmallPingResult
                        .LostRatio.ToString
                        (
                            CultureInfo.InvariantCulture ) ;
                    diagnosticResult.SmallGatewayResponseTime =
                        gatewaySmallPingResult.AverageRoundtripTime.ToString
                            (
                                CultureInfo.InvariantCulture ) ;
                    diagnosticResult.SmallHomeFailRatio =
                        homeSmallPingResult.LostRatio.ToString
                            (
                                CultureInfo.InvariantCulture ) ;
                    diagnosticResult.SmallHomeResponseTime =
                        homeSmallPingResult.AverageRoundtripTime.ToString
                            (
                                CultureInfo.InvariantCulture ) ;
                    diagnosticResult.SmallWordFailRatio =
                        wordSmallPingResult.LostRatio.ToString
                            (
                                CultureInfo.InvariantCulture ) ;
                    diagnosticResult.SmallWordResponseTime =
                        wordSmallPingResult.AverageRoundtripTime.ToString
                            (
                                CultureInfo.InvariantCulture ) ;
                }
            }

            return diagnosticResult ;
        }

        /// <summary>
        /// </summary>
        /// <param name="attemptNumber"></param>
        /// <param name="smallHomePingSettings"></param>
        /// <returns></returns>
        [ NotNull ]
        private static PingResult GetPingResult
            (
            int attemptNumber ,
            [ NotNull ] PingParameters smallHomePingSettings )
        {
            var pingResult = new PingResult
                (
                smallHomePingSettings ,
                attemptNumber ) ;
            pingResult.Calculate( ) ;

            return pingResult ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ NotNull ]
        public static List < NetworkAdapterParameters >
            GetNetworkAdaptersParameters ( )
        {
            var networkAdaptersParameters =
                new List < NetworkAdapterParameters >( ) ;

            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces( ) ;

            foreach ( var networkInterface in networkInterfaces )
            {
                if ( networkInterface?.OperationalStatus != OperationalStatus.Up )
                {
                    continue ;
                }
                var clientAddress = string.Empty ;
                var clientMask = string.Empty ;
                var clientGateway = string.Empty ;
                var clientDns = string.Empty ;
                var connectionName = string.Empty ;
                var adapterName = string.Empty ;
                var clientMacAddress = string.Empty ;

                if ( ! Handler.GetClientNetworkInterfaceProperties
                           (
                            networkInterface ,
                            ref connectionName ,
                            ref clientAddress ,
                            ref clientMask ,
                            ref clientGateway ,
                            ref clientDns ,
                            ref adapterName ,
                            ref clientMacAddress ) )
                {
                    continue ;
                }
                // ReSharper disable UseObjectOrCollectionInitializer
                var networkAdapterParameters = new NetworkAdapterParameters
                    // ReSharper restore UseObjectOrCollectionInitializer
                    (
                    clientAddress ,
                    clientGateway ,
                    clientMacAddress ,
                    clientDns ) ;
                networkAdapterParameters.AdapterName = adapterName ;
                networkAdaptersParameters.Add
                    (
                     networkAdapterParameters ) ;
            }

            return networkAdaptersParameters ;
        }

        /// <summary>
        /// </summary>
        /// <param name="networkInterface"></param>
        /// <param name="connectionName"></param>
        /// <param name="clientAddress"></param>
        /// <param name="clientAddressMask"></param>
        /// <param name="clientGateway"></param>
        /// <param name="clientDns"></param>
        /// <param name="adapterName"></param>
        /// <param name="clientMacAddress"></param>
        /// <returns></returns>
        private static bool GetClientNetworkInterfaceProperties
            (
            [ CanBeNull ] NetworkInterface networkInterface ,
            ref string connectionName ,
            ref string clientAddress ,
            ref string clientAddressMask ,
            ref string clientGateway ,
            ref string clientDns ,
            ref string adapterName ,
            ref string clientMacAddress )
        {
            var addressFound = false ;
            IPAddress clientIpAddress = null ;
            IPAddress clientDnsAddress = null ;

            if (networkInterface != null)
            {
                if (networkInterface.OperationalStatus != OperationalStatus.Up)
                {
                    return false;
                }
                var ipProperties = networkInterface.GetIPProperties();

                if (ipProperties != null)
                {
                    var gatewaysAddresses = Handler.GetGatewaysIpAddresses
                        (
                            ipProperties);
                    var clientGatewayAddress = Handler
                        .GetOperativeIpFromAddresses
                        (
                            gatewaysAddresses);

                    if (clientGatewayAddress != null)
                    {
                        var dnsAddresses = Handler.GetDnsIpAddresses
                            (
                                ipProperties);
                        clientDnsAddress = Handler.GetOperativeIpFromAddresses
                            (
                                dnsAddresses);
                    }
                    if (clientDnsAddress != null)
                    {
                        var interfaceIpV4Addresses = Handler
                            .GetInterfaceIpV4Addresses
                            (
                                ipProperties);

                        clientIpAddress = Handler.GetClientIpAddress
                            (
                                out clientAddressMask,
                                interfaceIpV4Addresses);
                    }
                    if (clientIpAddress != null)
                    {
                        clientAddress = clientIpAddress.ToString();
                        clientGateway = clientGatewayAddress.ToString();
                        clientMacAddress = networkInterface.GetPhysicalAddress()
                                                           .ToString();
                        clientDns = clientDnsAddress.ToString();
                        if ( networkInterface . Description != null )
                        {
                            adapterName = networkInterface.Description;
                        }
                        if ( networkInterface . Name != null )
                        {
                            connectionName = networkInterface.Name;
                        }
                        addressFound = true;
                    }
                }
            }
            return addressFound ;
        }

        /// <summary>
        /// </summary>
        /// <param name="clientAddressMask"></param>
        /// <param name="interfaceIpV4Addresses"></param>
        /// <returns></returns>
        [CanBeNull]
        private static IPAddress GetClientIpAddress
            (
            out string clientAddressMask,
            [CanBeNull] ICollection<InterfaceIpV4Address> interfaceIpV4Addresses)
        {
            IPAddress clientIpAddress = null;
            clientAddressMask = string.Empty;
            var ipV4InterfacesCount = interfaceIpV4Addresses?.Count;
            if (ipV4InterfacesCount > 0)
            {
                foreach (
                    var interfaceIpV4Address in interfaceIpV4Addresses)
                {
                    var interfaceAddress =
                        interfaceIpV4Address?.InterfaceIpAddress;
                    if (interfaceAddress != null)
                    {
                        clientIpAddress = Handler.GetOperativeIpAddress
                            (
                             interfaceAddress);
                    }
                    if (clientIpAddress == null)
                    {
                        continue;
                    }
                    if ( interfaceIpV4Address . AddressSubnetMask != null )
                    {
                        clientAddressMask =
                            interfaceIpV4Address.AddressSubnetMask;
                    }
                    break;
                }
            }
            return clientIpAddress;
        }

        /// <summary>
        /// </summary>
        /// <param name="ipProperties"></param>
        /// <returns></returns>
        [NotNull]
        private static List<InterfaceIpV4Address> GetInterfaceIpV4Addresses
            (
            [CanBeNull] IPInterfaceProperties ipProperties)
        {
            var interfaceIpV4Addresses =
                new List<InterfaceIpV4Address>();
            var unicastAddresses = ipProperties?.UnicastAddresses;

            var unicastCount = unicastAddresses?.Count;
            if (unicastCount > 0)
            {
                // ReSharper disable LoopCanBeConvertedToQuery
                foreach (var address in unicastAddresses)
                    // ReSharper restore LoopCanBeConvertedToQuery
                {
                    if ( ( address ? . IPv4Mask != null )
                         && ( address . Address != null ) )
                    {
                        var interfaceAddress = new InterfaceIpV4Address
                            (
                            address . Address ,
                            address . IPv4Mask . ToString ( ) ) ;
                        interfaceIpV4Addresses . Add
                            (
                                interfaceAddress ) ;

                    }
                }
            }
            return interfaceIpV4Addresses;
        }

        // ReSharper disable UnusedMember.Local
        /// <summary>
        /// </summary>
        /// <param name="ipProperties"></param>
        /// <returns></returns>
        private static List < IPAddress > GetUnicastAddresses
            (
            [ NotNull ] IPInterfaceProperties ipProperties )
            // ReSharper restore UnusedMember.Local
        {
            var unicastAddressesCollection = ipProperties.UnicastAddresses ;
            var unicastAddresses = new List < IPAddress >( ) ;
            var unicastCount = unicastAddressesCollection?.Count ;
            if ( unicastCount > 0 )
            {
                // ReSharper disable LoopCanBeConvertedToQuery
                foreach ( var address in unicastAddressesCollection )
                    // ReSharper restore LoopCanBeConvertedToQuery
                {
                    if ( address != null )
                    {
                        var ipAddress = address.Address ;
                        unicastAddresses.Add
                            (
                             ipAddress ) ;
                    }
                }
            }
            return unicastAddresses ;
        }

        /// <summary>
        /// </summary>
        /// <param name="ipProperties"></param>
        /// <returns></returns>
        [ NotNull ]
        private static List < IPAddress > GetGatewaysIpAddresses
            (
            [ NotNull ] IPInterfaceProperties ipProperties )
        {
            var gatewaysAddressesCollection = ipProperties.GatewayAddresses ;
            var gatewaysAddresses = new List < IPAddress >( ) ;
            // ReSharper disable LoopCanBeConvertedToQuery
            if ( gatewaysAddressesCollection != null )
            {
                foreach ( var addresses in gatewaysAddressesCollection )
                    // ReSharper restore LoopCanBeConvertedToQuery
                {
                    if ( addresses != null )
                    {
                        var ipAddress = addresses.Address ;
                        gatewaysAddresses.Add
                            (
                             ipAddress ) ;
                    }
                }
            }
            return gatewaysAddresses ;
        }

        /// <summary>
        /// </summary>
        /// <param name="ipProperties"></param>
        /// <returns></returns>
        [ NotNull ]
        private static List < IPAddress > GetDnsIpAddresses
            (
            [ NotNull ] IPInterfaceProperties ipProperties )
        {
            var dnsAddresses = new List < IPAddress >( ) ;
            var dnsAddressesCollection = ipProperties.DnsAddresses ;

            var dnsCount = dnsAddressesCollection?.Count ;
            if ( dnsCount > 0 )
            {
                // ReSharper disable LoopCanBeConvertedToQuery
                foreach ( var addresses in dnsAddressesCollection )
                    // ReSharper restore LoopCanBeConvertedToQuery
                {
                    var ipAddress = addresses ;
                    dnsAddresses.Add
                        (
                         ipAddress ) ;
                }
            }
            return dnsAddresses ;
        }

        /// <summary>
        /// </summary>
        /// <param name="gatewaysAddresses"></param>
        /// <returns></returns>
        [ CanBeNull ]
        private static IPAddress GetOperativeIpFromAddresses
            (
            [ NotNull ] ICollection < IPAddress > gatewaysAddresses )
        {
            IPAddress clientGatewayAddress = null ;

            var gatewaysCount = gatewaysAddresses.Count ;
            if ( gatewaysCount > 0 )
            {
                foreach ( var gatewaysAddress in gatewaysAddresses )
                {
                    if ( gatewaysAddress != null )
                    {
                        clientGatewayAddress = Handler.GetOperativeIpAddress
                            (
                             gatewaysAddress ) ;
                    }
                    if ( clientGatewayAddress != null )
                    {
                        break ;
                    }
                }
            }
            return clientGatewayAddress ;
        }

        /// <summary>
        /// </summary>
        /// <param name="gatewaysAddress"></param>
        /// <returns></returns>
        [ CanBeNull ]
        private static IPAddress GetOperativeIpAddress
            (
            [ NotNull ] IPAddress gatewaysAddress )
        {
            IPAddress clientGatewayAddress = null ;
            var address = gatewaysAddress ;

            if ( address.AddressFamily == AddressFamily.InterNetwork )
            {
                var isServiceAddress = Handler.IsServiceAddress
                    (
                     address ) ;
                if ( ! isServiceAddress )
                {
                    clientGatewayAddress = address ;
                }
            }
            return clientGatewayAddress ;
        }

        /// <summary>
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private static bool IsServiceAddress
            (
            [ NotNull ] IPAddress address )
        {
            // ReSharper disable RedundantNameQualifier
            return ( object.Equals
                         
                         (
                             address,
                             IPAddress.Broadcast)
                     || IPAddress.IsLoopback
                            (
                             address )
                     || object.Equals
                            (
                             address ,
                             IPAddress.None ) ) ;
            // ReSharper restore RedundantNameQualifier
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ CanBeNull ]
        public static BalanceTypeResponse PerformDiagnostic ( )
        {
            var response = new BalanceTypeResponse
                           {
                               Status = Handler.BalanceTypeRequestFail
                           } ;

            if ( Handler.SpeedTestServerAddress != null
                 && Handler.SmallPingSettings != null
                 && Handler.BigPingSettings != null )
            {
                var speedTestInput = new SpeedTestInput
                                     {
                                         Program = Handler.SpeedTestProgram ,
                                         HostName = Handler.SpeedTestServerAddress
                                     } ;

                var diagnosticResult = Handler.GetDiagnosticResult
                    (
                     Handler.SmallPingSettings ,
                     Handler.BigPingSettings ,
                     speedTestInput ,
                     Handler.C_HomeParameterIndex ,
                     Handler.C_WordParameterIndex ,
                     Handler.MeasuresCount ) ;

                if ( diagnosticResult.ConnectionState
                     == DiagnosticResult.ClientConnectionState.Up
                     && Handler.PostDiagnosticsResultFormat != null )
                {
                    var diagnosticsReportString = string.Format
                        (
                         Handler.PostDiagnosticsResultFormat ,
                         diagnosticResult.SmallGatewayResponseTime ,
                         diagnosticResult.SmallHomeResponseTime ,
                         diagnosticResult.SmallWordResponseTime ,
                         diagnosticResult.SmallGatewayFailRatio ,
                         diagnosticResult.SmallHomeFailRatio ,
                         diagnosticResult.SmallWordFailRatio ,
                         diagnosticResult.BigGatewayResponseTime ,
                         diagnosticResult.BigHomeResponseTime ,
                         diagnosticResult.BigWordResponseTime ,
                         diagnosticResult.BigGatewayFailRatio ,
                         diagnosticResult.BigHomeFailRatio ,
                         diagnosticResult.BigWordFailRatio ,
                         diagnosticResult.ClientSpeedTestIp ,
                         diagnosticResult.LanSpeed ,
                         Program.OsName ,
                         diagnosticResult.ClientIp ,
                         diagnosticResult.ClientMac ,
                         diagnosticResult.AdapterName ) ;

                    response = Handler.SendRequestWithBalanceTypeResponce
                        (
                         diagnosticsReportString ) ;
                }
            }

            return response ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thisForm"></param>
        public static void CloseForm
            (
            [ NotNull ] Form thisForm )
        {
            thisForm.Close( ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toolStripMenuItem"></param>
        /// <param name="userStartUpRunApplicationRegistryKey"></param>
        /// <param name="applicationsExecutablePath"></param>
        /// <param name="applicationName"></param>
        /// <param name="registryKeyName"></param>
        public static void ProcessAutoRunOption
            (
            [ CanBeNull ] ToolStripMenuItem toolStripMenuItem ,
            [CanBeNull] RegistryKey userStartUpRunApplicationRegistryKey ,
            [CanBeNull] string applicationsExecutablePath ,
            // ReSharper disable UnusedParameter.Global
            [CanBeNull] string applicationName ,
            // ReSharper restore UnusedParameter.Global
            [CanBeNull] string registryKeyName )
        {

            var registryApplicationsExecutablePath = string.Format
                (
                 "{0}{1}{0}" ,
                 '"' ,
                 applicationsExecutablePath ) ;

            var setUpAutorunResult = Handler.ProcessAutorun
                (
                 Handler.C_SetAutorun,
                 userStartUpRunApplicationRegistryKey,
                 registryApplicationsExecutablePath,
                 registryKeyName);
            if ( toolStripMenuItem != null )
            {
                toolStripMenuItem.Checked = setUpAutorunResult;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setOrDisableAutorun"></param>
        /// <param name="userStartUpRunApplicationRegistryKey"></param>
        /// <param name="setAutoRunPath"></param>
        /// <param name="registryKeyName"></param>
        /// <returns></returns>
        public static bool ProcessAutorun
            (
            bool setOrDisableAutorun ,
            [CanBeNull] RegistryKey userStartUpRunApplicationRegistryKey ,
            [CanBeNull] string setAutoRunPath ,
            [CanBeNull] string registryKeyName )
        {
            var isActionSuccessful = false ;
            if ( setOrDisableAutorun 
                && (setAutoRunPath != null))
            {
                try
                {
                    userStartUpRunApplicationRegistryKey?.SetValue
                        (
                            registryKeyName ,
                            setAutoRunPath ) ;
                    isActionSuccessful = true ;
                }
                catch ( Exception )
                {
                    MessageBox.Show
                        (
                         Handler.AutorunEnablingError ) ;
                }
            }
            else
            {
                try
                {
                    if ( registryKeyName != null )
                    {
                        userStartUpRunApplicationRegistryKey?.DeleteValue
                            (
                                registryKeyName ,
                                false ) ;
                    }
                    isActionSuccessful = true ;
                }
                catch ( Exception )
                {
                    MessageBox.Show
                        (
                         Handler.AutorunDisablingError ) ;
                }
            }

            return isActionSuccessful ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationName"></param>
        /// <param name="applicationStartupPath"></param>
        /// <param name="userStartUpRunApplicationRegistryKey"></param>
        /// <param name="autorunMenuItem"></param>
        /// <param name="applicationFilename"></param>
        /// <param name="applicationFileExtension"></param>
        /// <param name="isSilent"></param>
        /// <param name="applicationVersion"></param>
        /// <param name="registryKeyName"></param>
        /// <param name="updateStorageLocation"></param>
        // ReSharper disable FunctionComplexityOverflow
        // ReSharper disable FunctionComplexityOverflow
        public static bool CheckUpdate
            // ReSharper restore FunctionComplexityOverflow
            // ReSharper restore FunctionComplexityOverflow
            (
            [ CanBeNull ] string applicationName,
            [CanBeNull] string applicationStartupPath,
            [CanBeNull] RegistryKey userStartUpRunApplicationRegistryKey,
            [CanBeNull] ToolStripMenuItem autorunMenuItem,
            [CanBeNull] string applicationFilename,
            [CanBeNull] string applicationFileExtension,
            bool isSilent,
            [CanBeNull] string applicationVersion,
            [CanBeNull] string registryKeyName,
            [CanBeNull] string updateStorageLocation)
        {
            var isUpdateChecked = false;

            var isConnected = Handler.IsConnected();
            if (!isConnected
                && !isSilent)
            {
                MessageBox.Show
                    (
                        Handler.DisconnectOrInvalidConnection);
            }
            var updateVersion = string.Empty;
            if (isConnected)
            {
                updateVersion = Handler.GetFtpFileContent
                    (
                        Handler.UpdateStorageLocation,
                        Handler.UpdateVersionFilename,
                        Handler.AnonymousFtpUserName,
                        Handler.AnonymousPassword
                    );
            }

            if (!string.IsNullOrEmpty
                     (
                         updateVersion))
            {
                isUpdateChecked = true;
            }

            var isUpdateGreaterThanCurrent =
                Handler.IsVersionGreater
                    (
                        updateVersion,
                        applicationVersion);

            var letDownloadUpdate = false;

            if (isUpdateGreaterThanCurrent)
            {
                var userDecision = MessageBox.Show
                    (
                        Handler.NewVersionAvailable,
                        applicationName,
                        Handler.C_MessageBoxButtons,
                        Handler.C_MessageBoxIcon);

                letDownloadUpdate = (userDecision == Handler.C_UserAnswerYes);
            }
            else
            {
                if (!isSilent)
                {
                    MessageBox.Show
                        (
                            Handler.ActualVersionInstalled);
                }
            }

            var updateDestination =
                $"{applicationStartupPath}{Path.DirectorySeparatorChar}{applicationFilename}{updateVersion}{applicationFileExtension}" ;

            var downloadSuccess = false;
            if (letDownloadUpdate)
            {

                var updateFilename =
                    $"{applicationFilename}{applicationFileExtension}" ;
                var updateSource = $"{updateStorageLocation}{updateFilename}" ;

                downloadSuccess = Handler.DownloadUpdate
                    (
                        updateDestination,
                        updateSource);
            }

            var restartApplication = false;

            if (downloadSuccess)
            {
                Handler.ProcessAutorun
                    (
                        Handler.C_SetAutorun,
                        userStartUpRunApplicationRegistryKey,
                        updateDestination,
                        registryKeyName);
                autorunMenuItem.Checked = true;

                var userRestartDecision = MessageBox.Show
                    (
                        Handler.QuestionPerformRestart ,
                        applicationName,
                        Handler.C_MessageBoxButtons,
                        Handler.C_MessageBoxIcon);
                restartApplication = (userRestartDecision
                                      == Handler.C_UserAnswerYes);
            }

            if (restartApplication)
            {
                Handler.RestartApplication(
                    updateDestination);
            }

            return isUpdateChecked;
        }

        /// <summary>
        /// </summary>
        /// <param name="updateDestination"></param>
        private static void RestartApplication
            (
            [CanBeNull] string updateDestination)
        {

            if (updateDestination != null)
            {
                Application.Exit();
                Process.Start
                    (
                        updateDestination);
                Environment.Exit
                    (
                        Program.NormalExitCode);
            }

        }

        /// <summary>
        /// </summary>
        /// <param name="updateDestination"></param>
        /// <param name="updateSource"></param>
        /// <returns></returns>
        private static bool DownloadUpdate
            (
            string updateDestination,
            string updateSource)
        {
            var isDestinationValid = Handler.CheckDestination
                (
                    updateDestination);

            var downloadSuccess = false;

            if ( isDestinationValid
                 && ( Handler.AnonymousFtpUserName != null )
                 && ( Handler.AnonymousPassword != null ) )
            {
                try
                {
                    downloadSuccess = Handler.DownloadFtpFile
                        (
                            updateSource ,
                            updateDestination ,
                            Handler.AnonymousFtpUserName ,
                            Handler.AnonymousPassword ) ;
                }

                catch ( Exception )
                {
                    MessageBox.Show
                        (
                            Handler.DownloadFail ) ;
                }
            }
            return downloadSuccess;
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static bool DownloadFtpFile
            (
            [ CanBeNull ] string source,
            string destination,
            string userName,
            string password)
        {
            var downloadSuccess = false;
            if (source != null)
            {
                var request =
                    (FtpWebRequest) WebRequest.Create
                                        (
                                            source);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential
                    (
                    userName,
                    password);

                const int C_BytesDownloadsPortion = 1024;
                var buffer = new byte[C_BytesDownloadsPortion];
                using (var reader =
                    request.GetResponse()
                           .GetResponseStream())
                {
                    using (var fileStream = new FileStream
                        (
                        destination,
                        FileMode.Create))
                    {
                        if (reader != null)
                        {
                            var wasBytesRead = true;
                            while (wasBytesRead)
                            {
                                var bytesRead = reader.Read
                                    (
                                        buffer,
                                        0,
                                        buffer.Length);
                                wasBytesRead = bytesRead > 0;
                                if (wasBytesRead)
                                {
                                    fileStream.Write
                                        (
                                            buffer,
                                            0,
                                            bytesRead);
                                }
                            }
                            fileStream.Flush();
                            downloadSuccess = true;
                        }
                        fileStream.Close();
                    }
                }
            }
            return downloadSuccess;
        }

        /// <summary>
        /// </summary>
        /// <param name="updateDestination"></param>
        /// <returns></returns>
        private static bool CheckDestination
            (
            [ CanBeNull ] string updateDestination)
        {
            var destinationValid = false ;
            if (updateDestination != null)
            {
                FileInfo destination = null;
                try
                {
                    destination = new FileInfo(updateDestination);
                }
                catch (Exception)
                {
                    
                    //throw;
                }

                if (destination != null)
                {
                    if (destination.Exists)
                    {
                        try
                        {
                            destination.Delete();
                            destinationValid = true;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show
                                (
                                    string.Format
                                        (
                                            Handler.DeletingFileError,
                                            destination));
                        }
                    }
                    else
                    {
                        destinationValid = true;
                    }
                }
            }
            return destinationValid;
        }

        /// <summary>
        /// </summary>
        /// <param name="newVersion"></param>
        /// <param name="currentVersion"></param>
        /// <returns></returns>
        private static bool IsVersionGreater
            (
            [CanBeNull] string newVersion,
            [CanBeNull] string currentVersion)
        {
            var updateGreaterThanCurrent = false;

            if (!string.IsNullOrEmpty
                     (
                         newVersion)
                && !string.IsNullOrEmpty
                        (
                            currentVersion)
                )
            {
                var currentVersionString = currentVersion;

                var current = new Version(currentVersionString);
                var update = new Version(newVersion);

                updateGreaterThanCurrent = update > current;
            }
            return updateGreaterThanCurrent;
        }

        /// <summary>
        /// </summary>
        /// <param name="location"></param>
        /// <param name="filename"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [ CanBeNull ]
        private static string GetFtpFileContent(
            [ CanBeNull ] string location,
            [ CanBeNull ] string filename,
            [ CanBeNull ] string userName,
            [ CanBeNull ] string password)
        {
            
            string updateVersionString = null;
            try
            {
                var updateVersionSource = $"{location}{filename}" ;
                var request = (FtpWebRequest) WebRequest.Create
                                                  (
                                                      updateVersionSource);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials =
                    new NetworkCredential
                        (
                        userName,
                        password);

                var response = (FtpWebResponse) request.GetResponse();

                using (
                    var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        var reader = new StreamReader(responseStream);
                        updateVersionString = reader.ReadToEnd();
                        reader.Close();
                    }
                }
                response.Close();
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
                // ReSharper restore EmptyGeneralCatchClause
            {
                // throw;
            }
            return updateVersionString;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        private static bool IsConnected()
        {
            var result = false;
            var networkAdaptersParameters = Handler.GetNetworkAdaptersParameters();
            var interfacesNumber = networkAdaptersParameters.Count;
            if (interfacesNumber > 0)
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        private static string UpdateVersionFilename { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        public static string DisconnectOrInvalidConnection { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostMenuNotifyIcon"></param>
        /// <param name="balanceString"></param>
        /// <param name="showTime"></param>
        /// <param name="title"></param>
        // ReSharper disable UnusedMethodReturnValue.Global
        public static void SetBalanceMessage
            // ReSharper restore UnusedMethodReturnValue.Global
            ( [ NotNull ] NotifyIcon hostMenuNotifyIcon , [ NotNull ] string balanceString , int showTime , string title )
        {
            var currentBalance = hostMenuNotifyIcon.Text ;
            if ( ! string.Equals
                       (
                        currentBalance ,
                        balanceString ,
                        StringComparison.InvariantCultureIgnoreCase ) )
            {
                hostMenuNotifyIcon.Text = balanceString ;
                hostMenuNotifyIcon.ShowBalloonTip
                    (
                     showTime ,
                     title ,
                     balanceString ,
                     ToolTipIcon.Info ) ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ShowBalance
            (
            [ CanBeNull ] NotifyIcon notifyIcon ,
            Form ownerForm ,
            string applicationName )
        {
            var balanceString = Handler.GetBalance( ) ;
            string balanceMessage ;
            if ( string.IsNullOrEmpty
                (
                 balanceString ) )
            {
                balanceMessage = Handler.RequestBalanceFail ;
            }
            else
            {
                balanceMessage = balanceString ;
                if ( notifyIcon != null )
                {
                    Handler.SetBalanceMessage
                        (
                         notifyIcon ,
                         balanceMessage ,
                         Handler.ShowBalloonTipWithShortMilliseconds ,
                         applicationName ) ;
                }
            }
            var messageForm = new ShowMessageForm
                              {
                                  FormTitle = Handler.ShowBalanceTitle ,
                                  FormMessage = balanceMessage
                              } ;
            messageForm.ShowDialog
                (
                 ownerForm ) ;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ShowContacts
            (
            Form ownerForm
            )
        {
            var messageForm = new ShowMessageForm
            {
                FormTitle = Handler.ShowContactsTitle,
                FormMessage = Handler.ShowContactsMessage
            };
            messageForm.ShowDialog
                (
                 ownerForm);
        }



        /// <summary>
        /// 
        /// </summary>
        private static string RequestBalanceFail { get ; set ; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ CanBeNull ]
        public static string GetBalance ( )
        {
            var balanceString = string.Empty ;

            var networkAdaptersParameters = Handler.GetNetworkAdaptersParameters( ) ;
            var interfacesNumber = networkAdaptersParameters.Count ;
            if ( interfacesNumber > 0 )
            {
                if ( Handler.BalanceRequest != null )
                {
                    var balanceResponce = Handler.SendRequestWithBalanceTypeResponce
                        (
                         Handler.BalanceRequest ) ;
                    if ( balanceResponce != null )
                    {
                        if ( balanceResponce.Status == Handler.BalanceTypeRequestOk )
                        {
                            balanceString = balanceResponce.Message ;
                        }
                    }
                }
            }

            return balanceString ;
        }

        /// <summary>
        /// </summary>
        /// <param name="requestString"></param>
        /// <returns></returns>
        [ CanBeNull ]
        private static BalanceTypeResponse SendRequestWithBalanceTypeResponce
            (
            [ NotNull ] string requestString )
        {
            const string C_PostMethod = "POST" ;
            const string C_PostRequestContentType =
                "application/x-www-form-urlencoded" ;

            var request = ( HttpWebRequest ) WebRequest.Create
                                                 (
                                                  Handler.C_DiagnosticApiWebAddress ) ;
            var data = Encoding.ASCII.GetBytes
                (
                 requestString ) ;

            request.Method = C_PostMethod ;

            request.ContentType = C_PostRequestContentType ;
            request.ContentLength = data.Length ;
            request.Timeout = Handler.PostRequestTimeout ;

            BalanceTypeResponse jsonResponse = null ;

            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write
                        (
                            data,
                            0,
                            data.Length);
                }

                var response = request.GetResponse() as HttpWebResponse;

                if (response?.StatusCode == HttpStatusCode.OK)
                {
                    var jsonSerializer =
                        new DataContractJsonSerializer
                            (typeof (BalanceTypeResponse));

                    using (var stream = response.GetResponseStream())
                    {
                        if (stream != null)
                        {
                            var objResponse = jsonSerializer.ReadObject
                                (
                                 stream);
                            jsonResponse =
                                objResponse as BalanceTypeResponse;
                        }
                    }
                }
            }
            catch (Exception)
            {
                jsonResponse = new BalanceTypeResponse
                               {
                                   Message = string.Empty,
                                   Status = Handler.BalanceTypeRequestFail
                               };
            }
            return jsonResponse ;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void OpenPayWebPage ( )
        {
            Process.Start
                (
                 Handler.C_PayWebPageAddress ) ;
        }
    }
}
