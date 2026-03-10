using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Poligonos
{
    namespace Poligonos
    {
        public class PoligonoClass
        {

            private int Id;
            public string Nome { get; set; }
            public List<Point> ListaDePontos { get; set; }

            public int getId()
            {
                return Id;
            }
            public PoligonoClass()
            {
                Id = 0;
                ListaDePontos = new List<Point>();  
                Nome = string.Empty;  
            }

            public PoligonoClass(int id,string nome)
            {
                id = id;
                ListaDePontos = new List<Point>();
                Nome = string.Empty;
            }


            public PoligonoClass(string nome, List<Point> pontos)
            {
                Id = 0;
                Nome = nome;
                ListaDePontos = pontos ?? new List<Point>();  
            }

            
            public void AdicionarPonto(Point ponto)
            {
                ListaDePontos.Add(ponto);
            }

            public override string ToString()
            {
                return $"Polígono: {Nome}, Pontos: {ListaDePontos.Count}";
            }
        }
    }
}