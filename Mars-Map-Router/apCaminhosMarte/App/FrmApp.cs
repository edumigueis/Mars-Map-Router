using apCaminhosMarte.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace apCaminhosMarte
{

    public partial class FrmApp : Form
    {

        private Graphics g;
        private Cidade origem;
        private Cidade destino;

        ArvoreBinaria<Cidade> Arvore { get; set; }
        List<AvancoCaminho> Lista { get; set; }

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

        private void FrmApp_Load(object sender, EventArgs e)
        {
            var leitor = new LeitorDeArquivoMarsMap();

            Arvore = leitor.LerCidades();
            Lista = leitor.LerCaminhos();

            var lista = Arvore.ToList();

            List<LsbItems> dataOrigem = new List<LsbItems>();
            List<LsbItems> dataDestino = new List<LsbItems>();

            for (int i = 0; i < lista.Count; i++)
            {
                dataOrigem.Add(new LsbItems(lista[i].Id, lista[i].Nome));
                dataDestino.Add(new LsbItems(lista[i].Id, lista[i].Nome));
            }

            dataDestino.Sort();
            dataOrigem.Sort();

            lsbDestino.DataSource = dataDestino;
            lsbOrigem.DataSource = dataOrigem;

            lsbDestino.DisplayMember = "Text";
            lsbOrigem.DisplayMember = "Text";
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            origem = Arvore.Busca(new Cidade((lsbOrigem.SelectedItem as LsbItems).Id, default, default, default));
            destino = Arvore.Busca(new Cidade((lsbDestino.SelectedItem as LsbItems).Id, default, default, default));


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

        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            DesenharArvore(true, Arvore.Raiz, pictureBox4.Width / 2 - 100, 30, (Math.PI / 180) * 90, 1, 400, "Poppins", e.Graphics);
        }

        private void pbMapa_Paint(object sender, PaintEventArgs e)
        {
            DesenharCidades(e.Graphics, "Poppins");
        }

        private void DesenharArvore(bool primeiraVez, NoArvore<Cidade> raiz, int x, int y, double angulo, double incremento, double comprimento, string font, Graphics g)
        {
            int xf, yf;
            if (raiz != null)
            {
                Pen caneta = new Pen(Color.FromArgb(85, 85, 85), 2);
                xf = (int)Math.Round(x + Math.Cos(angulo) * comprimento);
                yf = (int)Math.Round(y + Math.Sin(angulo) * comprimento);
                if (primeiraVez)
                    yf = 30;
                g.DrawLine(caneta, x, y, xf, yf);
                DesenharArvore(false, raiz.Esq, xf, yf, Math.PI / 2 + incremento,
                incremento * 0.60, comprimento * 0.8, font, g);
                DesenharArvore(false, raiz.Dir, xf, yf, Math.PI / 2 - incremento,
                incremento * 0.60, comprimento * 0.8, font, g);
                SolidBrush preenchimento = new SolidBrush(Color.MediumTurquoise);
                g.FillRectangle(preenchimento, xf - 40, yf, 80, 30);
                g.DrawString(Convert.ToString(raiz.Info.Nome), new Font(font, 7),
                new SolidBrush(Color.White), xf - 35, yf + 8);
            }
        }

        private void DesenharCidades(Graphics g, string font)
        {
            for (int i = 0; i < Lista.Count; i++)
            {
                Pen caneta = new Pen(Color.FromArgb(190, 184, 28, 28), 2);
                caneta.DashStyle = DashStyle.Dash;
                caneta.DashPattern = new float[] { 4.0f, 4.0f, 4.0f, 4.0f };
                caneta.DashCap = DashCap.Round;
                g.DrawLine(caneta, (Lista[i].Origem.X * pbMapa.Width) / 4096, (Lista[i].Origem.Y * pbMapa.Height) / 2048, (Lista[i].Destino.X * pbMapa.Width) / 4096, (Lista[i].Destino.Y * pbMapa.Height) / 2048);
            }

            for (int i = 0; i < Arvore.Qtd; i++)
            {
                var cidade = new Cidade(i, default, default, default);
                int x = (Arvore.Busca(cidade).X * pbMapa.Width) / 4096;
                int y = (Arvore.Busca(cidade).Y * pbMapa.Height) / 2048;

                g.FillRectangle(new SolidBrush(Color.Black), x - 3, y - 3, 6, 6);
                g.DrawString(Arvore.Busca(cidade).Nome, new Font(font, 8, FontStyle.Bold), new SolidBrush(Color.Black), x + 3, y + 2);
            }
        }
    }
}
