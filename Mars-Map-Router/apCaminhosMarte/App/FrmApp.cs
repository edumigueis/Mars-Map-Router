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
        private bool[] passou;
        private ArvoreBinaria<Cidade> arvore;
        private AvancoCaminho[,] matrizCaminhos;
        private List<AvancoCaminho> listaCaminhos;
        private List<AvancoCaminho[]> resultados;
        private Stack<AvancoCaminho> caminhoEncontrado;

        internal ArvoreBinaria<Cidade> Arvore { get => arvore; set => arvore = value; }
        internal AvancoCaminho[,] MatrizCaminhos { get => matrizCaminhos; set => matrizCaminhos = value; }
        internal List<AvancoCaminho> ListaCaminhos { get => listaCaminhos; set => listaCaminhos = value; }
        internal List<AvancoCaminho[]> Resultados { get => resultados; set => resultados = value; }
        internal Stack<AvancoCaminho> CaminhoEncontrado { get => caminhoEncontrado; set => caminhoEncontrado = value; }

        private PictureBox pbAnterior = new PictureBox();

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
            ListaCaminhos = leitor.LerCaminhos();
            MatrizCaminhos = leitor.LerCaminhosComoMatriz();

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
            label3.Visible = true;
            label4.Visible = true;
            dataGridView1.Visible = true;
            dataGridView2.Visible = true;
            origem = Arvore.Busca(new Cidade((lsbOrigem.SelectedItem as LsbItems).Id, default, default, default));
            destino = Arvore.Busca(new Cidade((lsbDestino.SelectedItem as LsbItems).Id, default, default, default));

            if (!Solucionador.BuscarCaminhos(ref caminhoEncontrado, ref resultados, ref arvore, ref origem, ref destino, ref passou, ref matrizCaminhos))
            {
                var cu = Resultados;
            }
            else
            {
                var cu = Resultados;
            }


        }

        private void lsbOrigem_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (lsbOrigem.Items.Count == 0)
                return;

            if (e.Index < 0)
                return;

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
            if (lsbDestino.Items.Count == 0)
                return;

            if (e.Index < 0)
                return;

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
                e.Graphics.DrawString(lsbDestino.Items[e.Index].ToString(), e.Font, Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            }
            else
            {
                e.DrawBackground();
                e.Graphics.DrawString(lsbDestino.Items[e.Index].ToString(), e.Font, Brushes.WhiteSmoke, e.Bounds, StringFormat.GenericDefault);
            }

            e.DrawFocusRectangle();
        }

        private void pbMapa_Paint(object sender, PaintEventArgs e)
        {
            DesenharCidades(e.Graphics, "Poppins");
        }

        private void FrmApp_Shown(object sender, EventArgs e)
        {
            var pb = new PictureBox();
            pb.Width = panel7.Width;
            pb.Height = panel7.Height;
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            Bitmap bmp = new Bitmap(panel7.Width, panel7.Height);
            DesenharArvore(true, Arvore.Raiz, bmp.Width / 2, 80, (Math.PI / 180) * 90, 1, 400, "Poppins", Graphics.FromImage(bmp));
            pb.Image = bmp;

            panel7.Controls.Add(pb);

            panel7.Controls[panel7.Controls.IndexOf(panel8)].BringToFront();
        }

        private void FrmApp_Resize(object sender, EventArgs e)
        {
            if (Arvore == null)
                return;

            var pb = new PictureBox();
            pb.Width = panel7.Width;
            pb.Height = panel7.Height;
            pb.SizeMode = PictureBoxSizeMode.AutoSize;
            Bitmap bmp = new Bitmap(panel7.Width, panel7.Height);
            DesenharArvore(true, Arvore.Raiz, bmp.Width / 2, 80, (Math.PI / 180) * 90, 1, 400, "Poppins", Graphics.FromImage(bmp));
            pb.Image = bmp;

            panel7.Controls.Add(pb);

            panel7.Controls[panel7.Controls.IndexOf(panel8)].BringToFront();

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
                    yf = 80;
                else
                    comprimento -= 20;
                g.DrawLine(caneta, x, y, xf, yf);
                DesenharArvore(false, raiz.Esq, xf, yf, Math.PI / 2 + incremento,
                incremento * 0.60, comprimento * 0.8, font, g);
                DesenharArvore(false, raiz.Dir, xf, yf, Math.PI / 2 - incremento,
                incremento * 0.60, comprimento * 0.8, font, g);
                SolidBrush preenchimento = new SolidBrush(Color.MediumTurquoise);
                g.FillRectangle(preenchimento, xf - 45, yf, 90, 30);
                g.DrawString(Convert.ToString(raiz.Info.Nome), new Font(font, 7),
                new SolidBrush(Color.White), xf - 40, yf + 8);
            }
        }

        private void DesenharCidades(Graphics g, string font)
        {
            for (int i = 0; i < ListaCaminhos.Count; i++)
            {
                Pen c = new Pen(Color.Black, 1f);
                AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 8);
                c.CustomEndCap = bigArrow;
                c.DashStyle = DashStyle.Dash;
                c.DashPattern = new float[] { 8.0f, 8.0f, 8.0f, 8.0f };
                c.DashCap = DashCap.Round;
                g.DrawLine(c, (ListaCaminhos[i].Origem.X * pbMapa.Width) / 4096, (ListaCaminhos[i].Origem.Y * pbMapa.Height) / 2048, (ListaCaminhos[i].Destino.X * pbMapa.Width) / 4096, (ListaCaminhos[i].Destino.Y * pbMapa.Height) / 2048);
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
