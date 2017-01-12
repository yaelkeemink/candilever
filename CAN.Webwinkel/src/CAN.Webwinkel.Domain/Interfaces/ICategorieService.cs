using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Domain.Interfaces
{
    public interface ICategorieService
    {
        IEnumerable<string> AlleCategorieen();
    }
}
