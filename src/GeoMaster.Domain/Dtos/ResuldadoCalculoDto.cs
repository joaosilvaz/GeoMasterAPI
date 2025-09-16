using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMaster.Domain.Dtos;

public enum TipoOperacao { Area, Perimetro, Volume, AreaSuperficial }

public sealed record ResultadoCalculoDto(
    string TipoForma,
    double Resultado,
    TipoOperacao Operacao,
    DateTimeOffset DataCalculo)
{
    public static object? CriarResponse(string tipo, double resultado, string nomeOperacao)
    {
        throw new NotImplementedException();
    }

    public static ResultadoCalculoDto From(string tipoForma, double resultado, TipoOperacao operacao)
        => new(
            TipoForma: tipoForma,
            Resultado: Math.Round(resultado, 4, MidpointRounding.AwayFromZero),
            Operacao: operacao,
            DataCalculo: DateTimeOffset.UtcNow
        );
}
