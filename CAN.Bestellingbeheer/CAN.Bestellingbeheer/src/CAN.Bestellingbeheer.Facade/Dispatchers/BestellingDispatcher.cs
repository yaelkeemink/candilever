using Minor.WSA.EventBus.Dispatcher;
using Minor.WSA.EventBus.Config;

namespace CAN.Bestellingbeheer.Facade.Dispatchers 
{
    [RoutingKey("#")]
    public class BestellingDispatcher : EventDispatcher 
    {
        public BestellingDispatcher(EventBusConfig config) 
            : base(config) 
        {

        }
        //implement
        //public void OnRoomCreated(IncomingEvent incomingEvent) 
        //{
        //    // Do whatever you want with incoming event (persist or process)
        //}
    }
}
