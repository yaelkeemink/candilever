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

        public string Postcode { get; set; }
        public string Telefoonnummer { get; set; }
        public string Email { get; set; }
        public string Huisnummer { get; set; }
        public string Adres { get; set; }
        public string Land { get; set; }
    }
}
