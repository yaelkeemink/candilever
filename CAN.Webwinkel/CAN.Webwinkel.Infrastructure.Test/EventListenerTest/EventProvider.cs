using Kantilever.Catalogusbeheer.Events;
using Kantilever.Magazijnbeheer;
using Kantilever.Magazijnbeheer.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Infrastructure.Test.EventListenerTest
{
    public class EventProvider
    {

        public ArtikelAanCatalogusToegevoegd FietsCreatedEvent
        {
            get
            {
                return new ArtikelAanCatalogusToegevoegd()
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
                    Categorieen = new List<string>() { "Fietsen", "Heren Fietsen" }
                };
            }
        }

        public ArtikelAanCatalogusToegevoegd HerenFietsCreatedEvent
        {
            get
            {
                return new ArtikelAanCatalogusToegevoegd()
                {
                    Artikelnummer = 2,
                    Beschrijving = "Hele mooie heren fiets",
                    AfbeeldingUrl = @"http://www.scribblelive.com/wp-content/uploads/2014/06/IMG_6424-1300x866.jpg",
                    Leverancier = "Kantilever2",
                    LeverancierCode = "KTL2",
                    LeverbaarTot = new DateTime(2017, 8, 1),
                    LeverbaarVanaf = new DateTime(2017, 1, 1),
                    Naam = "Heren fiets",
                    Prijs = 1590.67M,
                    Categorieen = new List<string>() { "Heren Fietsen" }
                };
            }
        }

        public ArtikelUitCatalogusVerwijderd VerwijderFietsEvent
        {
            get
            {
                return new ArtikelUitCatalogusVerwijderd()
                {
                    Artikelnummer = FietsCreatedEvent.Artikelnummer

                };
            }
        }

        public ArtikelUitMagazijnGehaald VerlaagVoorraadEvent
        {
            get
            {
                return new ArtikelUitMagazijnGehaald()
                {
                    ArtikelID = FietsCreatedEvent.Artikelnummer,
                    Voorraad = 8
                };

            }
        }

        public ArtikelInMagazijnGezet VerhoogVoorraadEvent
        {
            get
            {
                return new ArtikelInMagazijnGezet()
                {
                    ArtikelID = FietsCreatedEvent.Artikelnummer,
                    Voorraad = 19
                };

            }
        }
    }
}
