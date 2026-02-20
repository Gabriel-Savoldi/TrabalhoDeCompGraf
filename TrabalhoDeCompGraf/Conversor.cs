using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabalhoDeCompGraf
{
    class Conversor
    {
        /* private static HSI RgbToHsi(int R, int G, int B)
         {
             double r, g, b, h, s, i, H, S, I;
             int soma = R + G + B;
             if (soma != 0)
             {
                 r = (double)R / soma;
                 g = (double)G / soma;
                 b = (double)B / soma;

                 h = Math.Acos((0.5 * ((r - g) + (r - b))) / Math.Pow((Math.Pow((r - g), 2) + (r - b) * (g - b)), 0.5));

                 if (b > g)
                 {
                     h = 2 * Math.PI - h;
                 }


                 if(r==b && b==g) // evitando o numerador ser 0 normalmente quando isso ocorre h=1
                 {
                     h = 0;
                 }

             }
             else
             {
                 r = g = b = h = 0;
             }


             s = 1 - (3 * Math.Min(r, Math.Min(g, b)));



             i = soma / (3 * 255.0);

             H = Math.Round(h * 180 / Math.PI);
             S = Math.Round(s * 100);
             I = Math.Round(i * 255);
             //S = s;
             //I = i;
             return new HSI(H, S, I);

         }*/


        private static HSI RgbToHsi(int R, int G, int B)
        {
            double r = R / 255.0;
            double g = G / 255.0;
            double b = B / 255.0;

            double h = 0.0;
            double s = 0.0;
            double i;

            // ------------------------
            // Intensidade
            // ------------------------
            i = (r + g + b) / 3.0;

            // ------------------------
            // Saturação
            // ------------------------
            double minRGB = Math.Min(r, Math.Min(g, b));

            if (i > 0)
                s = 1 - (minRGB / i);
            else
                s = 0;

            // ------------------------
            // Hue
            // ------------------------
            if (s != 0) // se não for cinza
            {
                double num = 0.5 * ((r - g) + (r - b));
                double den = Math.Sqrt((r - g) * (r - g) +
                                       (r - b) * (g - b));

                if (den != 0)
                {
                    double val = num / den;

                    // Proteção contra erro numérico
                    val = Math.Max(-1.0, Math.Min(1.0, val));

                    double theta = Math.Acos(val);

                    if (b <= g)
                        h = theta;
                    else
                        h = 2 * Math.PI - theta;
                }
                else
                {
                    h = 0;
                }
            }
            else
            {
                h = 0; // pixel cinza
            }

            // ------------------------
            // Conversões finais
            // ------------------------
            double H = Math.Round(h * 180.0 / Math.PI); // graus
            double S = Math.Round(s * 100.0);           // 0-100
            double I = Math.Round(i * 255.0);           // 0-255

            return new HSI(H, S, I);
        }


        public static RGB HsiToRgb(HSI hsi)
        {
            double h, s, i, x, z, y,index=0;
            h = hsi.getH() * Math.PI / 180;
            s = hsi.getS() / 100;
            i = hsi.getI() / 255;

            x = i * (1 - s);

            if (h < 2 * Math.PI / 3)
            {
                index = 0;
            }
            else if (h < 4 * Math.PI / 3)
            {
                h = h - 2 * Math.PI / 3;
                index = 1;
            }
            else
            {
                h = h - 4 * Math.PI / 3;
                index = 2;
            }

            y = i * (1 + (s * Math.Cos(h) / Math.Cos(Math.PI / 3 - h)));
            z = 3 * i - (x + y);
            
            if(index==0)
            {
                return new RGB((int)Math.Round(y * 255), (int)Math.Round(z * 255), (int)Math.Round(x * 255)); ;
            }
            if(index==1)
            {
                return new RGB((int)Math.Round(x * 255), (int)Math.Round(y * 255), (int)Math.Round(z * 255));
            }
            else
            {
                return new RGB((int)Math.Round(z * 255), (int)Math.Round(x * 255), (int)Math.Round(y * 255));
            } 
        }


        public static void ImgRgbToImgHsi(Bitmap imgBitmapSrc, HSI[,] hsi)
        {
            int width = imgBitmapSrc.Width;
            int height = imgBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imgBitmapSrc.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int padding = (bitmapDataSrc.Stride - (width * pixelSize));

            unsafe
            {
                byte* pSrc = (byte*)bitmapDataSrc.Scan0.ToPointer();
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        //na memoria está  b g r 
                        hsi[y, x] = RgbToHsi((int)pSrc[2], (int)pSrc[1], (int)pSrc[0]);
                        pSrc += pixelSize;
                    }
                    pSrc += padding;
                }

            }
            imgBitmapSrc.UnlockBits(bitmapDataSrc);
        }



        public static void RgbToRgb(Bitmap imgBitmapSrc, RGB[,] rgb)
        {
            int width = imgBitmapSrc.Width;
            int height = imgBitmapSrc.Height;
            int pixelSize = 3;

            BitmapData bitmapDataSrc = imgBitmapSrc.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int padding = (bitmapDataSrc.Stride - (width * pixelSize));

            unsafe
            {
                byte* pSrc = (byte*)bitmapDataSrc.Scan0.ToPointer();
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        //na memoria está  b g r 
                        rgb[y, x] = new RGB((int)pSrc[2], (int)pSrc[1], (int)pSrc[0]);
                        pSrc += pixelSize;
                    }
                    pSrc += padding;
                }

            }
            imgBitmapSrc.UnlockBits(bitmapDataSrc);
        }





        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Image MiniaturaR(RGB[,] rgb, int y, int x)
        {
            Bitmap imagem = new Bitmap(x, y);

            for(int i = 0; i < y; i++)
                for(int j = 0; j< x; j++)
                    imagem.SetPixel(j, i, Color.FromArgb(rgb[i,j].getR(), rgb[i, j].getR(), rgb[i, j].getR()));

            return (Image)imagem;
        }

        public static Image MiniaturaG(RGB[,] rgb, int y, int x)
        {
            Bitmap imagem = new Bitmap(x, y);

            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                    imagem.SetPixel(j, i, Color.FromArgb(rgb[i, j].getG(), rgb[i, j].getG(), rgb[i, j].getG()));

            return (Image)imagem;
        }

        public static Image MiniaturaB(RGB[,] rgb, int y, int x)
        {
            Bitmap imagem = new Bitmap(x, y);

            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                    imagem.SetPixel(j, i, Color.FromArgb(rgb[i, j].getB(), rgb[i, j].getB(), rgb[i, j].getB()));

            return (Image)imagem;
        }


        public static Image MiniaturaH(HSI[,] hsi, int y, int x)
        {
            Bitmap imagem = new Bitmap(x, y);
            int cinza;

            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    cinza = (int)((hsi[i, j].getH() / 360.0) * 255);
                    imagem.SetPixel(j, i, Color.FromArgb(cinza, cinza, cinza));
                }

            return (Image)imagem;
        }

        public static Image MiniaturaS(HSI[,] hsi, int y, int x)
        {
            Bitmap imagem = new Bitmap(x, y);
            int cinza;

            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    cinza = (int)((hsi[i, j].getS()/100.0) * 255);
                    cinza = Math.Max(0, Math.Min(255, cinza));
                    imagem.SetPixel(j, i, Color.FromArgb(cinza, cinza, cinza));
                }

            return (Image)imagem;
        }

        public static Image MiniaturaI(HSI[,] hsi, int y, int x)
        {
            Bitmap imagem = new Bitmap(x, y);
            int cinza;

            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    cinza = (int)(hsi[i, j].getI());
                    cinza = Math.Max(0, Math.Min(255, cinza));
                    imagem.SetPixel(j, i, Color.FromArgb(cinza, cinza, cinza));
                }

            return (Image)imagem;
        }


        public static Image SegmentarHue1(RGB[,] rgb, HSI[,] hsi, int y, int x, double inicio, double fim)
        {
            Bitmap imagem = new Bitmap(x, y);
            int cinza;

            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    if (hsi[i, j].getH() >= inicio && hsi[i, j].getH() <= fim && hsi[i, j].getS() > 10)
                        imagem.SetPixel(j, i,Color.FromArgb(rgb[i, j].getR(),rgb[i, j].getG(),rgb[i, j].getB()));
                    else
                    {
                        cinza = (int)(0.299 * rgb[i, j].getR() + 0.587 * rgb[i, j].getG() + 0.114 * rgb[i, j].getB());
                        cinza = Math.Max(0, Math.Min(255, cinza));
                        imagem.SetPixel(j, i, Color.FromArgb(cinza, cinza, cinza));
                    }

                }
            return (Image)imagem;
        }

        public static Image SegmentarHue2(RGB[,] rgb, HSI[,] hsi, int y, int x, double inicio, double fim)
        {

            Bitmap imagem = new Bitmap(x, y);
            int cinza;

            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                {
                    if ((hsi[i, j].getH() >= inicio || hsi[i, j].getH() <= fim) && hsi[i, j].getS() > 10)
                        imagem.SetPixel(j, i, Color.FromArgb(rgb[i, j].getR(), rgb[i, j].getG(), rgb[i, j].getB()));
                    else
                    {
                        cinza = (int)(0.299 * rgb[i, j].getR() + 0.587 * rgb[i, j].getG() + 0.114 * rgb[i, j].getB());
                        cinza = Math.Max(0, Math.Min(255, cinza));
                        imagem.SetPixel(j, i, Color.FromArgb(cinza, cinza, cinza));
                    }
                }
            return (Image)imagem;
        }

        public static void luminancia(RGB[,] rgb, Bitmap imgDest)
        {
            RGB pixel;
            int width = imgDest.Width;
            int height = imgDest.Height;
            BitmapData bitmapDataDst = imgDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {

                int stride = bitmapDataDst.Stride;
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();
                int cinza;
          

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte* p = dst + y * stride + x * 3;
                        pixel = rgb[y,x];
                        cinza = (int)(pixel.getR() * 0.299+ pixel.getG() * 0.587+ pixel.getB() * 0.114);
                        p[0] = p[1] = p[2] = (byte)cinza;
                    }
                }

            }
            imgDest.UnlockBits(bitmapDataDst);
        }

        public static void aumentarMatiz(RGB[,] rgb, HSI[,] hsi, Bitmap imgDest)
        {
            RGB pixelRgb;
            HSI pixelHsi;
            int width = imgDest.Width;
            int height = imgDest.Height;

            BitmapData bitmapDataDst = imgDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                int stride = bitmapDataDst.Stride;
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();
                double H;
                HSI novo;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                            byte* p = dst + y * stride + x * 3;

                            H = hsi[y, x].getH();
                            H = (hsi[y, x].getH() + 10) % 360;
                            hsi[y,x] = new HSI(H, hsi[y, x].getS(), hsi[y, x].getI());

                            pixelRgb = HsiToRgb(hsi[y,x]);
                            p[0] = (byte)pixelRgb.getR();
                            p[1] = (byte)pixelRgb.getG();
                            p[2] = (byte)pixelRgb.getB();
                        
                    }
                }
            }
            imgDest.UnlockBits(bitmapDataDst);
        }

        public static void diminuirMatiz(RGB[,] rgb, HSI[,] hsi, Bitmap imgDest)
        {
            RGB pixelRgb;
            HSI pixelHsi;
            int width = imgDest.Width;
            int height = imgDest.Height;

            BitmapData bitmapDataDst = imgDest.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {

                int stride = bitmapDataDst.Stride;
                byte* dst = (byte*)bitmapDataDst.Scan0.ToPointer();
                double H;
                HSI novo;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        byte* p = dst + y * stride + x * 3;

                        H = hsi[y, x].getH();
                        H = (hsi[y, x].getH() - 10) % 360;
                        hsi[y, x] = new HSI(H, hsi[y, x].getS(), hsi[y, x].getI());

                        pixelRgb = HsiToRgb(hsi[y, x]);
                        p[0] = (byte)pixelRgb.getR();
                        p[1] = (byte)pixelRgb.getG();
                        p[2] = (byte)pixelRgb.getB();

                    }
                }

            }
            imgDest.UnlockBits(bitmapDataDst);
        }

    }
}
