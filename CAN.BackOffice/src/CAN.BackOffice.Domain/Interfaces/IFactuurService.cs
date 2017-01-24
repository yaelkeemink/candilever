using CAN.BackOffice.Domain.Entities;
using System;

namespace CAN.BackOffice.Domain.Interfaces
{
    public interface IFactuurService : IDisposable
    {
        Bestelling ZoekBestelling(long bestellingsnummer);
    }
}
