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
        Bitmap img;
        Bitmap bmp;
        private int pixelSize = 3;
        private int x1, y1, x2, y2;
        bool desenhando = false;
        List<Point> points = new List<Point>
        {
            new Point(-1,-1),
            new Point(0,-1),
            new Point(1,-1),
            new Point(-1,0),
            new Point(1,0),
            new Point(-1,1),
            new Point(0,1),
            new Point(1,1)
        };



        public Form1()
        {
            InitializeComponent();
            
            bmp = new Bitmap(1200, 800);
            
            picBox1.SizeMode = PictureBoxSizeMode.Normal;
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
            }
            img = new Bitmap(1200, 800);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.Clear(Color.White);
            }
            picBox1.Image = bmp;
            x1 = x2 = y1 = y2 = -1;

        }

        private void picBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!desenhando && x1==-1)  
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
                    desenharReta(bmp,x1, y1, x2, y2, Color.Black);
                else
                    if (rbDDA.Checked)
                    DDA(bmp,x1, y1, x2, y2, Color.Red);
                else
                    if (rbPMR.Checked)
                    bresenham1(bmp, x1, y1, x2, y2, Color.Blue);
                else if(rbPMC.Checked)
                    circuPM(bmp, x1, y1, x2, y2,Color.Red);
                else if (rbEqCircu.Checked)
                    circuEq(bmp, x1, y1, x2, y2, Color.Red);
                else if (rbTrigo.Checked)
                    circuTrig(bmp, x1, y1, x2, y2, Color.Red);
                else if(rbElipse.Checked)
                    MidpointElipse(bmp, x1, y1, x2, y2, Color.Green);


                x1 = x2 = y2 = y1 = -1;
                picBox1.Image = bmp;
                picBox1.Refresh();

                desenhando = false;
            }
        }

        private void picBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (desenhando)
            {
                Bitmap temp = (Bitmap)bmp.Clone();

                if (rbEqReta.Checked)
                    desenharReta(temp, x1, y1, e.X, e.Y, Color.Gray);
                else if (rbDDA.Checked)
                    DDA(temp, x1, y1, e.X, e.Y, Color.Gray);
                else if (rbPMR.Checked)
                    bresenham1(temp, x1, y1, e.X, e.Y, Color.Gray);
                else if(rbPMC.Checked)
                    circuPM(temp, x1, y1, e.X, e.Y, Color.Gray);
                else if(rbElipse.Checked)
                    MidpointElipse(temp, x1, y1, e.X, e.Y, Color.Gray);
                else if (rbEqCircu.Checked)
                    circuEq(temp, x1, y1, e.X, e.Y, Color.Gray);
                else if (rbTrigo.Checked)
                    circuTrig(temp, x1, y1, e.X, e.Y, Color.Gray);


                picBox1.Image = temp;
            }
        }

        public void circuTrig(Bitmap alvo, int x1, int y1, int x2, int y2, Color cor)
        {
            int r = (int)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

            BitmapData bitmapDatabmp = alvo.LockBits(
                new Rectangle(0, 0, alvo.Width, alvo.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* p = (byte*)bitmapDatabmp.Scan0.ToPointer();

                for (double ang = 0; ang < 2 * Math.PI; ang += 0.01)
                {
                    int x = (int)(r * Math.Cos(ang));
                    int y = (int)(r * Math.Sin(ang));

                    pintarPixel(bitmapDatabmp, x1 + x, y1 + y, cor, p);
                }
            }

            alvo.UnlockBits(bitmapDatabmp);
            picBox1.Refresh();
        }

        public void circuEq(Bitmap alvo, int x1, int y1, int x2, int y2, Color cor)
        {
            int r = (int)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));

            BitmapData bitmapDatabmp = alvo.LockBits(
                new Rectangle(0, 0, alvo.Width, alvo.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* p = (byte*)bitmapDatabmp.Scan0.ToPointer();

                for (int x = 0; x <= r; x++)
                {
                    int y = (int)Math.Sqrt(r * r - x * x);

                    pintarPM(bitmapDatabmp, x1, y1, x, y, cor, p);
                }
            }

            alvo.UnlockBits(bitmapDatabmp);
            picBox1.Refresh();
        }


        public void circuPM(Bitmap alvo, int x1, int y1, int x2, int y2, Color cor)
        {
            int x = 0;

            int r = (int)Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            int y = r;

            int d = 1 - r;

            BitmapData bitmapDatabmp = alvo.LockBits(
                new Rectangle(0, 0, alvo.Width, alvo.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* p = (byte*)bitmapDatabmp.Scan0.ToPointer();

                pintarPM(bitmapDatabmp, x1, y1, x, y, cor, p);

                while (x <= y)
                {
                    if (d < 0)
                    {
                        d += 2 * x + 3;
                    }
                    else
                    {
                        d += 2 * (x - y) + 5;
                        y--;
                    }
                    x++;
                    pintarPM(bitmapDatabmp, x1, y1, x, y, cor, p);
                }
            }

            alvo.UnlockBits(bitmapDatabmp);
            picBox1.Refresh();
        }

        unsafe private void pintarPixel(BitmapData img, int x, int y, Color cor, byte* p)
        {
            if (x > 0 && y > 0 && x < img.Width && y < img.Height)
            {

                byte* pos = p + x * pixelSize + y * img.Stride;
                pos[0] = cor.B;
                pos[1] = cor.G;
                pos[2] = cor.R;
            }
        }


        unsafe public void ElipsePoints(BitmapData img,int xc,int yc,int x, int y, Color cor,byte* p)
        {
            pintarPixel(img,xc + x, yc + y, cor,p);
            pintarPixel(img, xc - x, yc + y, cor,p);
            pintarPixel(img, xc + x, yc - y, cor,p);
            pintarPixel(img, xc - x, yc - y, cor,p);
        }

        /* end CirclePoints */
        void MidpointElipse(Bitmap alvo, int x1, int y1, int x2, int y2, Color cor)
        {

            BitmapData bitmapDatabmp = alvo.LockBits(
                new Rectangle(0, 0, alvo.Width, alvo.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* p = (byte*)bitmapDatabmp.Scan0.ToPointer();
                int x, y;
                int a = Math.Abs(x2 - x1);
                int b = Math.Abs(y2 - y1);
                double d1, d2;
                /* Valores iniciais */
                x = 0;
                y = b;
                d1 = b * b - a * a * b +  a * a/4.0;
                ElipsePoints(bitmapDatabmp,x1,y1, x, y, cor,p); /* Simetria de ordem 4 */
                while (a * a * (y - 0.5) > b * b * (x + 1))
                {
                    /* Regi~ao 1 */
                    if (d1 < 0)
                    {
                        d1 = d1 + b * b * (2 * x + 3);
                        x++;
                    }
                    else
                    {
                        d1 = d1 + b * b * (2 * x + 3) + a * a * (-2 * y + 2);
                        x++;
                        y--;
                    }/*end if*/
                    ElipsePoints(bitmapDatabmp, x1, y1, x, y, cor, p); /* Simetria de ordem 4 */
                }/* end while */
                d2 = b * b * (x + 0.5) * (x + 0.5) + a * a * (y - 1) * (y - 1) - a * a * b * b;
                while (y > 0)
                {
                    /* Regi~ao 2 */
                    if (d2 < 0)
                    {
                        d2 = d2 + b * b * (2 * x + 2) + a * a * (-2 * y + 3);
                        x++;
                        y--;
                    }
                    else
                    {
                        d2 = d2 + a * a * (-2 * y + 3);
                        y--;
                    }/*end if*/
                    ElipsePoints(bitmapDatabmp, x1, y1, x, y, cor, p); /* Simetria de ordem 4 */
                }/* end while */
            }
            alvo.UnlockBits(bitmapDatabmp);
        }/*end MidpointElipse*/


        unsafe private void pintarPM(BitmapData img, int xc, int yc, int x, int y, Color cor, byte* p)
        {
            pintarPixel(img, xc + x, yc + y, cor, p);
            pintarPixel(img, xc + y, yc + x, cor, p);
            pintarPixel(img, xc - x, yc + y, cor, p);
            pintarPixel(img, xc - y, yc + x, cor, p);
            pintarPixel(img, xc - x, yc - y, cor, p);
            pintarPixel(img, xc - y, yc - x, cor, p);
            pintarPixel(img, xc + x , yc - y, cor, p);
            pintarPixel(img, xc + y, yc - x, cor, p);
        }

        public void DDA(Bitmap alvo,int x1, int y1, int x2, int y2, Color cor)
        {
            int dx = x2 - x1;
            int dy = y2 - y1;

            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

            float xInc = dx / (float)steps;
            float yInc = dy / (float)steps;

            float x = x1;
            float y = y1;

            BitmapData bitmapDatabmp = alvo.LockBits(
                new Rectangle(0, 0, alvo.Width, alvo.Height),
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
            alvo.UnlockBits(bitmapDatabmp);
            picBox1.Refresh();
        }

        public void bresenham1(Bitmap alvo,int x1, int y1, int x2, int y2, Color cor)
        {
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx = (x1 < x2) ? 1 : -1;
            int sy = (y1 < y2) ? 1 : -1;

            int err = dx - dy;

            BitmapData bitmapDatabmp = alvo.LockBits(
                new Rectangle(0, 0, alvo.Width, alvo.Height),
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

            alvo.UnlockBits(bitmapDatabmp);
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
            bmp = new Bitmap(img);
            picBox1.Image = bmp;
            desenhando = false;
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

        public void desenharReta(Bitmap alvo,int x1, int y1, int x2, int y2, Color cor)
        {
            double dy = y2 - y1;
            double dx = x2 - x1;

            BitmapData bitmapDatabmp = alvo.LockBits(
                new Rectangle(0, 0, alvo.Width, alvo.Height),
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
            alvo.UnlockBits(bitmapDatabmp);
            picBox1.Refresh();
        }
    }
}
