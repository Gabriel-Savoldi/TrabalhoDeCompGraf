using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Image = System.Drawing.Image;

namespace TrabalhoDeCompGraf
{
    public struct HSI
    {
        double H, S, I;

        public double getH()
        {
            return H;
        }

        public double getS()
        {
            return S;
        }

        public double getI()
        {
            return I;
        }

        public HSI(double Hue, double Saturation, double Intensity)
        {
            H = Hue;
            S = Saturation;
            I = Intensity;
        }
    }


    public struct RGB
    {
        int R, G, B;

        public int getR()
        {
            return R;
        }

        public int getG()
        {
            return G;
        }

        public int getB()
        {
            return B;
        }

        public RGB(int Red, int Green, int Blue)
        {
            R = Red;
            G = Green;
            B = Blue;
        }
    }

    public partial class Form1 : Form
    {
        private Bitmap imageBitmap;
        private Image image;
        private HSI[,] imgHSI;
        private RGB[,] imgRGB;


        public int GetFormHeight()
        {
            return this.Height;
        }

        public int GetFormWidth()
        {
            return this.Width;
        }

        public void inicializar()
        {
            picBoxImg1.Visible = true;

            pictureBoxH.Visible = false;
            pictureBoxS.Visible = false;
            pictureBoxI.Visible = false;

            labelH.Visible = false;
            labelS.Visible = false;
            labelI.Visible = false;


            pictureBoxR.Visible = false;
            pictureBoxG.Visible = false;
            pictureBoxB.Visible = false;

            labelR.Visible = false;
            labelG.Visible = false;
            labelB.Visible = false;

           
        }


        public Form1()
        {
            InitializeComponent();
            picBoxImg1.SizeMode = PictureBoxSizeMode.Normal;
            picBoxZoom.Visible = false;
            labelX.Visible = false;

            inicializar();

            trackBarInicio.SetRange(0, 360);
            trackBarFim.SetRange(0, 360);
        }


        private void btnAbrirImg_Click(object sender, EventArgs e)
        {
            inicializar();
            trackBarFim.Value = 0;
            trackBarInicio.Value = 0;
            labelGrauInicio.Text = "0°";
            labelGrauFim.Text = "0°";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Escolha uma imagem";
            openFileDialog.Filter = "Arquivos de Imagem (*.jpg;*.gif;*.bmp;*.png) |*.jpg;*.gif;*.bmp;*.png; ";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                image = Image.FromFile(openFileDialog.FileName);
                picBoxImg1.Image = image;
                picBoxZoom.Image = null;
                imageBitmap = (Bitmap)image;
                imgHSI = new HSI[image.Height, image.Width];
                imgRGB = new RGB[image.Height, image.Width];
                Conversor.ImgRgbToImgHsi(imageBitmap, imgHSI);
                Conversor.RgbToRgb(imageBitmap,imgRGB);
            }
        }

        private void picBoxImg1_MouseMove(object sender, MouseEventArgs e)
        {
            if (picBoxImg1.Image != null)
            {

                if (e.X >= 0 && e.X < picBoxImg1.Image.Width && e.Y >= 0 && e.Y < picBoxImg1.Image.Height && checkboxColorPick.Checked == true)
                {
                    //muda as label do HSI
                    labHue.Text = "" + imgHSI[e.Y, e.X].getH();
                    labSaturation.Text = "" + imgHSI[e.Y, e.X].getS();
                    labIntensity.Text = "" + imgHSI[e.Y, e.X].getI();

                    //muda as label do RGB
                    labRed.Text = "" + imgRGB[e.Y, e.X].getR();
                    labBlue.Text = "" + imgRGB[e.Y, e.X].getB();
                    labGreen.Text = "" + imgRGB[e.Y, e.X].getG();


                    
                    //muda as label do CMY
                    labC.Text = "" + (255 - imgRGB[e.Y, e.X].getR());
                    labM.Text = "" + (255 - imgRGB[e.Y, e.X].getG());
                    labY.Text = "" + (255 - imgRGB[e.Y, e.X].getB());



                    Point pt = picBoxImg1.PointToClient(MousePosition);


                    // Exibe o zoom da área em torno do mouse
                    ShowZoom(pt.X, pt.Y);
                }

            }
        }


        private void checkboxColorPick_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxColorPick.Checked)
            {
                this.Cursor = Cursors.Cross;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void ShowZoom(int x, int y)
        {
            int zoomArea = 3;   // área real capturada (menor = mais zoom)
            int zoomSize = 100;  // tamanho da lupa na tela

            Rectangle zoomRect = new Rectangle(
                x - zoomArea / 2,
                y - zoomArea / 2,
                zoomArea,
                zoomArea
            );

            Bitmap zoomBitmap = new Bitmap(zoomSize, zoomSize);

            using (Graphics g = Graphics.FromImage(zoomBitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

                g.DrawImage(
                    picBoxImg1.Image, // IMPORTANTE: usar a imagem original
                    new Rectangle(0, 0, zoomSize, zoomSize),
                    zoomRect,
                    GraphicsUnit.Pixel
                );
            }

            picBoxZoom.Image = zoomBitmap;
        }


        private void picBoxImg1_MouseClick(object sender, MouseEventArgs e)
        {
            checkboxColorPick.Checked = false;
        }

        private void picBoxImg1_MouseEnter(object sender, EventArgs e)
        {
            if (checkboxColorPick.Checked)
            {
                picBoxZoom.Visible = true;
                labelX.Visible= true;
            }
        }

        private void picBoxImg1_MouseLeave(object sender, EventArgs e)
        {
            //picBoxZoom.Visible = false;
        }


        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btnExibir_Click(object sender, EventArgs e)
        {

            if (picBoxImg1.Image != null)
            {
                Image imagem;

                picBoxImg1.Visible = false;

                pictureBoxH.Visible = true;
                pictureBoxS.Visible = true;
                pictureBoxI.Visible = true;

                labelH.Visible = true;
                labelS.Visible = true;
                labelI.Visible = true;


                pictureBoxR.Visible = true;
                pictureBoxG.Visible = true;
                pictureBoxB.Visible = true;

                labelR.Visible = true;
                labelG.Visible = true;
                labelB.Visible = true;

                // miniatura R
                imagem = Conversor.MiniaturaR(imgRGB, imageBitmap.Height, imageBitmap.Width);
                pictureBoxR.Image = imagem;
                // miniatura G
                imagem = Conversor.MiniaturaG(imgRGB, imageBitmap.Height, imageBitmap.Width);
                pictureBoxG.Image = imagem;
                // miniatura B
                imagem = Conversor.MiniaturaB(imgRGB, imageBitmap.Height, imageBitmap.Width);
                pictureBoxB.Image = imagem;


                // miniatura H
                imagem = Conversor.MiniaturaH(imgHSI, imageBitmap.Height, imageBitmap.Width);
                pictureBoxH.Image = imagem;
                // miniatura S
                imagem = Conversor.MiniaturaS(imgHSI, imageBitmap.Height, imageBitmap.Width);
                pictureBoxS.Image = imagem;
                // miniatura I
                imagem = Conversor.MiniaturaI(imgHSI, imageBitmap.Height, imageBitmap.Width);
                pictureBoxI.Image = imagem;
            }

        }

        private void btnSegmentar_Click(object sender, EventArgs e)
        {
            inicializar();
            if (picBoxImg1.Image != null)
            {
                Image imagem;
                if (trackBarInicio.Value <= trackBarFim.Value)
                    imagem = Conversor.SegmentarHue1(imgRGB, imgHSI, imageBitmap.Height, imageBitmap.Width, trackBarInicio.Value, trackBarFim.Value);
                else
                    imagem = Conversor.SegmentarHue2(imgRGB, imgHSI, imageBitmap.Height, imageBitmap.Width, trackBarInicio.Value, trackBarFim.Value);
                picBoxImg1.Image = imagem;
            }
        }

        private void trackBarInicio_Scroll(object sender, EventArgs e)
        {
            labelGrauInicio.Text = trackBarInicio.Value.ToString()+"°";
        }

        private void trackBarFim_Scroll(object sender, EventArgs e)
        {
            labelGrauFim.Text = trackBarFim.Value.ToString() + "°";
        }

        private void imgOriginal_btn(object sender, EventArgs e)
        {
            picBoxImg1.Image = image;
        }

        private void btnLuminancia_click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            Conversor.luminancia(imgRGB, imgDest);
            picBoxImg1.Image = imgDest;
        }

        private void aumentarMatiz_click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            Conversor.aumentarMatiz(imgRGB, imgHSI, imgDest);
            picBoxImg1.Image = imgDest;
        }

        private void diminuitMatiz_click(object sender, EventArgs e)
        {
            Bitmap imgDest = new Bitmap(image);
            Conversor.diminuirMatiz(imgRGB, imgHSI, imgDest);
            picBoxImg1.Image = imgDest;
        }
    }
    }

