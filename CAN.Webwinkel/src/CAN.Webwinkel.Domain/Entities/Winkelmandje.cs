using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Domain.Entities
{
    public class Winkelmandje
    {
        public long Id { get; set; }
        public string WinkelmandjeNummer { get; set; }
        public IList<ArtikelDTO> Artikelen { get; set; }


        public Winkelmandje()
        {
            WinkelmandjeNummer = Guid.NewGuid().ToString();
            Artikelen = new List<ArtikelDTO>();
        }
    }
}
