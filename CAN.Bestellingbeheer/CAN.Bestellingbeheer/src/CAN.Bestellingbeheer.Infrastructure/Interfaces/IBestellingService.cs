using CAN.Bestellingbeheer.Domain.Entities;
using System;

namespace CAN.Bestellingbeheer.Infrastructure.Interfaces
{
    public interface IBestellingService
        :IDisposable
    {
        int UpdateBestelling(Bestelling bestelling);
        Bestelling StatusNaarOpgehaald(long id);
        void CreateBestelling(Bestelling bestelling);
        Bestelling StatusNaarGoedgekeurd(long id);
        Bestelling StatusNaarAfgekeurd(long id);
    }
}
