using GeoMaster.Application.Abstractions;
using GeoMaster.Domain.Abstractions;

namespace GeoMaster.Application.Services;

public sealed class CalculadoraService : ICalculadoraService
{
    public double CalcularArea(object forma) =>
        forma is ICalculos2D s2d
            ? s2d.CalcularArea()
            : throw new NotSupportedException("Área só é válida para formas 2D.");

    public double CalcularPerimetro(object forma) =>
        forma is ICalculos2D s2d
            ? s2d.CalcularPerimetro()
            : throw new NotSupportedException("Perímetro só é válido para formas 2D.");

    public double CalcularVolume(object forma) =>
        forma is ICalculos3D s3d
            ? s3d.CalcularVolume()
            : throw new NotSupportedException("Volume só é válido para formas 3D.");

    public double CalcularAreaSuperficial(object forma) =>
        forma is ICalculos3D s3d
            ? s3d.CalcularAreaSuperficial()
            : throw new NotSupportedException("Área superficial só é válida para formas 3D.");
}
