using CAN.WinkelmandjeBeheer.Domain.Domain.Entities;
using CAN.WinkelmandjeBeheer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.WinkelmandjeBeheer.Domain.Interfaces
{
    public interface IWinkelmandjeService
    {
        Winkelmandje CreateWinkelmandje(Winkelmandje winkelmandje);
        Winkelmandje UpdateWinkelmandje(Winkelmandje winkelmandje);
        void FinishWinkelmandje(Bestelling bestelling);
        void Dispose();
    }
}
