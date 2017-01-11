using InfoSupport.WSA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InfoSupport.WSA.Common;

namespace CAN.Klantbeheer.Domain.Test.Mocks
{
    public class PublishMock
        : IEventPublisher
    {
        public void Dispose()
        {
            
        }

        public void Publish(DomainEvent domainEvent)
        {
            
        }
    }
}
