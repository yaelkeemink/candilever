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
        int UpdateStatusOpgehaald(long id);
        Bestelling CreateBestelling(Bestelling bestelling);
    }
}
