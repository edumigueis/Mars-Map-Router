using apCaminhosMarte.App;
using System;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Form pre = new FrmInit();
                pre.ShowDialog();

                Form app = new FrmApp();
                app.ShowDialog();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}
