using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.WinkelmandjeBeheer.Domain.Entities
{
    public enum BestelStatus
    {
        Geplaatst = 0,
        Goedgekeurd = 50,
        Opgehaald = 100,
        Afgekeurd = 150,
    }
    public class Bestelling
    {
        [Required]
        public long Klantnummer { get; set; }
        [Required]
        public string VolledigeNaam { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public string Huisnummer { get; set; }
        [Required]
        public string Land { get; set; }
        [Required]
        public string WinkelmandjeNummer { get; set; }
        public DateTime BestelDatum { get; set; }
        public BestelStatus Status { get; set; }
    }
}
