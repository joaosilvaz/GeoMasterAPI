using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoMaster.Domain.Abstractions;

namespace GeoMaster.Application.Abstractions;

public interface ICalculadoraService
{
    double CalcularArea(object forma);
    double CalcularPerimetro(object forma);
    double CalcularVolume(object forma);
    double CalcularAreaSuperficial(object forma);
}
