
using CAN.BackOffice.Domain.Entities;
using System;

namespace CAN.BackOffice.Domain.Interfaces
{
    public interface IMagazijnService
        : IDisposable
    {
        Bestelling GetVolgendeBestelling();
        void ZetBestellingOpOpgehaald(long bestelling);
    }
}
