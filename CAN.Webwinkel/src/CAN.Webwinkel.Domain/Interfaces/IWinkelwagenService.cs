using CAN.Webwinkel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Domain.Interfaces
{
    public interface IWinkelwagenService
    {
        void Insert(Winkelmandje mandje);
        void Update(Winkelmandje mandje);
        Winkelmandje FindWinkelmandje(string guid);
    }
}
