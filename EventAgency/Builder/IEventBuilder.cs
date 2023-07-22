using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAgency.Observer;

namespace EventAgency.Builder
{
    public interface IEventBuilder
    {
        IEventBuilder SetEventID(int eventID);
        IEventBuilder SetEventName(string eventName);
        IEventBuilder SetEventType(string eventType);
        IEventBuilder SetEventTime(DateTime eventTime);
        IEventBuilder SetLocation(Location location);
        IEventBuilder SetEventScenario(EventScenario eventScenario);
        IEventBuilder SetPrice(decimal price);
        IEventBuilder SetCapacity(int capacity);
        IEventBuilder SetGuestList(List<IObserver> guestList);
        IEventBuilder SetParticipants(List<Participant> participants);
        Event BuildEvent();
    }
}
