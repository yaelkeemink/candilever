namespace CAN.Klantbeheer.Domain.Domain.Entities
{
    public class Klant
    {
        public long Klantnummer { get; set; }

        public string Voornaam { get; set; }

        public string Achternaam { get; set; }

        public string Tussenvoegsels { get; set; }

        public string Postcode { get; set; }
    }
}
