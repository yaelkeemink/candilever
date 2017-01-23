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
        Guid CreateWinkelmandje(Winkelmandje winkelmandje);
        Guid UpdateWinkelmandje(Winkelmandje winkelmandje);
        void FinishWinkelmandje(Bestelling bestelling);
        void Dispose();
    }
}
