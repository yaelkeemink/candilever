using CAN.Klantbeheer.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CAN.Klantbeheer.Domain.Entities
{
    public class Klant
    {
        [Key]
        public long Klantnummer { get; set; }
        [Required]
        public string Voornaam { get; set; }
        [Required]
        public string Achternaam { get; set; }

        public string Tussenvoegsels { get; set; }
        [Required]
        public string Postcode { get; set; }
        public string Telefoonnummer { get; set; }
        public string Email { get; set; }
        [Required]
        public string Huisnummer { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public Land Land { get; set; }
    }
}
