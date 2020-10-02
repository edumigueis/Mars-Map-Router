using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    static class Program
    {
        [DllImport("Shcore.dll")]
        static extern int SetProcessDpiAwareness(int PROCESS_DPI_AWARENESS);

        private enum DpiAwareness
        {
            None = 0,
            SystemAware = 1,
            PerMonitorAware = 2
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SetProcessDpiAwareness((int)DpiAwareness.PerMonitorAware);
            Application.Run(new FrmApp());
        }
    }
}
