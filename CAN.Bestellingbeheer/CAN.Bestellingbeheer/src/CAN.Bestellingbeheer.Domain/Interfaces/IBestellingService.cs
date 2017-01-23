using CAN.Bestellingbeheer.Domain.DTO;
using CAN.Bestellingbeheer.Domain.Entities;
using InfoSupport.WSA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Bestellingbeheer.Domain.Interfaces
{
    public interface IBestellingService
        :IDisposable
    {
        int UpdateBestelling(Bestelling bestelling);
        int StatusNaarOpgehaald(long id);
        BestellingDTO CreateBestelling(Bestelling bestelling);
        Bestelling StatusNaarGoedgekeurd(long id);
    }
}
