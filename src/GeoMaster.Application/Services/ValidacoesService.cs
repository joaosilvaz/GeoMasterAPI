using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoMaster.Application.Abstractions;
using GeoMaster.Domain.Abstractions;

namespace GeoMaster.Application.Services
{
    public class ValidacoesService : IValidacoesService
    {
        public bool ValidarFormaContida(IFormaContivel formaExterna, IFormaContivel formaInterna)
        {
            return formaExterna.Contem(formaInterna);
        }
    }
}