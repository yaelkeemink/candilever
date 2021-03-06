﻿using CAN.Webwinkel.Domain.Entities;
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
                    ArtikelCategorie = new List<ArtikelCategorie>()
                };

                ArtikelCategorie arCat = new ArtikelCategorie() {
                    Artikel = ar,
                    Categorie = new Categorie() {
                        Naam = "Heren fiets"
                    }
                };
                ar.ArtikelCategorie.Add(arCat);

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
                    ArtikelCategorie = new List<ArtikelCategorie>()
                };

                ArtikelCategorie arCat = new ArtikelCategorie()
                {
                    Artikel = ar,
                    Categorie = new Categorie()
                    {
                        Naam = "Heren fiets"
                    }
                };
                ar.ArtikelCategorie.Add(arCat);

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
                    ArtikelCategorie = new List<ArtikelCategorie>() 
                };

                ArtikelCategorie arCat = new ArtikelCategorie()
                {
                    Artikel = ar,
                    Categorie = new Categorie()
                    {
                        Naam = "Dames fiets"
                    }
                };
                ar.ArtikelCategorie.Add(arCat);
                return ar;
            }
        }
    }
}
