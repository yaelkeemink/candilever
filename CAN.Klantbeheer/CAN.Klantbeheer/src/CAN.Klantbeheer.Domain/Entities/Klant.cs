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
        public int Telefoonnummer { get; set; }
    }
}
