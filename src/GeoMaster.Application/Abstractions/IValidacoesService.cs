using GeoMaster.Domain.Abstractions;

namespace GeoMaster.Application.Abstractions
{
    public interface IValidacoesService
    {
        bool ValidarFormaContida(IFormaContivel formaExterna, IFormaContivel formaInterna);
    }
}
