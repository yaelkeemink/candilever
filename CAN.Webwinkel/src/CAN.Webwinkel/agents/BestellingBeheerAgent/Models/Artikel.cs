// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace CAN.Webwinkel.Agents.BestellingsAgent.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Artikel
    {
        /// <summary>
        /// Initializes a new instance of the Artikel class.
        /// </summary>
        public Artikel() { }

        /// <summary>
        /// Initializes a new instance of the Artikel class.
        /// </summary>
        public Artikel(string naam, double prijs, int aantal, long? bestellingnummer = default(long?), Bestelling bestelling = default(Bestelling), long? artikelnummer = default(long?))
        {
            Bestellingnummer = bestellingnummer;
            Bestelling = bestelling;
            Artikelnummer = artikelnummer;
            Naam = naam;
            Prijs = prijs;
            Aantal = aantal;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bestellingnummer")]
        public long? Bestellingnummer { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "bestelling")]
        public Bestelling Bestelling { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "artikelnummer")]
        public long? Artikelnummer { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "naam")]
        public string Naam { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "prijs")]
        public double Prijs { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "aantal")]
        public int Aantal { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Naam == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Naam");
            }
            if (this.Bestelling != null)
            {
                this.Bestelling.Validate();
            }
        }
    }
}
