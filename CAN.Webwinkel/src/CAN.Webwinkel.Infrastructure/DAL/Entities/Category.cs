using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Infrastructure.DAL.Entities
{
    public class Category
    {
        public long Id { get; set; }
        public string Naam { get; set; }
        public List<ArtikelCategory> ArtikelCategory { get; set; } = new List<ArtikelCategory>();
    }
}
