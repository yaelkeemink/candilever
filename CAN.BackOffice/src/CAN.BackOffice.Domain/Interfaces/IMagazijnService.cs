
using CAN.BackOffice.Domain.Entities;

namespace CAN.BackOffice.Domain.Interfaces
{
    public interface IMagazijnService
    {
        Bestelling GetBestelling();
        int UpdateStatusBestelling(long bestelling);
    }
}
