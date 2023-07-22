using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAgency.Builder;
using EventAgency.Repository;

namespace EventAgency.Command
{
    class AddEventCommand : IEventCommand
    {
        private Event eventToAdd;
        private EventRepository eventRepository;

        public AddEventCommand(Event eventToAdd, EventRepository eventRepository)
        {
            this.eventToAdd = eventToAdd;
            this.eventRepository = eventRepository;
        }

        public void Execute()
        {
            eventRepository.Add(eventToAdd);
        }
    }

    class UpdateEventCommand : IEventCommand
    {
        private Event eventToUpdate;
        private EventRepository eventRepository;

        public UpdateEventCommand(Event eventToUpdate, EventRepository eventRepository)
        {
            this.eventToUpdate = eventToUpdate;
            this.eventRepository = eventRepository;
        }

        public void Execute()
        {
            eventRepository.Update(eventToUpdate);
        }
    }

    class DeleteEventCommand : IEventCommand
    {
        private int eventToDeleteId;
        private EventRepository eventRepository;

        public DeleteEventCommand(int eventToDeleteId, EventRepository eventRepository)
        {
            this.eventToDeleteId = eventToDeleteId;
            this.eventRepository = eventRepository;
        }

        public void Execute()
        {
            eventRepository.Remove(eventToDeleteId);
        }
    }

    class AddScenarioCommand : IEventCommand
    {
        private EventScenario eventScenario;
        private EventRepository eventRepository;

        public AddScenarioCommand(EventScenario eventScenario, EventRepository eventRepository)
        {
            this.eventScenario = eventScenario;
            this.eventRepository = eventRepository;
        }

        public void Execute()
        {
            eventRepository.AddScenario(eventScenario);
        }
    }

    class UpdateScenarioCommand : IEventCommand
    {
        private EventScenario eventScenario;
        private EventRepository eventRepository;

        public UpdateScenarioCommand(EventScenario eventScenario, EventRepository eventRepository)
        {
            this.eventScenario = eventScenario;
            this.eventRepository = eventRepository;
        }

        public void Execute()
        {
            eventRepository.UpdateScenario(eventScenario);
        }
    }

    class AddGuestCommand : IEventCommand
    {
        private Guest newGuest;
        private GuestRepository guestRepository;

        public AddGuestCommand(Guest newGuest, GuestRepository guestRepository)
        {
            this.newGuest = newGuest;
            this.guestRepository = guestRepository;
        }

        public void Execute()
        {
            guestRepository.Add(newGuest);
        }
    }

    class UpdateGuestCommand : IEventCommand
    {
        private Guest guest;
        private GuestRepository guestRepository;

        public UpdateGuestCommand(Guest guest, GuestRepository guestRepository)
        {
            this.guest = guest;
            this.guestRepository = guestRepository;
        }

        public void Execute()
        {
            guestRepository.Update(guest);
        }
    }

    class DeleteGuestCommand : IEventCommand
    {
        private int guestId;
        private GuestRepository guestRepository;

        public DeleteGuestCommand(int guestId, GuestRepository guestRepository)
        {
            this.guestId = guestId;
            this.guestRepository = guestRepository;
        }

        public void Execute()
        {
            guestRepository.Remove(guestId);
        }
    }

    class AssignGuestToEventCommand : IEventCommand
    {
        private Event toEvent;
        private Guest guest;
        private string guestStatus;
        private GuestRepository guestRepository;

        public AssignGuestToEventCommand(Event toEvent, Guest guest, string guestStatus, GuestRepository guestRepository)
        {
            this.toEvent = toEvent;
            this.guest = guest;
            this.guestStatus = guestStatus;
            this.guestRepository = guestRepository;
        }

        public void Execute()
        {
            guestRepository.AssignGuestToEvent(toEvent, guest, guestStatus);
        }
    }

    class AddLocationCommand : IEventCommand
    {
        private Location location;
        private LocationRepository locationRepository;

        public AddLocationCommand(Location location, LocationRepository locationRepository)
        {
            this.location = location;
            this.locationRepository = locationRepository;
        }

        public void Execute()
        {
            locationRepository.Add(location);
        }
    }

    class UpdateLocationCommand : IEventCommand
    {
        private Location location;
        private LocationRepository locationRepository;

        public UpdateLocationCommand(Location location, LocationRepository locationRepository)
        {
            this.location = location;
            this.locationRepository = locationRepository;
        }

        public void Execute()
        {
            locationRepository.Update(location);
        }
    }

    class DeleteLocationCommand : IEventCommand
    {
        private int locationId;
        private LocationRepository locationRepository;

        public DeleteLocationCommand(int locationId, LocationRepository locationRepository)
        {
            this.locationId = locationId;
            this.locationRepository = locationRepository;
        }

        public void Execute()
        {
            locationRepository.Remove(locationId);
        }
    }

    class AddParticipantCommand : IEventCommand
    {
        private Participant participant;
        private ParticipantRepository participantRepository;

        public AddParticipantCommand(Participant participant, ParticipantRepository participantRepository)
        {
            this.participant = participant;
            this.participantRepository = participantRepository;
        }

        public void Execute()
        {
            participantRepository.Add(participant);
        }
    }

    class UpdateParticipantCommand : IEventCommand
    {
        private Participant participant;
        private ParticipantRepository participantRepository;

        public UpdateParticipantCommand(Participant participant, ParticipantRepository participantRepository)
        {
            this.participant = participant;
            this.participantRepository = participantRepository;
        }

        public void Execute()
        {
            participantRepository.Update(participant);
        }
    }

    class DeleteParticipantCommand : IEventCommand
    {
        private int participantId;
        private ParticipantRepository participantRepository;

        public DeleteParticipantCommand(int participantId, ParticipantRepository participantRepository)
        {
            this.participantId = participantId;
            this.participantRepository = participantRepository;
        }

        public void Execute()
        {
            participantRepository.Remove(participantId);
        }
    }

    class AssignParticipantToEventCommand : IEventCommand
    {
        private Event toEvent;
        private Participant participant;
        private int workHours;
        private ParticipantRepository participantRepository;

        public AssignParticipantToEventCommand(Event toEvent, Participant participant, int workHours, ParticipantRepository participantRepository)
        {
            this.toEvent = toEvent;
            this.participant = participant;
            this.workHours = workHours;
            this.participantRepository = participantRepository;
        }

        public void Execute()
        {
            participantRepository.AssignParticipantToEvent(toEvent, participant, workHours);
        }
    }
}
