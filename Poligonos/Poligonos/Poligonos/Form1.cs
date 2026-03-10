using Poligonos.Poligonos;
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

namespace Poligonos
{
    public partial class Form1 : Form
    {
        Image img;
        Bitmap bmp;
        private int pixelSize = 3;
        private int x1, y1, x2, y2;
        bool desenhando = false,addPoligono=false;
        private int qtdPoligonos = 0;
        List<PoligonoClass> listaPoligonos = new List<PoligonoClass> {};
        PoligonoClass poligonoAtual = null;
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


            string[] nomesPoligonos = { "Polígono 1", "Polígono 2", "Polígono 3" };
           
        }

        private void DestacarBotao(Button botao)
        {
            // Remover destaque de todos os botões
            foreach (Button btn in ((FlowLayoutPanel)botao.Parent).Controls)
            {
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
            }

            // Destacar o botão clicado
            botao.BackColor = Color.White;
            botao.ForeColor = Color.Cyan;
        }

        private void btnAddPoligono_Click(object sender, EventArgs e)
        {
            
            string nome = tbNomePoligono.Text;

            Button btn = new Button
            {
                Name = "poligono" + qtdPoligonos,
                Text = nome,
                Width = 150,
                Height = 40,
                Margin = new Padding(5),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            
            btn.Click += (send, ev) =>
            {
                // Ação que vai executar ao clicar no botão

                picBox1.Image = bmp;
                // Extrair o número (supondo que o número seja sempre de um dígito no final)
                string numeroStr = btn.Name.Substring(btn.Name.Length -1);  // Pega o último caractere

                // Converter para inteiro
                int numero = int.Parse(numeroStr);

                PoligonoClass novoPoli = buscaPoligono(numero);
                if(novoPoli!=null)
                {
                    destacarPoligono(novoPoli);
                }
                //AdicionarPoligono(nome);
                
                // Destaca o botão clicado
                DestacarBotao(btn);
            };


            poligonoAtual = new PoligonoClass(qtdPoligonos,nome);
            // Adiciona o botão ao FlowLayoutPanel
            qtdPoligonos++;
            flowLayoutPanel1.Controls.Add(btn);
            btnAddPoligono.Enabled = false;
            label2.Text = "Desenhe o poligono Usando o clique Esquerdo!\n Para Terminar o desenho Use o clique Direito do mouse!";
            addPoligono = true;
        }


        private PoligonoClass buscaPoligono(int id)
        {



            int i = 0;
            
            while (i < listaPoligonos.Count && listaPoligonos[i].getId() != id)
            {
                int aux = listaPoligonos[i].getId();
                i++;
            }
              if(i<listaPoligonos.Count)
                return listaPoligonos [i];
            return null;
        }
        private void destacarPoligono(PoligonoClass poligono)
        {
            int x1 = -1, x2 = -1, y1 = -1, y2 = -1;
            x1 = x2 = y1 = y2 = -1;

            Bitmap temp = (Bitmap)bmp.Clone();

            Point inicial = poligono.ListaDePontos[0];

            foreach (Point p in poligono.ListaDePontos)
            {
                if (x1 == -1)
                {
                    x1 = p.X;
                    y1 = p.Y;
                }
                else
                {
                    x2 = p.X; y2 = p.Y;
                    if (x1 > x2 && y1 > y2)
                    {
                        //mais moderno e menos ariscado
                        (x1, x2) = (x2, x1);
                        (y1, y2) = (y2, y1);
                        desenharReta(temp, x1, y1, x2, y2, Color.Cyan);

                    }
                    else
                    {
                        desenharReta(temp, x1, y1, x2, y2, Color.Cyan);
                        x1 = x2;
                        y1 = y2;
                    }
                }

            }
            x2=inicial.X; y2 = inicial.Y;
            if (x1 > x2 && y1 > y2)
            {
                //mais moderno e menos ariscado
                (x1, x2) = (x2, x1);
                (y1, y2) = (y2, y1);
                desenharReta(temp, x1, y1, x2, y2, Color.Cyan);

            }
            else
            {
                desenharReta(temp, x1, y1, x2, y2, Color.Cyan);
                x1 = x2;
                y1 = y2;
            }

            picBox1.Image = temp;
            picBox1.Refresh();
        }

        private void picBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (addPoligono)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (!desenhando && x1 == -1)
                    {
                        x1 = e.X;
                        y1 = e.Y;
                        poligonoAtual.AdicionarPonto(new Point(e.X, e.Y));
                        desenhando = true;

                    }
                    else
                    {
                        x2 = e.X;
                        y2 = e.Y;
                        poligonoAtual.AdicionarPonto(new Point(e.X, e.Y));
                        if (x1 > x2 && y1 > y2)
                        {
                            //mais moderno e menos ariscado
                            (x1, x2) = (x2, x1);
                            (y1, y2) = (y2, y1);
                            desenharReta(bmp, x1, y1, x2, y2, Color.Black);

                        }
                        else
                        {
                            desenharReta(bmp, x1, y1, x2, y2, Color.Black);
                            x1 = x2;
                            y1 = y2;
                        }


                        x2 = y2=-1;

                        picBox1.Image = bmp;
                        picBox1.Refresh();
                    }
                }
                else if(e.Button == MouseButtons.Right)
                {
                    picBox1.Image = bmp;
                    picBox1.Refresh();
                    desenhando= addPoligono = false;
                    btnAddPoligono.Enabled = true;
                    

                    Point ponto = poligonoAtual.ListaDePontos[0];
                    x2 = ponto.X;
                    y2 = ponto.Y;
                    if (x1 > x2 && y1 > y2)
                    {
                        //mais moderno e menos ariscado
                        (x1, x2) = (x2, x1);
                        (y1, y2) = (y2, y1);
                    }
                    desenharReta(bmp, x1, y1, x2, y2, Color.Black);

                    listaPoligonos.Add(poligonoAtual);
                    poligonoAtual = null;

                    x1 = x2 = y1 = y2 = -1;
                }
            }
        }

        private void picBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (desenhando)
            {
                Bitmap temp = (Bitmap)bmp.Clone();
                desenharReta(temp, x1, y1, e.X, e.Y, Color.Gray);
                picBox1.Image = temp;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void desenharReta(Bitmap alvo, int x1, int y1, int x2, int y2, Color cor)
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
