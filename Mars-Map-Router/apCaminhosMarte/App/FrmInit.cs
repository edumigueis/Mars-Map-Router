using System;
using System.Drawing;
using System.Windows.Forms;

namespace apCaminhosMarte.App
{
    public partial class FrmInit : Form
    {
        int pb1, pb2, pb3, t1, t2;

        public FrmInit()
        {
            InitializeComponent();
            pb1 = pictureBox1.Location.Y;
            pb2 = pictureBox2.Location.Y;
            pb3 = pictureBox3.Location.Y;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            t1++;
            if (t1 < 40)
            {
                pictureBox1.Location = new Point(pictureBox1.Location.X, pb1++);
                pictureBox2.Location = new Point(pictureBox2.Location.X, pb2++);
            }
            else
            {
                pictureBox1.Location = new Point(pictureBox1.Location.X, pb1--);
                pictureBox2.Location = new Point(pictureBox2.Location.X, pb2--);
            }

            if (t1 == 120)
                t1 = 0;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            t2++;
            if (t2 < 40)
                pictureBox3.Location = new Point(pictureBox3.Location.X, pb3--);
            else
            {
                pictureBox3.Location = new Point(pictureBox3.Location.X, pb3++);
            }

            if (t2 == 120)
                t2 = 0;
        }
    }
}
