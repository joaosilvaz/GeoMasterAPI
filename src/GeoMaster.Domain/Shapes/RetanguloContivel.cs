using Domain;
using GeoMaster.Domain.Abstractions;

namespace GeoMaster.Domain.Shapes
{
    public class RetanguloContivel : Retangulo, IFormaContivel
    {
        public RetanguloContivel(double largura, double altura) : base(largura, altura) { }

        public bool Contem(object formaInterna)
        {
            return formaInterna switch
            {
                CirculoContivel circuloInterno => RetanguloPodeConterCirculo(circuloInterno),
                RetanguloContivel retanguloInterno => RetanguloPodeConterRetangulo(retanguloInterno),
                _ => false
            };
        }

        public bool Contem(IFormaContivel formaInterna)
        {
            throw new NotImplementedException();
        }

        private bool RetanguloPodeConterCirculo(CirculoContivel circulo)
        {
            var diametro = circulo.Raio * 2;
            return diametro <= Largura && diametro <= Altura;
        }

        private bool RetanguloPodeConterRetangulo(RetanguloContivel retanguloInterno)
        {
            bool orientacaoNormal = retanguloInterno.Largura <= Largura &&
                               retanguloInterno.Altura <= Altura;

            bool orientacaoRotacionada = retanguloInterno.Largura <= Altura &&
                               retanguloInterno.Altura <= Largura;
            return orientacaoNormal || orientacaoRotacionada;
        }
    }
}