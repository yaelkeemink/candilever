using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Domain.Interfaces
{
    public interface IFactuurService
    {
        Bestelling ZoekBestelling(int bestellingsnummer);
    }
}
