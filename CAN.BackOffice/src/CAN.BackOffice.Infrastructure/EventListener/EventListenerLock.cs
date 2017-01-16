using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CAN.Webwinkel.Infrastructure.EventListener
{
    public class EventListenerLock
    {
        private object LockObject;

        private long _eventsReceived;
        private long _expectedEvents;
        public ManualResetEvent EventReplayLock { get; private set; }
        public ManualResetEvent StartUpLock { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        public EventListenerLock()
        {
            LockObject = new object();
            StartUpLock = new ManualResetEvent(false);
            StartUpLock.Reset();
            EventReplayLock = new ManualResetEvent(false);
            EventReplayLock.Reset();

        }



        /// <summary>
        /// 
        /// </summary>
        public void EventReceived()
        {
            lock (LockObject)
            {
                _eventsReceived++;
                if (_eventsReceived >= _expectedEvents)
                {
                    EventReplayLock.Set();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="events"></param>
        public void SetExpectedEvents(long events)
        {
            _expectedEvents = events;
            if (_expectedEvents == 0)
            {
                EventReplayLock.Set();
            }


        }
    }
}
