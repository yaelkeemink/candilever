using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Bestellingbeheer.Domain.Exceptions
{
    public class InvalidBestelStatusException
        : Exception
    {
        public InvalidBestelStatusException(string message)
        {
            Message = message;
        }
        public override string Message { get; }
    }
}
