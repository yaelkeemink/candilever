using CAN.Webwinkel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Domain.Interfaces
{
    interface IArtikelService
    {
        IEnumerable<Artikel> ArtikelenBijCategorie(string categorieNaam); 
    }
}
