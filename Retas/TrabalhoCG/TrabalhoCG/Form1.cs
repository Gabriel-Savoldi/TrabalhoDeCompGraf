using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoCG
{
    public partial class Form1 : Form
    {
        Image img;
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(1200, 800);
            picBox1.SizeMode = PictureBoxSizeMode.Normal;
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }

            picBox1.Image = bmp;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        void writePixel(int x, int y, Color cor)
        {
            if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
            {
                bmp.SetPixel(x, y, cor);
                picBox1.Refresh();
            }
        }

        public void desenharReta(int x1, int y1, int x2, int y2, Color cor)
        {
            double dy = y2 - y1;
            double dx = x2 - x1;
            double m = dy / dx;

            for (int x = x1; x <= x2; x++)
            {
                {
                    double y = y1 + m * (x - x1);
                    writePixel(x, (int)(Math.Round(y)), cor);
                }

            }
        }
    }
}
