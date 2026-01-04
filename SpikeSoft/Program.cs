using SpikeSoft.UtilityManager;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SpikeSoft
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                File.WriteAllText("fatal.log",
                    DateTime.Now + Environment.NewLine +
                    e.ExceptionObject.ToString());
            };

            Application.ThreadException += (s, e) =>
            {
                File.WriteAllText("ui_exception.log",
                    DateTime.Now + Environment.NewLine +
                    e.Exception.ToString());
            };

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.AssemblyResolve += MyHandler;
            Application.ApplicationExit += new EventHandler(AppExit);
            Application.Run(new MainWindow());
        }
        private static Assembly MyHandler(object source, ResolveEventArgs e)
        {
            string fileName = e.Name.Split(',')[0] + ".dll";

            string assemblyPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "resources",
                "lib",
                fileName
            );

            if (!File.Exists(assemblyPath))
                return null;

            return Assembly.LoadFrom(assemblyPath);
        }
        private static void AppExit(object sender, EventArgs e)
        {
            TmpMan.CleanAllTmpFiles();
        }
    }
}
