using GeoMaster.Application.Abstractions;
using GeoMaster.Domain.Abstractions;

namespace GeoMaster.Application.Services
{
    public sealed class ValidacoesService : IValidacoesService
    {
        public bool ValidarFormaContida(IFormaContivel formaExterna, IFormaContivel formaInterna)
            => formaExterna.Contem(formaInterna);
    }
}
