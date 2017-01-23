using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Models
{
    public class ArtikelOverzichtModel
    {

        public List<ApiArtikelenModel> Artikelen { get; set; }
        public int AantalPaginas { get; set; }

        public int HuidigePaginanummer { get; set; }

        public int VorigePagina
        {
            get
            {
                return HuidigePaginanummer - 1;
            }
        }

        public int VolgendePagina
        {
            get
            {
                return HuidigePaginanummer + 1;
            }
        }
    }
}
