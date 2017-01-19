using CAN.Klantbeheer.Domain.Entities;

namespace CAN.Klantbeheer.Domain.Interfaces
{
    public interface IKlantService
    {
        long CreateKlant(Klant klant);
        int UpdateKlant(Klant klant);

    }
}