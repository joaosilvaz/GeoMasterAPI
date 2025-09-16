using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoMaster.Domain.Abstractions;

namespace GeoMaster.Domain.Shapes
{
    public class Retangulo : FormaRetangular, ICalculos2D
    {
        public Retangulo(double largura, double altura) : base(largura, altura)
        {
        }

        public double CalcularArea()
        {
            return Largura * Altura;
        }

        public double CalcularPerimetro()
        {
            return 2 * (Largura + Altura);
        }
    }
}