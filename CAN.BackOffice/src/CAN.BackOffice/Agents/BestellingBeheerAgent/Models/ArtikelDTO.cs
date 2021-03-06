// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace CAN.BackOffice.Agents.BestellingsAgent.Agents.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class ArtikelDTO
    {
        /// <summary>
        /// Initializes a new instance of the ArtikelDTO class.
        /// </summary>
        public ArtikelDTO() { }

        /// <summary>
        /// Initializes a new instance of the ArtikelDTO class.
        /// </summary>
        public ArtikelDTO(string naam, string prijs, int aantal, long? id = default(long?), long? artikelnummer = default(long?), string leverancier = default(string), string leverancierCode = default(string))
        {
            Id = id;
            Artikelnummer = artikelnummer;
            Naam = naam;
            Prijs = prijs;
            Aantal = aantal;
            Leverancier = leverancier;
            LeverancierCode = leverancierCode;
        }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public long? Id { get; set; }

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
        public string Prijs { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "aantal")]
        public int Aantal { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "leverancier")]
        public string Leverancier { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "leverancierCode")]
        public string LeverancierCode { get; set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public virtual void Validate()
        {
            if (Naam == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Naam");
            }
            if (Prijs == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Prijs");
            }
        }
    }
}
