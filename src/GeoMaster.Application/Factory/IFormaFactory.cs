using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeoMaster.Application.Factory
{
    public interface IFormaFactory
    {
        object CriarForma(string? tipo, IReadOnlyDictionary<string, double>? props);
        bool SuportaTipo(string tipo);
    }
}
