using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SpikeSoft.UtilityManager
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.AssemblyResolve += MyHandler;
        }

        private static Assembly MyHandler(object source, ResolveEventArgs e)
        {
            //The namespace of the project is embeddll, and the embedded dll resources are in the libs folder, so the namespace used here is: embeddll.libs.
            string _resName = "SpikeSoft.UtilityManager.libs." + new AssemblyName(e.Name).Name + ".dll";

            if  (Assembly.GetExecutingAssembly().GetManifestResourceStream(_resName) == null)
            {
                string assemblypath = AppDomain.CurrentDomain.BaseDirectory + "resources/lib/" + e.Name.Split(",".ToCharArray(), 2)[0] + ".dll";
                if (File.Exists(assemblypath)) return Assembly.LoadFile(assemblypath);
                return null;
            }

            using (var _stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_resName))
            {
                byte[] _data = new byte[_stream.Length];
                _stream.Read(_data, 0, _data.Length);
                return Assembly.Load(_data);
            }
        }
    }
}
