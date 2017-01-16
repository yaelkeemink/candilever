// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace CAN.Webwinkel.Agents.KlantAgent.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Klant
    {
        /// <summary>
        /// Initializes a new instance of the Klant class.
        /// </summary>
        public Klant() { }

        /// <summary>
        /// Initializes a new instance of the Klant class.
        /// </summary>
        public Klant(string voornaam, string achternaam, string postcode, string huisnummer, string adres, int land, long? klantnummer = default(long?), string tussenvoegsels = default(string), string telefoonnummer = default(string), string email = default(string))
        {
            Klantnummer = klantnummer;
            Voornaam = voornaam;
            Achternaam = achternaam;
            Tussenvoegsels = tussenvoegsels;
            Postcode = postcode;
            Telefoonnummer = telefoonnummer;
            Email = email;
            Huisnummer = huisnummer;
            Adres = adres;
            Land = land;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "klantnummer")]
        public long? Klantnummer { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "voornaam")]
        public string Voornaam { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "achternaam")]
        public string Achternaam { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "tussenvoegsels")]
        public string Tussenvoegsels { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "postcode")]
        public string Postcode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "telefoonnummer")]
        public string Telefoonnummer { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "huisnummer")]
        public string Huisnummer { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "adres")]
        public string Adres { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "land")]
        public int Land { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Voornaam == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Voornaam");
            }
            if (Achternaam == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Achternaam");
            }
            if (Postcode == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Postcode");
            }
            if (Huisnummer == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Huisnummer");
            }
            if (Adres == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Adres");
            }
        }
    }
}