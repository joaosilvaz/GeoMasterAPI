using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMaster.Domain.Abstractions
{
    public interface IFormaContivel
    {
        bool Contem(IFormaContivel formaInterna);
    }
}