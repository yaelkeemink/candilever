using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Domain.Interfaces
{
    public interface ISalesService
        : IDisposable
    {
        IEnumerable<Bestelling> FindAllTeControleren();
        void BestellingGoedkeuren(long id);
        void BestellingAfkeuren(long id);
        Klant FindKlant(long klantnummer);
        Bestelling FindBestelling(long id);
    }
}
