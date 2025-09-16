using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoMaster.Domain.Abstractions;

namespace GeoMaster.Domain.Shapes
{
    public class Esfera : FormaCircular, ICalculos3D
    {
        public Esfera(double raio) : base(raio)
        {
        }

        public double CalcularAreaSuperficial()
        {
            return 4 * Math.PI * Math.Pow(Raio, 2);
        }

        public double CalcularVolume()
        {
            return (4.0 / 3.0) * Math.PI * Math.Pow(Raio, 3);
        }
    }
}