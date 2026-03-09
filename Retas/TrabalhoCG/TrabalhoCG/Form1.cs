using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoCG
{
    public partial class Form1 : Form
    {
        Image img;
        Bitmap bmp;
        private int pixelSize = 3;
        private int x1,y1,x2,y2;
        bool desenhando = false;

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
            x1 = x2 = y1 = y2 = -1;

        }



        private void picBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!desenhando)
            {
                x1 = e.X;
                y1 = e.Y;
                desenhando = true;
            }
            else
            {
                x2 = e.X;
                y2 = e.Y;

                if (x1 > x2 && y1 > y2)
                {
                    //mais moderno e menos ariscado
                    (x1, x2) = (x2, x1);
                    (y1, y2) = (y2, y1);
                }
                if (rbEqReta.Checked)
                    desenharReta(x1, y1, x2, y2, Color.Black);
                else
                    if (rbDDA.Checked)
                    DDA(x1, y1, x2, y2, Color.Red);
                else
                    if (rbPMR.Checked)
                    bresenham1(x1, y1, x2, y2, Color.Blue);
                x1 = x2 = y2 = y1 = -1;

                desenhando = false;
            }
        }

        private void picBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (desenhando)
            {
                Bitmap temp = (Bitmap)bmp.Clone();

                using (Graphics g = Graphics.FromImage(temp))
                {
                    g.DrawLine(Pens.Gray, x1, y1, e.X, e.Y);
                }

                picBox1.Image = temp;
            }
        }

        public void DDA(int x1, int y1, int x2, int y2, Color cor)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;

            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

            float xInc = dx / (float)steps;
            float yInc = dy / (float)steps;

            float x = x1;
            float y = y1;

            BitmapData bitmapDatabmp = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);


            unsafe
            {
                byte* pscan0 = (byte*)bitmapDatabmp.Scan0.ToPointer();
                byte* pos;

                for (int i = 0; i <= steps; i++)
                {
                    int xPos = (int)Math.Round(x);
                    int yPos = (int)Math.Round(y);

                    if (xPos >= 0 && xPos < bmp.Width &&
                        yPos >= 0 && yPos < bmp.Height)
                    {
                        pos = pscan0 + (xPos * 3) + (yPos * bitmapDatabmp.Stride);
                        pos[0] = cor.B;
                        pos[1] = cor.G;
                        pos[2] = cor.R;
                    }

                    x += xInc;
                    y += yInc;
                }
            }

            bmp.UnlockBits(bitmapDatabmp);
            picBox1.Refresh();
        }

        public void bresenham1(int x1, int y1, int x2, int y2, Color cor)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx = (x1 < x2) ? 1 : -1;
            int sy = (y1 < y2) ? 1 : -1;

            int err = dx - dy;

            BitmapData bitmapDatabmp = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* pscan0 = (byte*)bitmapDatabmp.Scan0.ToPointer();
                byte* pos;

                while (true)
                {
                    if (x1 >= 0 && x1 < bmp.Width && y1 >= 0 && y1 < bmp.Height)
                    {
                        pos = pscan0 + (x1 * 3) + (y1 * bitmapDatabmp.Stride);
                        pos[0] = cor.B;
                        pos[1] = cor.G;
                        pos[2] = cor.R;
                    }

                    if (x1 == x2 && y1 == y2)
                        break;

                    int e2 = 2 * err;

                    if (e2 > -dy)
                    {
                        err -= dy;
                        x1 += sx;
                    }

                    if (e2 < dx)
                    {
                        err += dx;
                        y1 += sy;
                    }
                }
            }

            bmp.UnlockBits(bitmapDatabmp);
            picBox1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            rbDDA.Checked = false;
            rbEqCircu.Checked = false;
            rbEqReta.Checked = false;
            rbTrigo.Checked = false;
            rbPMC.Checked = false;
            rbPMR.Checked = false;
        }

        void writeAllPixel(Color cor)
        {
            BitmapData bitmapDatabmp = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
                byte* pscan0 = (byte*)bitmapDatabmp.Scan0.ToPointer();
                byte* pos;
                for (int y = 0;y<bmp.Height;y++)
                {
                    for(int x=0;x<bmp.Width;x++)
                    {
                        pos = pscan0 + (x * 3) + (y * bitmapDatabmp.Stride);
                        pos[0] = cor.B;
                        pos[1] = cor.G;
                        pos[2] = cor.R;
                    }
                }
            }
            bmp.UnlockBits(bitmapDatabmp);
            picBox1.Refresh();
        }

        public void desenharReta(int x1, int y1, int x2, int y2, Color cor)
        {
            double dy = y2 - y1;
            double dx = x2 - x1;

            BitmapData bitmapDatabmp = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* pscan0 = (byte*)bitmapDatabmp.Scan0.ToPointer();
                byte* pos;

                // 🔹 Caso reta vertical
                if (dx == 0)
                {
                    int yStart = Math.Min(y1, y2);
                    int yEnd = Math.Max(y1, y2);

                    for (int y = yStart; y <= yEnd; y++)
                    {
                        pos = pscan0 + (x1 * 3) + (y * bitmapDatabmp.Stride);
                        pos[0] = cor.B;
                        pos[1] = cor.G;
                        pos[2] = cor.R;
                    }
                }
                else
                {
                    double m = dy / dx;

                    if (Math.Abs(dy) > Math.Abs(dx))
                    {
                        int yStart = Math.Min(y1, y2);
                        int yEnd = Math.Max(y1, y2);

                        for (int y = yStart; y <= yEnd; y++)
                        {
                            double x = x1 + (y - y1) / m;
                            int xPos = (int)Math.Round(x);

                            pos = pscan0 + (xPos * 3) + (y * bitmapDatabmp.Stride);
                            pos[0] = cor.B;
                            pos[1] = cor.G;
                            pos[2] = cor.R;
                        }
                    }
                    else
                    {
                        int xStart = Math.Min(x1, x2);
                        int xEnd = Math.Max(x1, x2);

                        for (int x = xStart; x <= xEnd; x++)
                        {
                            double y = y1 + m * (x - x1);
                            int yPos = (int)Math.Round(y);

                            pos = pscan0 + (x * 3) + (yPos * bitmapDatabmp.Stride);
                            pos[0] = cor.B;
                            pos[1] = cor.G;
                            pos[2] = cor.R;
                        }
                    }
                }
            }

            bmp.UnlockBits(bitmapDatabmp);
            picBox1.Refresh();
        }

        
    }
}
