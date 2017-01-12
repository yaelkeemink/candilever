using CAN.Klantbeheer.Domain.Entities;

namespace CAN.Klantbeheer.Domain.Interfaces
{
    public interface IKlantService
    {
        int CreateKlant(Klant klant);
        int UpdateKlant(Klant klant);

    }
}