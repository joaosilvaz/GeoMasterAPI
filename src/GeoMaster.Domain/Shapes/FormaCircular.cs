using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMaster.Domain.Shapes
{
    public abstract class FormaCircular
    {
        public double Raio { get; protected set; }

        protected FormaCircular(double raio)
        {
            ValidarRaio(raio);
            Raio = raio;
        }
        private static void ValidarRaio(double raio)
        {
            if (raio <= 0)
                throw new ArgumentException("O raio deve ser maior que zero");
        }
    }
}