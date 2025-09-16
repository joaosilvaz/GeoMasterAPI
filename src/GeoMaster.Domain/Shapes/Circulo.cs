using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoMaster.Domain.Abstractions;

namespace GeoMaster.Domain.Shapes
{
    public class Circulo : FormaCircular, ICalculos2D
    {
        public Circulo(double raio) : base(raio)
        {
        }

        public double CalcularArea()
        {
            return Math.PI * Math.Pow(Raio, 2);
        }

        public double CalcularPerimetro()
        {
            return 2 * Math.PI * Raio;
        }
    }
}