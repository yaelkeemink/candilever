using System;

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
