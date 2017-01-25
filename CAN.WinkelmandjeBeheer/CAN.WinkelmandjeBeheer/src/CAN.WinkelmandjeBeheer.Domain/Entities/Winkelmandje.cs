using CAN.WinkelmandjeBeheer.Domain.DTO;
using CAN.WinkelmandjeBeheer.Domain.Entities;
using System;
using System.Collections.Generic;


namespace CAN.WinkelmandjeBeheer.Domain.Domain.Entities
{
    public class Winkelmandje
    {
        public long Id { get; set; }
        public string WinkelmandjeNummer { get; set; }
        public IList<ArtikelDTO> Artikelen { get; set; }


        public Winkelmandje()
        {
            Artikelen = new List<ArtikelDTO>();
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(WinkelmandjeNummer))
            {
                WinkelmandjeNummer =  Guid.NewGuid().ToString();
            }
        }
    }
}
