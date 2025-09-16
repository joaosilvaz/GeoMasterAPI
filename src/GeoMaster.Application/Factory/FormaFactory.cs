using System.Text.Json;
using GeoMaster.Domain.Shapes;

namespace GeoMaster.Application.Factory
{
    public sealed class FormaFactory : IFormaFactory
    {
        private readonly IDictionary<string, Func<IReadOnlyDictionary<string, double>, object>> _map;
        private readonly HashSet<string> _tipos;

        public FormaFactory()
        {
            _map = new Dictionary<string, Func<IReadOnlyDictionary<string, double>, object>>(StringComparer.OrdinalIgnoreCase)
            {
                ["circulo"] = p => new Circulo(Req(p, "raio")),
                ["retangulo"] = p => new Retangulo(Req(p, "largura"), Req(p, "altura")),
                ["esfera"] = p => new Esfera(Req(p, "raio"))
            };

            _tipos = new HashSet<string>(_map.Keys, StringComparer.OrdinalIgnoreCase);
        }

        public object CriarForma(string? tipo, IReadOnlyDictionary<string, double>? props)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("Informe o tipo da forma.");
            if (props is null)
                throw new ArgumentException("Informe as propriedades da forma.");

            if (!_map.TryGetValue(tipo, out var build))
                throw new ArgumentException($"Tipo de forma '{tipo}' não é suportado.");

            return build(props);
        }

        public bool SuportaTipo(string tipo) => _tipos.Contains(tipo);

        private static double Req(IReadOnlyDictionary<string, double> p, string key)
        {
            if (!p.TryGetValue(key, out var v) || v <= 0)
                throw new ArgumentException($"Propriedade '{key}' é obrigatória e deve ser > 0.");
            return v;
        }


    }
}
