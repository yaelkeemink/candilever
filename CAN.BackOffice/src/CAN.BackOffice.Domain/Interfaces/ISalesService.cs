using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Domain.Interfaces
{
    public interface ISalesService
    {
        IEnumerable<Bestelling> FindAllTeControlleren();
    }
}
