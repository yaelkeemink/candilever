using Kantilever.Catalogusbeheer.Events;
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

    }
}
