using System.Text.Json;
using GeoMaster.Domain.Shapes;

namespace GeoMaster.Application.Factory
{
    public class FormaFactory : IFormaFactory
    {

        private readonly Dictionary<string, Func<JsonElement, object>> _criadoresForma;

        public FormaFactory()
        {
            _criadoresForma = new Dictionary<string, Func<JsonElement, object>>(StringComparer.OrdinalIgnoreCase)
            {
                ["circulo"] = CriarCirculo,
                ["retangulo"] = CriarRetangulo,
                ["esfera"] = CriarEsfera
            };
        }

        public object CriarForma(string tipo, JsonElement propriedades)
        {
            if (!_criadoresForma.TryGetValue(tipo, out var criador))
            {
                throw new ArgumentException($"Tipo de forma '{tipo}' não é suportado");
            }
            return criador(propriedades);
        }

        public bool SuportaTipo(string tipo)
        {
            return _criadoresForma.ContainsKey(tipo);
        }

        private object CriarCirculo(JsonElement props)
        {
            var raio = ValidadorPropriedades.ValidarRaio(props);
            return new Circulo(raio);
        }

        private object CriarRetangulo(JsonElement props)
        {
            var (largura, altura) = ValidadorPropriedades.ValidarLarguraAltura(props);
            return new Retangulo(largura, altura);
        }

        private object CriarEsfera(JsonElement props)
        {
            var raio = ValidadorPropriedades.ValidarRaio(props);
            return new Esfera(raio);
        }

        public object CriarForma(string? tipo, IReadOnlyDictionary<string, double>? props)
        {
            throw new NotImplementedException();
        }
    }
}