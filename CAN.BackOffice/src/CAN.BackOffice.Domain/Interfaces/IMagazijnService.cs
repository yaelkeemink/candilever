
using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Domain.Interfaces
{
    public interface IMagazijnService
    {
        Bestelling GetVolgendeBestelling();
        int ZetBestellingOpOpgehaald(long bestelling);
    }
}
