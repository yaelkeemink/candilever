using CAN.Webwinkel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Infrastructure.Test.RepositoriesTest
{
    public class DemoEntities
    {

        public Artikel Fiets
        {
            get
            {
                var ar = new Artikel()
                {
                    Artikelnummer = 1,
                    Beschrijving = "Hele mooie fiets",
                    AfbeeldingUrl = @"http://www.scribblelive.com/wp-content/uploads/2014/06/IMG_6424-1300x866.jpg",
                    Leverancier = "Kantilever",
                    LeverancierCode = "KTL",
                    LeverbaarTot = new DateTime(2017, 8, 1),
                    LeverbaarVanaf = new DateTime(2017, 1, 1),
                    Naam = "Fiets",
                    Prijs = 159.67M,
                };                

                return ar;
            }
        }

        public Artikel HerenFiets
        {
            get
            {
                var ar = new Artikel()
                {
                    Artikelnummer = 2,
                    Beschrijving = "Hele mooie heren fiets",
                    AfbeeldingUrl = @"http://www.scribblelive.com/wp-content/uploads/2014/06/IMG_6424-1300x866.jpg",
                    Leverancier = "Kantilever",
                    LeverancierCode = "KTL",
                    LeverbaarTot = new DateTime(2017, 8, 1),
                    LeverbaarVanaf = new DateTime(2017, 1, 1),
                    Naam = "Heren fiets",
                    Prijs = 259.67M,
                };                

                return ar;
            }
        }

        public Artikel DamesFiets
        {
            get
            {
                var ar = new Artikel()
                {
                    Artikelnummer = 3,
                    Beschrijving = "Hele mooie dames fiets",
                    AfbeeldingUrl = @"http://www.scribblelive.com/wp-content/uploads/2014/06/IMG_6424-1300x866.jpg",
                    Leverancier = "Kantilever",
                    LeverancierCode = "KTL",
                    LeverbaarTot = new DateTime(2017, 8, 1),
                    LeverbaarVanaf = new DateTime(2017, 1, 1),
                    Naam = "dames fiets",
                    Prijs = 59.67M,
                };               
                return ar;
            }
        }

        public ArtikelDTO FietsDTO
        {
            get
            {
                var ar = new ArtikelDTO()
                {
                    Artikelnummer = 1,
                    Aantal= 1,
                    Leverancier = "Kantilever",
                    LeverancierCode = "KTL",
                    Naam = "Fiets",
                    Prijs = 159.67M,
                };

                return ar;
            }
        }

        public ArtikelDTO HerenFietsDTO
        {
            get
            {
                var ar = new ArtikelDTO()
                {
                    Artikelnummer = 2,
                    Aantal = 5,
                    Leverancier = "Kantilever",
                    LeverancierCode = "KTL",
                    Naam = "Heren fiets",
                    Prijs = 259.67M,
                };

                return ar;
            }
        }

        public ArtikelDTO DamesFietsDTO
        {
            get
            {
                var ar = new ArtikelDTO()
                {
                    Artikelnummer = 3,
                    Aantal = 2,
                    Leverancier = "Kantilever",
                    LeverancierCode = "KTL",
                    Naam = "dames fiets",
                    Prijs = 59.67M,
                };
                return ar;
            }
        }

        public Winkelmandje Winkelmand
        {
            get
            {
                var ar = new Winkelmandje()
                {
                    Id = 1,
                    WinkelmandjeNummer = "123456",
                    Artikelen = CreateArtikelenDTOList()
                };
                return ar;
            }
        }

        public List<ArtikelDTO> CreateArtikelenDTOList()
        {
            List<ArtikelDTO> artikelen = new List<ArtikelDTO>();
            artikelen.Add(DamesFietsDTO);
            artikelen.Add(HerenFietsDTO);
            artikelen.Add(FietsDTO);
            return artikelen;
        }

        public List<Artikel> CreateArtikelenList()
        {
            List<Artikel> artikelen = new List<Artikel>();
            artikelen.Add(DamesFiets);
            artikelen.Add(HerenFiets);
            artikelen.Add(Fiets);
            return artikelen;
        }
    }
}
