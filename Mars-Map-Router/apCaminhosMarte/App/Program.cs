using apCaminhosMarte.App;
using System;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [STAThread]
        static void Main()
        {
            //Este try catch tem por objetivo receber excessões inesperadas causadas pelo programa,
            //como por exemplo uma falta de memória, um problema com os arquivos e etc. A ideia é 
            //que o usuário não receba um erro enorme em sua tela.
            try
            {
                if (Environment.OSVersion.Version.Major >= 6)
                    SetProcessDPIAware();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Form pre = new FrmInit();
                pre.ShowDialog();

                Form app = new FrmApp();
                app.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show("Algum erro inesperado aconteceu. Por favor, tente novamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}
