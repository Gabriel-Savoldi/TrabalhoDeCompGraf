using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        private int pixelSize = 3;
        private int x1,y1,x2,y2;

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
            if (x1 != -1)
            {
                x2 = e.X;
                y2 = e.Y;

                if (x1 > x2 && y1 > y2)
                {

                    //mais moderno e menos ariscado
                    (x1, x2) = (x2, x1);
                    (y1, y2) = (y2, y1);

                    /*
                    x1 = x1 + x2;
                    x2 = x1 - x2;
                    x1 = x1 - x2;


                    y1 = y1 + y2;
                    y2 = y1 - y2;
                    y1 = y1 - y2;
                    */

                }
                desenharReta(x1,y1,x2,y2,Color.Black);
                x1=x2= y2 = y1= -1;
            }
            else
            {
                x1 = e.X;
                y1 = e.Y;
            }

        }



        /* NAO USA ESSE TA SEM DMA FICA MUITO PESADO
        void writePixel(int x, int y, Color cor)
        {
            BitmapData bitmapDatabmp = bmp.LockBits(new Rectangle(0,0,bmp.Width,bmp.Height),ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            unsafe
            {
               if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
                {
                    byte* pscan0 = (byte*)bitmapDatabmp.Scan0.ToPointer();
                    pscan0 += (x * 3) + (y * bitmapDatabmp.Stride);
                    pscan0[0] = cor.B;
                    pscan0[1] = cor.G;
                    pscan0[2] = cor.R;
                }
            }

            bmp.UnlockBits(bitmapDatabmp);
            picBox1.Refresh();
        } 
        */


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
            double m = dy / dx;
            BitmapData bitmapDatabmp = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* pscan0 = (byte*)bitmapDatabmp.Scan0.ToPointer();
                byte* pos;

                if (dy>dx) {
                    for (int y = y1; y <= y2; y++)    // ta com erro ele explode quando o y1 = y   dai fica 0/m = Nan e explode o Xpos
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
                    for (int x = x1; x <= x2; x++)  // deve ter o mesmo erro do de cima mas eu n achei até agr
                    {
                        double y = y1 + m * (x - x1);
                        int yPos = (int) Math.Round(y);

                        pos = pscan0 + (x * 3) + (yPos * bitmapDatabmp.Stride);
                        pos[0] = cor.B;
                        pos[1] = cor.G;
                        pos[2] = cor.R;
                    }
                }


            }
            bmp.UnlockBits(bitmapDatabmp);
            picBox1.Refresh();
        }

        private void chekBoxReta_CheckedChanged(object sender, EventArgs e)
        {

            //Color cor = Color.Red;
            //writeAllPixel(cor);
        }
    }
}
