﻿using System;

namespace CAN.Bestellingbeheer.Domain.Entities
{
    public class Artikel
    {
        public long BestellingId { get; set; }

        public Bestelling Bestelling { get; set; }


        public long Id { get; set; }

        public string Naam { get; set; }

        public decimal Prijs { get; set; }

        public int Aantal { get; set; }
    }
}
