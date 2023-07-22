using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAgency;
using EventAgency.Observer;

namespace EventAgency.Builder
{
    public class EventBuilder : IEventBuilder
    {
        private Event _event = new Event();

        public IEventBuilder SetEventID(int eventID)
        {
            _event.eventId = eventID;
            return this;
        }
        public IEventBuilder SetEventName(string eventName)
        {
            _event.eventName = eventName;
            return this;
        }
        public IEventBuilder SetEventType(string eventType)
        {
            _event.eventType = eventType;
            return this;
        }
        public IEventBuilder SetEventTime(DateTime eventTime)
        {
            _event.eventTime = eventTime;
            return this;
        }
        public IEventBuilder SetLocation(Location location)
        {
            _event.location = location;
            return this;
        }
        public IEventBuilder SetEventScenario(EventScenario eventScenario)
        {
            _event.eventScenario = eventScenario;
            return this;
        }
        public IEventBuilder SetPrice(decimal price)
        {
            _event.price = price;
            return this;
        }
        public IEventBuilder SetCapacity(int capacity)
        {
            _event.capacity = capacity;
            return this;
        }
        public IEventBuilder SetGuestList(List<IObserver> guestList)
        {
            _event.guestList = guestList;
            return this;
        }
        public IEventBuilder SetParticipants(List<Participant> participants)
        {
            _event.participants = participants;
            return this;
        }

        public Event BuildEvent()
        {
            return _event;
        }
    }
}
