using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Mappers
{
    public class WinkelmandjeMapper
    {
        public WinkelmandjeMapper()
        {

        }

        public static Domain.Entities.Winkelmandje Map(Agents.WinkelwagenAgent.Models.Winkelmandje mandje)
        {
            var entity = new Domain.Entities.Winkelmandje();
            entity.WinkelmandjeNummer = mandje.WinkelmandjeNummer;
            entity.Artikelen = MapArtikelen(mandje.Artikelen);

            return entity;
        }

        private static IList<Domain.Entities.ArtikelDTO> MapArtikelen(IList<Agents.WinkelwagenAgent.Models.ArtikelDTO> artikelen)
        {
            IList<Domain.Entities.ArtikelDTO> entityList = new List<Domain.Entities.ArtikelDTO>();

            foreach(var artikel in artikelen)
            {
                Domain.Entities.ArtikelDTO entity = new Domain.Entities.ArtikelDTO();
                entity.Leverancier = artikel.Leverancier;
                entity.LeverancierCode = artikel.LeverancierCode;
                entity.Naam = artikel.Naam;
                entity.Prijs = decimal.Parse(artikel.Prijs);
                entity.Artikelnummer = (long)artikel.Artikelnummer;
                entity.Aantal = artikel.Aantal;

                entityList.Add(entity);
            }

            return entityList;
        }
    }
}
