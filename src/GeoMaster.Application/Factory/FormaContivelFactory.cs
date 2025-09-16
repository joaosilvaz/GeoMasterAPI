using GeoMaster.Application.Factory;
using GeoMaster.Domain.Abstractions;
using GeoMaster.Domain.Shapes;

namespace GeoMaster.Application.Factory
{
    public sealed class FormaContivelFactory : IFormaContivelFactory
    {
        private readonly IDictionary<string, Func<IReadOnlyDictionary<string, double>, IFormaContivel>> _map
            = new Dictionary<string, Func<IReadOnlyDictionary<string, double>, IFormaContivel>>(StringComparer.OrdinalIgnoreCase)
            {
                ["circulo"] = p => new Circulo(Req(p, "raio")),
                ["retangulo"] = p => new Retangulo(Req(p, "largura"), Req(p, "altura"))
            };

        public IFormaContivel CriarFormaContivel(string? tipo, IReadOnlyDictionary<string, double>? props)
        {
            if (string.IsNullOrWhiteSpace(tipo)) throw new ArgumentException("Informe o tipo da forma.");
            if (props is null) throw new ArgumentException("Informe as propriedades da forma.");
            if (!_map.TryGetValue(tipo, out var b)) throw new ArgumentException($"Tipo de forma não suportado para contenção: {tipo}");
            return b(props);
        }

        private static double Req(IReadOnlyDictionary<string, double> p, string k)
            => p.TryGetValue(k, out var v) && v > 0 ? v : throw new ArgumentException($"Propriedade '{k}' obrigatória e > 0.");
    }
}
