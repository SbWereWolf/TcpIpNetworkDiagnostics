using System;
using System.Threading;
using System.Windows.Forms;
using RuskomDiagnostics.Properties;

namespace RuskomDiagnostics
{
    /// <summary>
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// </summary>
        public static int NormalExitCode => -1 ;

        /// <summary>
        /// </summary>
        public static string OsName { get; private set; }

        /// <summary>
        /// </summary>
        public static string ApplicationIdentifier { get ; private set ; }

        /// <summary>
        /// </summary>
        private static int Windows7MajorVersion { get ; set ; }

        /// <summary>
        /// </summary>
        private static int Windows7MinorVersion { get ; set ; }

        static Program ( )
        {
            // ReSharper disable StringLiteralsWordIsNotInDictionary
            const string C_ApplicationIdentificator = "RuskomDiagnostics" ;
            
            const string C_AllowOnlyOneProgramInstance =
                "Одновременно может работать только одна программа диагностики" ;
            // ReSharper restore StringLiteralsWordIsNotInDictionary
            const int C_Windows7MajorVersion = 6 ;
            const int C_Windows7MinorVersion = 1 ;

            Program.ApplicationStartupPath = Application.StartupPath;

            if ( Settings.Default != null )
            {
                Program.ApplicationIdentifier =
                    Settings.Default.ApplicationIdentificator ??
                    C_ApplicationIdentificator ;
                Program.AllowOnlyOneProgramInstance =
                    Settings.Default.ApplicationIdentificator ??
                    C_AllowOnlyOneProgramInstance ;

                Program.Windows7MajorVersion =
                    Settings.Default.Windows7MajorVersion > 0
                        ? Settings.Default.Windows7MajorVersion
                        : C_Windows7MajorVersion ;
                Program.Windows7MinorVersion =
                    Settings.Default.Windows7MinorVersion > 0
                        ? Settings.Default.Windows7MinorVersion
                        : C_Windows7MinorVersion ;
            }
            else
            {
                Program.ApplicationIdentifier = C_ApplicationIdentificator ;
                Program.AllowOnlyOneProgramInstance = C_AllowOnlyOneProgramInstance ;

                Program.Windows7MajorVersion = C_Windows7MajorVersion ;
                Program.Windows7MinorVersion = C_Windows7MinorVersion ;
            }
        }

        /// <summary>
        /// </summary>
        public static string ApplicationStartupPath { get; private set; }

        /// <summary>
        /// </summary>
        private static string AllowOnlyOneProgramInstance { get ; set ; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [ STAThread ]
        private static void Main ( )
        {
            if ( Environment.OSVersion != null )
            {
                if ( Environment.OSVersion.Version != null )
                {
                    Program.OsName = Environment.OSVersion.VersionString ;
                    var osMajorVersion = Environment.OSVersion.Version.Major ;
                    var osMinorVersion = Environment.OSVersion.Version.Minor ;

                    if ( osMajorVersion <= Program.Windows7MajorVersion
                         && osMinorVersion <= Program.Windows7MinorVersion )
                    {
                        bool result ;
                        var mutex = new Mutex
                            ( true , Program.ApplicationIdentifier , out result ) ;

                        if ( ! result )
                        {
                            MessageBox.Show ( Program.AllowOnlyOneProgramInstance ) ;
                            return ;
                        }
                        GC.KeepAlive ( mutex ) ;
                    }
                }
            }

            Application.EnableVisualStyles( ) ;
            Application.SetCompatibleTextRenderingDefault ( false ) ;
            Application.Run ( new RuskomDiagnosticsForm( ) ) ;
        }
    }
}
