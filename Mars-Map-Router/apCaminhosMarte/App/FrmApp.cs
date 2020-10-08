using System;
using System.Drawing;
using System.Windows.Forms;

namespace apCaminhosMarte
{
    public partial class FrmApp : Form
    {
        public FrmApp()
        {
            InitializeComponent();
            panel2.BackColor = Color.FromArgb(255, 60, 80, 185);
            lsbDestino.BackColor = Color.FromArgb(255, 60, 80, 185);
            lsbOrigem.BackColor = Color.FromArgb(255, 60, 80, 185);
            btnBuscar.BackColor = Color.FromArgb(255, 60, 80, 185);
            dataGridView1.RowHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            pbMapa.BackColor = Color.FromArgb(92, 213, 189);
            dataGridView2.Columns.Add("a", "a");
            dataGridView2.Columns.Add("b", "b");
            dataGridView2.Columns.Add("c", "c");
            dataGridView2.Columns.Add("d", "d");
            dataGridView2.Rows.Add();
            dataGridView2.Rows[0].Cells[0].Value = "Tharsis  ->";
            dataGridView2.Rows[0].Cells[1].Value = "Portholee  ->";
            dataGridView2.Rows[0].Cells[2].Value = "Redsea  ->";
            dataGridView2.Rows[0].Cells[3].Value = "Bothadge";
            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.Width = 120;
            }

            dataGridView1.Columns.Add("a", "a");
            dataGridView1.Columns.Add("b", "b");
            dataGridView1.Columns.Add("c", "c");
            dataGridView1.Columns.Add("d", "d");
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].Cells[0].Value = "Tharsis  ->";
            dataGridView1.Rows[0].Cells[1].Value = "Portholee  ->";
            dataGridView1.Rows[0].Cells[2].Value = "Redsea  ->";
            dataGridView1.Rows[0].Cells[3].Value = "Bothadge";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[1].Cells[0].Value = "Tharsis  ->";
            dataGridView1.Rows[1].Cells[1].Value = "Portholee  ->";
            dataGridView1.Rows[1].Cells[2].Value = "Redsea  ->";
            dataGridView1.Rows[1].Cells[3].Value = "Bothadge";
            dataGridView1.Rows.Add();
            dataGridView1.Rows[2].Cells[0].Value = "Tharsis  ->";
            dataGridView1.Rows[2].Cells[1].Value = "Portholee  ->";
            dataGridView1.Rows[2].Cells[2].Value = "Redsea  ->";
            dataGridView1.Rows[2].Cells[3].Value = "Bothadge";
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = 120;
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Buscar caminhos entre cidades selecionadas");
        }

        private void lsbOrigem_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e = new DrawItemEventArgs(e.Graphics,
                          e.Font,
                          e.Bounds,
                          e.Index,
                          e.State ^ DrawItemState.Selected,
                          e.ForeColor,
                          Color.FromArgb(229, 237, 250));
                e.DrawBackground();
                e.Graphics.DrawString(lsbOrigem.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            }
            else
            {
                e.DrawBackground();
                e.Graphics.DrawString(lsbOrigem.Items[e.Index].ToString(), e.Font, Brushes.WhiteSmoke, e.Bounds, StringFormat.GenericDefault);
            }

            e.DrawFocusRectangle();
        }

        private void lsbDestino_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          Color.FromArgb(229, 237, 250)); 
                                                                         
                e.DrawBackground();
                e.Graphics.DrawString(lsbOrigem.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            }
            else
            {
                e.DrawBackground();
                e.Graphics.DrawString(lsbOrigem.Items[e.Index].ToString(), e.Font, Brushes.WhiteSmoke, e.Bounds, StringFormat.GenericDefault);
            }

            e.DrawFocusRectangle();
        }

    }
}
