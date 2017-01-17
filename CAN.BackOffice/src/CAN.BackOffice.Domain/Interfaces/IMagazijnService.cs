using CAN.BackOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Domain.Interfaces
{
    public interface IMagazijnService
    {
        Bestelling GetBestelling();
        int UpdateStatusBestelling(Bestelling bestelling);
    }
}
