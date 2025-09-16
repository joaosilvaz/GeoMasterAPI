using GeoMaster.Domain.Abstractions;
using GeoMaster.Domain.Shapes;

namespace Domain
{
    public class CirculoContivel : Circulo, IFormaContivel
    {
        public CirculoContivel(double raio) : base(raio) { }

        public bool Contem(object formaInterna)
        {
            return formaInterna switch
            {
                CirculoContivel circuloInterno => CirculoPodeConterCirculo(circuloInterno),
                RetanguloContivel retanguloInterno => CirculoPodeConterRetangulo(retanguloInterno),
                _ => false
            };
        }

        public bool Contem(IFormaContivel formaInterna)
        {
            throw new NotImplementedException();
        }

        private bool CirculoPodeConterCirculo(CirculoContivel circuloInterno)
        {
            return Raio >= circuloInterno.Raio;
        }

        private bool CirculoPodeConterRetangulo(RetanguloContivel retangulo)
        {
            var diagonal = Math.Sqrt(Math.Pow(retangulo.Largura, 2) + Math.Pow(retangulo.Altura, 2));
            var raioNecessario = diagonal / 2.0;
            return raioNecessario <= Raio;
        }
    }
}