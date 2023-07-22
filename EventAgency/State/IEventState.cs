using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventAgency.State
{
    public interface IEventState
    {
        void Update(Event updatedEvent, string connectionString);
        void AssignGuestToEvent(Event toEvent, Guest guest, string guestStatus, string connectionString);
        void AssignParticipantToEvent(Event toEvent, Participant participant, int workHours, string connectionString);
    }
}
