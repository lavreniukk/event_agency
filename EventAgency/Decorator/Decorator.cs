using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAgency.Decorator
{
    abstract class Decorator : Event
    {
        protected Event savedEvent;
        public Decorator(string n, Event savedEvent) : base(n)
        {
            this.savedEvent = savedEvent;
        }
    }

    class Florist : Decorator
    {
        public Florist(Event e) : base(e.eventType + ", with florist", e)
        {
            this.eventId = e.eventId;
            this.eventName = e.eventName;
            this.eventTime = e.eventTime;
            this.location = e.location;
            this.eventScenario = e.eventScenario;
            this.price = e.price;
            this.capacity = e.capacity;
            this.guestList = e.guestList;
            this.participants = e.participants;
        }
    }

    class Delivery : Decorator
    {
        public Delivery(Event e) : base(e.eventType + ", with delivery service", e)
        {
            this.eventId = e.eventId;
            this.eventName = e.eventName;
            this.eventTime = e.eventTime;
            this.location = e.location;
            this.eventScenario = e.eventScenario;
            this.price = e.price;
            this.capacity = e.capacity;
            this.guestList = e.guestList;
            this.participants = e.participants;
        }
    }
}
