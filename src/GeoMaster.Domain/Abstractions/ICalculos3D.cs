using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMaster.Domain.Abstractions
{
    public interface ICalculos3D
    {
        double CalcularVolume();
        double CalcularAreaSuperficial();
    }
}
