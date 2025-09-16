using Domain;
using GeoMaster.Domain.Abstractions;
using GeoMaster.Domain.Shapes;
namespace GeoMaster.Application.Factory
{
    public sealed class FormaContivelFactory : IFormaContivelFactory
    {
        private readonly IDictionary<string, Func<IReadOnlyDictionary<string, double>, IFormaContivel>> _map;

        public FormaContivelFactory()
        {
            _map = new Dictionary<string, Func<IReadOnlyDictionary<string, double>, IFormaContivel>>(StringComparer.OrdinalIgnoreCase)
            {
                // ajuste os types abaixo para as SUAS classes que implementam IFormaContivel
                ["circulo"] = p => new CirculoContivel(Req(p, "raio")),
                ["retangulo"] = p => new RetanguloContivel(Req(p, "largura"), Req(p, "altura"))
            };
        }

        public IFormaContivel CriarFormaContivel(string? tipo, IReadOnlyDictionary<string, double>? props)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("Informe o tipo da forma.");
            if (props is null)
                throw new ArgumentException("Informe as propriedades da forma.");

            if (!_map.TryGetValue(tipo, out var build))
                throw new ArgumentException($"Tipo de forma não suportado para contenção: {tipo}");

            return build(props);
        }

        private static double Req(IReadOnlyDictionary<string, double> p, string key)
        {
            if (!p.TryGetValue(key, out var v) || v <= 0)
                throw new ArgumentException($"Propriedade '{key}' obrigatória e deve ser > 0.");
            return v;
        }
    }
}
