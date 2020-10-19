//Eduardo Migueis - 19167 e Rodrigo Smith - 19197

using apCaminhosMarte.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace apCaminhosMarte
{

    public partial class FrmApp : Form
    {
        private bool achou = false;
        private Graphics g;
        private Cidade origem;
        private Cidade destino;
        private ArvoreBinaria<Cidade> arvore;
        private AvancoCaminho[,] matrizCaminhos;
        private List<AvancoCaminho> listaCaminhos;
        private List<AvancoCaminho[]> resultados;
        private Stack<AvancoCaminho> caminhoEncontrado;
        private AvancoCaminho[] listaMelhorCaminho;

        internal ArvoreBinaria<Cidade> Arvore { get => arvore; set => arvore = value; }
        internal AvancoCaminho[,] MatrizCaminhos { get => matrizCaminhos; set => matrizCaminhos = value; }
        internal List<AvancoCaminho> ListaCaminhos { get => listaCaminhos; set => listaCaminhos = value; }
        internal List<AvancoCaminho[]> Resultados { get => resultados; set => resultados = value; }
        internal Stack<AvancoCaminho> CaminhoEncontrado { get => caminhoEncontrado; set => caminhoEncontrado = value; }
        internal AvancoCaminho[] ListaMelhorCaminho { get => listaMelhorCaminho; set => listaMelhorCaminho = value; }

        private PictureBox pbAnterior = new PictureBox();

        public FrmApp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inicializa o formulário com a interface inicial e padrão.
        /// </summary>
        private void FrmApp_Load(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(255, 60, 80, 185);
            lsbDestino.BackColor = Color.FromArgb(255, 60, 80, 185);
            lsbOrigem.BackColor = Color.FromArgb(255, 60, 80, 185);
            btnBuscar.BackColor = Color.FromArgb(255, 60, 80, 185);

            label5.Visible = false;
            label8.Visible = false;

            dataGridView1.RowHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;

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

        /// <summary>
        /// Lida com o click do botão de buscar.
        /// </summary>
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            achou = false;
            pbMapa.Refresh();

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear(); //limpa dgvs

            if (lsbOrigem.SelectedIndex == lsbDestino.SelectedIndex)
            {
                label3.Visible = false;
                label4.Visible = false;
                dataGridView1.Visible = false;
                dataGridView2.Visible = false;
                label8.Visible = false;
                label5.Visible = true;
                return;
            }

            origem = Arvore.Busca(new Cidade((lsbOrigem.SelectedItem as LsbItems).Id, default, default, default)); // descobre origem
            destino = Arvore.Busca(new Cidade((lsbDestino.SelectedItem as LsbItems).Id, default, default, default)); //descobre destino

            if (!Solucionador.BuscarCaminhos(ref caminhoEncontrado, ref resultados, arvore, origem, destino, ref matrizCaminhos)) // chama o método de solução de caminhos
            { 
                //não achou caminhos
                label5.Visible = false;
                label8.Visible = true;
                label3.Visible = false;
                label4.Visible = false;
                dataGridView1.Visible = false;
                dataGridView2.Visible = false;
                achou = false;
            }
            else
            {
                //achou caminhos
                label5.Visible = false;
                label8.Visible = false;
                label3.Visible = true;
                label4.Visible = true;
                dataGridView1.Visible = true;
                dataGridView2.Visible = true;
                achou = true;
                ExibirTodosOsCaminhosNoDGV();
                ExibirMelhorCaminhoNoDGV();
                pbMapa.Refresh();
            }

        }
        //Ambos os event handlers abaixo para DrawItem são para personalização da cor de seleção das ListBoxes.
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

        /// <summary>
        /// Este event handler verifica qual radioButton está checado para determinar a exibição das linhas entre cidades.
        /// </summary>
        private void pbMapa_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            if (radioButton1.Checked)
            {
                DesenharCidades("Poppins");
            }
            else if (radioButton2.Checked)
            {
                DesenharLinhas();
                DesenharCidades("Poppins");
            }
            else
            {
                DesenharLinhasComSetas();
                DesenharCidades("Poppins");
            }

            if (achou)
            {
                DesenharMelhorCaminho();
                DesenharCidades("Poppins");
            }
        }
        //Inicialização do form
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

            radioButton3.PerformClick();
        }

        /// <summary>
        /// Método que permite que, após redimensionamento, seja possível dar scroll na árvore de cidades.
        /// </summary>
        /// <param name="sender">Auto object sender for event</param>
        /// <param name="e">Auto event argument</param>
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

        private void radioButton1_Click(object sender, EventArgs e)
        {
            pbMapa.Refresh();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            pbMapa.Refresh();
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            pbMapa.Refresh();
        }

        /// <summary>
        /// Método que desenha a árvore de cidades na tela.
        /// </summary>
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

        /// <summary>
        /// Desenha as cidades no mapa de acordo com sua coordenada. 
        /// Usa-se uma regra de 3 para definir a nova coordenada 
        /// baseada na width e height da tela.
        /// </summary>
        /// <param name="font">Fonte a ser usada.</param>
        private void DesenharCidades(string font)
        {
            for (int i = 0; i < Arvore.Qtd; i++)
            {
                var cidade = new Cidade(i, default, default, default);
                int x = (Arvore.Busca(cidade).X * pbMapa.Width) / 4096;
                int y = (Arvore.Busca(cidade).Y * pbMapa.Height) / 2048;

                g.FillRectangle(new SolidBrush(Color.Black), x - 3, y - 3, 6, 6);
                g.DrawString(Arvore.Busca(cidade).Nome, new Font(font, 8, FontStyle.Bold), new SolidBrush(Color.Black), x + 3, y + 2);
            }
        }

        private void DesenharLinhas()
        {
            for (int i = 0; i < ListaCaminhos.Count; i++)
            {
                Pen c = new Pen(Color.DarkRed, 1);
                c.DashStyle = DashStyle.Dash;
                c.DashPattern = new float[] { 4.0f, 4.0f, 4.0f, 4.0f };
                c.DashCap = DashCap.Round;
                g.DrawLine(c, (ListaCaminhos[i].Origem.X * pbMapa.Width) / 4096, (ListaCaminhos[i].Origem.Y * pbMapa.Height) / 2048, (ListaCaminhos[i].Destino.X * pbMapa.Width) / 4096, (ListaCaminhos[i].Destino.Y * pbMapa.Height) / 2048);
            }
        }

        private void DesenharLinhasComSetas()
        {
            for (int i = 0; i < ListaCaminhos.Count; i++)
            {
                Pen c = new Pen(Color.Black, 1f);
                AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 8);
                c.CustomEndCap = bigArrow;
                c.DashStyle = DashStyle.Dash;
                c.DashPattern = new float[] { 4.0f, 4.0f, 4.0f, 4.0f };
                c.DashCap = DashCap.Round;
                g.DrawLine(c, (ListaCaminhos[i].Origem.X * pbMapa.Width) / 4096, (ListaCaminhos[i].Origem.Y * pbMapa.Height) / 2048, (ListaCaminhos[i].Destino.X * pbMapa.Width) / 4096, (ListaCaminhos[i].Destino.Y * pbMapa.Height) / 2048);
            }
        }

        /// <summary>
        /// Exibe todos os caminhos encontrados e armazenados na List resultados nos DataGridViews do form.
        /// </summary>
        private void ExibirTodosOsCaminhosNoDGV()
        {
            var listLength = new List<int>();

            for (int i = 0; i < Resultados.Count; i++)
            {
                listLength.Add(Resultados[i].Length);
            }

            var maxLength = listLength.Max();

            for (int i = 0; i < maxLength + 1; i++)
            {
                dataGridView1.Columns.Add(i + "", i + "");
            }

            for (int i = 0; i < Resultados.Count; i++)
            {
                dataGridView1.Rows.Add();
                int k = Resultados[i].Length - 1;

                for (int j = 0; j < Resultados[i].Length; j++)
                {
                    dataGridView1.Rows[i].Cells[j].Value = Resultados[i][k].Origem.Nome + " ->";

                    if (j == Resultados[i].Length - 1)
                        dataGridView1.Rows[i].Cells[j + 1].Value = Resultados[i][k].Destino.Nome + "";

                    k--;
                }
            }

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = 127;
            }
        }

        /// <summary>
        /// Acessa o melhor caminho e o exibe no DataGridView.
        /// </summary>
        private void ExibirMelhorCaminhoNoDGV()
        {
            ListaMelhorCaminho = Solucionador.BuscarMelhorCaminho(Resultados);

            for (int i = 0; i < ListaMelhorCaminho.Length + 1; i++)
            {
                dataGridView2.Columns.Add(i + "", i + "");
            }

            dataGridView2.Rows.Add();

            int k = ListaMelhorCaminho.Length - 1;

            for (int i = 0; i < ListaMelhorCaminho.Length; i++)
            {
                dataGridView2.Rows[0].Cells[i].Value = ListaMelhorCaminho[k].Origem.Nome + " ->";

                if (i == ListaMelhorCaminho.Length - 1)
                    dataGridView2.Rows[0].Cells[i + 1].Value = ListaMelhorCaminho[k].Destino.Nome + "";

                k--;
            }

            foreach (DataGridViewColumn column in dataGridView2.Columns)
            {
                column.Width = 127;
            }
        }

        /// <summary>
        /// Desenha o melhor caminho na tela usando uma Pen e os mesmos GrA
        /// </summary>
        private void DesenharMelhorCaminho()
        {
            for (int i = 0; i < ListaMelhorCaminho.Length; i++)
            {
                Pen c = new Pen(Color.FromArgb(0, 0, 185), 2);
                AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 8);
                c.CustomEndCap = bigArrow;
                g.DrawLine(c, (ListaMelhorCaminho[i].Origem.X * pbMapa.Width) / 4096, (ListaMelhorCaminho[i].Origem.Y * pbMapa.Height) / 2048, (ListaMelhorCaminho[i].Destino.X * pbMapa.Width) / 4096, (ListaMelhorCaminho[i].Destino.Y * pbMapa.Height) / 2048);
            }
        }
    }
}
