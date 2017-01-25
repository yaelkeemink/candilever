using CAN.Webwinkel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Domain.Interfaces
{
    public interface IArtikelService
    {
        IEnumerable<Artikel> AlleArtikelen();
        IEnumerable<Artikel> AlleArtikelenPerPagina(int paginanummer, int aantalArtikelen);
        int AantalPaginas(int aantalArtikelenPerPagina);
        string FindArtikelByArtikelNummer(long artikelnummer);
    }
}
