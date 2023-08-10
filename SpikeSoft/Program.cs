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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.AssemblyResolve += MyHandler;
            Application.ApplicationExit += new EventHandler(AppExit);
            Application.Run(new MainWindow());
        }
        private static Assembly MyHandler(object source, ResolveEventArgs e)
        {
            string assemblypath = AppDomain.CurrentDomain.BaseDirectory + "resources/lib/" + e.Name.Split(",".ToCharArray(), 2)[0] + ".dll";
            if (File.Exists(assemblypath)) return Assembly.LoadFile(assemblypath);
            return null;
        }
        private static void AppExit(object sender, EventArgs e)
        {
            TmpMan.CleanAllTmpFiles();
        }
    }
}
