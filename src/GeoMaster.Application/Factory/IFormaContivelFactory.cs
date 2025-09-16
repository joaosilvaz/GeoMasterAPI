using GeoMaster.Domain.Abstractions;

namespace GeoMaster.Application.Factory
{
    public interface IFormaContivelFactory
    {
        IFormaContivel CriarFormaContivel(string? tipo, IReadOnlyDictionary<string, double>? props);
    }
}
