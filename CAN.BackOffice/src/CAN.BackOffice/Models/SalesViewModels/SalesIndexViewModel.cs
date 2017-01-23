﻿using CAN.BackOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.BackOffice.Models.SalesViewModels
{
    public class SalesIndexViewModel
    {
        public decimal TotaalPrijs { get; set; }
        public long Id { get; set; }

        public string Klantnaam { get; set; }

        public DateTime BestelDatum { get; set; }

        public static implicit operator SalesIndexViewModel(Bestelling model)
        {
            decimal totaalprijs = 0;
            foreach(var item in model.Artikelen)
            {
                totaalprijs += item.Prijs * item.Aantal;
            }
            return new SalesIndexViewModel()
            {
                TotaalPrijs = totaalprijs,
                BestelDatum = model.BestelDatum,
                Id = model.Id,
                Klantnaam = model.Klantnaam,
            };
        }

    }
}
