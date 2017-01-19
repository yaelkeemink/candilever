using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Bestellingbeheer.Domain.Exceptions
{
    public class InvalidStatusException
        : Exception
    {
        public InvalidStatusException(string message)
        {
            Message = message;
        }
        public override string Message { get; }
    }
}
