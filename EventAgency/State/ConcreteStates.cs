using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Npgsql;

namespace EventAgency.State
{
    public class PlanningState : IEventState
    {
        public void AssignGuestToEvent(Event toEvent, Guest guest, string guestStatus, string connectionString)
        {
            MessageBox.Show("Event is still planning, update to Approved to add guests", "Assigning guests message");
        }

        public void AssignParticipantToEvent(Event toEvent, Participant participant, int workHours, string connectionString)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO event_participant (id_participant, id_event, work_hours) " +
                    "VALUES (@participantId, @eventId, @workHours)";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("participantId", participant.participantId);
                command.Parameters.AddWithValue("eventId", toEvent.eventId);
                command.Parameters.AddWithValue("workHours", workHours);

                command.ExecuteNonQuery();
            }
        }

        public void Update(Event updatedEvent, string connectionString)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE event SET name_event = @eventName, type_event = @eventType, time_event = @eventTime, id_location = @locationId, id_event_scen = @scenarioId, price = @price, capacity = @capacity WHERE id_event = @eventId";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("eventId", updatedEvent.eventId);
                command.Parameters.AddWithValue("eventName", updatedEvent.eventName);
                command.Parameters.AddWithValue("eventType", updatedEvent.eventType);
                command.Parameters.AddWithValue("eventTime", updatedEvent.eventTime);
                command.Parameters.AddWithValue("locationId", updatedEvent.location.locationId);
                command.Parameters.AddWithValue("scenarioId", updatedEvent.eventScenario.eventScenarioId);
                command.Parameters.AddWithValue("price", updatedEvent.price);
                command.Parameters.AddWithValue("capacity", updatedEvent.capacity);

                switch(updatedEvent.State.GetType().Name)
                {
                    case "PlanningState":
                        command.Parameters.AddWithValue("state", "planning");
                        break;
                    case "CancelledState":
                        command.Parameters.AddWithValue("state", "cancelled");
                        break;
                    case "FinishedState":
                        command.Parameters.AddWithValue("state", "finished");
                        break;
                    case "ApprovedState":
                        command.Parameters.AddWithValue("state", "approved");
                        break;
                }

                command.ExecuteNonQuery();
            }
            updatedEvent.Notify();
        }
    }

    public class CancelledState : IEventState
    {
        public void AssignGuestToEvent(Event toEvent, Guest guest, string guestStatus, string connectionString)
        {
            MessageBox.Show("Event was cancelled", "Message");
        }

        public void AssignParticipantToEvent(Event toEvent, Participant participant, int workHours, string connectionString)
        {
            MessageBox.Show("Event was cancelled", "Message");
        }

        public void Update(Event updatedEvent, string connectionString)
        {
            MessageBox.Show("Event was cancelled", "Message");
        }
    }

    public class FinishedState : IEventState
    {
        public void AssignGuestToEvent(Event toEvent, Guest guest, string guestStatus, string connectionString)
        {
            MessageBox.Show("Event was finished", "Message");
        }

        public void AssignParticipantToEvent(Event toEvent, Participant participant, int workHours, string connectionString)
        {
            MessageBox.Show("Event was finished", "Message");
        }

        public void Update(Event updatedEvent, string connectionString)
        {
            MessageBox.Show("Event was finished", "Message");
        }
    }

    public class ApprovedState : IEventState
    {
        public void AssignGuestToEvent(Event toEvent, Guest guest, string guestStatus, string connectionString)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO event_guest (id_guest, id_event, guest_status) " +
                    "VALUES (@guestId, @eventId, @guestStatus)";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("guestId", guest.guestId);
                command.Parameters.AddWithValue("eventId", toEvent.eventId);
                command.Parameters.AddWithValue("guestStatus", guestStatus);

                command.ExecuteNonQuery();
            }
        }

        public void AssignParticipantToEvent(Event toEvent, Participant participant, int workHours, string connectionString)
        {
            MessageBox.Show("Event is already approved, change to Planning to add participants", "Assigning participants message");
        }

        public void Update(Event updatedEvent, string connectionString)
        {
            MessageBox.Show("Event is already approved, change to Planning to update event", "Updating event message");
        }
    }
}
