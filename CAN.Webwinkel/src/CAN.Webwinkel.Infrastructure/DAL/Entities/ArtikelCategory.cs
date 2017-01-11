using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Infrastructure.DAL.Entities
{
    public class ArtikelCategory
    {
        public long ArtikelId { get; set; }
        public Artikel Artikel { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
