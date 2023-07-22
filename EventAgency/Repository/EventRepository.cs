using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAgency.Observer;
using EventAgency.State;
using Npgsql;

namespace EventAgency.Repository
{
    public class EventRepository : IRepository<Event>
    {
        private string _connectionString;
        private LocationRepository _locationRepository;

        public EventRepository(string connectionString, LocationRepository locationRepository)
        {
            _connectionString = connectionString;
            _locationRepository = locationRepository;
        }

        public IEnumerable<Event> GetAll()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM event";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int eventId = reader.GetInt32(reader.GetOrdinal("id_event"));
                            string nameEvent = reader.GetString(reader.GetOrdinal("name_event"));
                            string typeEvent = reader.GetString(reader.GetOrdinal("type_event"));
                            DateTime timeEvent = reader.GetDateTime(reader.GetOrdinal("time_event"));
                            int locationId = reader.GetInt32(reader.GetOrdinal("id_location"));
                            int scenarioId = reader.GetInt32(reader.GetOrdinal("id_event_scen"));
                            decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
                            int capacity = reader.GetInt32(reader.GetOrdinal("capacity"));
                            string stringState = reader.GetString(reader.GetOrdinal("state_event"));

                            Location location = _locationRepository.GetById(locationId);
                            EventScenario eventScenario = GetScenarioById(scenarioId);
                            List<IObserver> guests = GetEventGuests(eventId);
                            List<Participant> participants = GetEventParticipants(eventId);

                            Event eventData = new Event(eventId, nameEvent, typeEvent, timeEvent, location, eventScenario, price, capacity, guests, participants);

                            switch (stringState)
                            {
                                case "planning":
                                    eventData.SetEventState(new PlanningState());
                                    break;
                                case "cancelled":
                                    eventData.SetEventState(new CancelledState());
                                    break;
                                case "finished":
                                    eventData.SetEventState(new FinishedState());
                                    break;
                                case "approved":
                                    eventData.SetEventState(new ApprovedState());
                                    break;
                            }

                            yield return eventData;
                        }
                    }
                }
            }
        }

        public Event GetById(int eventId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM event WHERE id_event = @eventId";
                    command.Parameters.AddWithValue("eventId", eventId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nameEvent = reader.GetString(reader.GetOrdinal("name_event"));
                            string typeEvent = reader.GetString(reader.GetOrdinal("type_event"));
                            DateTime timeEvent = reader.GetDateTime(reader.GetOrdinal("time_event"));
                            int locationId = reader.GetInt32(reader.GetOrdinal("id_location"));
                            int scenarioId = reader.GetInt32(reader.GetOrdinal("id_event_scen"));
                            decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
                            int capacity = reader.GetInt32(reader.GetOrdinal("capacity"));

                            Location location = _locationRepository.GetById(locationId);
                            EventScenario eventScenario = GetScenarioById(scenarioId);
                            List<IObserver> guests = GetEventGuests(eventId);
                            List<Participant> participants = GetEventParticipants(eventId);

                            Event eventData = new Event(eventId, nameEvent, typeEvent, timeEvent, location, eventScenario, price, capacity, guests, participants);
                            return eventData;
                        }
                    }
                }
            }
            return null;
        }

        public void Add(Event newEvent)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO event (name_event, type_event, time_event, id_location, id_event_scen, price, capacity, state_event) " +
                    "VALUES (@eventName, @eventType, @eventTime, @locationId, @scenarioId, @price, @capacity, 'planning')";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("eventName", newEvent.eventName);
                command.Parameters.AddWithValue("eventType", newEvent.eventType);
                command.Parameters.AddWithValue("eventTime", newEvent.eventTime);
                command.Parameters.AddWithValue("locationId", newEvent.location.locationId);
                command.Parameters.AddWithValue("scenarioId", newEvent.eventScenario.eventScenarioId);
                command.Parameters.AddWithValue("price", newEvent.price);
                command.Parameters.AddWithValue("capacity", newEvent.capacity);

                command.ExecuteNonQuery();
            }
        }

        public void Remove(int eventId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM event WHERE id_event = @eventId";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("eventId", eventId);

                command.ExecuteNonQuery();
            }
        }

        public void Update(Event updatedEvent)
        {
            updatedEvent.State.Update(updatedEvent, _connectionString);
        }

        public void ChangeState(Event updatedEvent)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE event SET state_event = @state WHERE id_event = @eventId";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("eventId", updatedEvent.eventId);

                switch (updatedEvent.State.GetType().Name)
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
        }

        public EventScenario GetLastScenrio()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM eventscenario ORDER BY id_event_scen DESC LIMIT 1";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int scenarioId = reader.GetInt32(reader.GetOrdinal("id_event_scen"));
                            string locationPrep = reader.GetString(reader.GetOrdinal("location_prep"));
                            string description = reader.GetString(reader.GetOrdinal("desc_event_scen"));
                            string plan = reader.GetString(reader.GetOrdinal("plan_event_scen"));

                            EventScenario eventScenario = new EventScenario(scenarioId, locationPrep, description, plan);
                            return eventScenario;
                        }
                    }
                }
            }
            return null;
        }

        public EventScenario GetScenarioById(int scenarioId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM eventscenario WHERE id_event_scen = @scenarioId";
                    command.Parameters.AddWithValue("scenarioId", scenarioId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string locationPrep = reader.GetString(reader.GetOrdinal("location_prep"));
                            string description = reader.GetString(reader.GetOrdinal("desc_event_scen"));
                            string plan = reader.GetString(reader.GetOrdinal("plan_event_scen"));

                            EventScenario eventScenario = new EventScenario(scenarioId, locationPrep, description, plan);
                            return eventScenario;
                        }
                    }
                }
            }
            return null;
        }

        public void AddScenario(EventScenario eventScenario)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO eventscenario (location_prep, desc_event_scen, plan_event_scen) " +
                    "VALUES (@locationPrep, @eventDescription, @eventPlan)";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("locationPrep", eventScenario.locationPreparation);
                command.Parameters.AddWithValue("eventDescription", eventScenario.eventDescription);
                command.Parameters.AddWithValue("eventPlan", eventScenario.eventPlan);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateScenario(EventScenario eventScenario)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE eventscenario SET location_prep = @locationPrep, desc_event_scen = @eventDescription, plan_event_scen = @eventPlan WHERE id_event_scen = @eventScenarioId";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("eventScenarioId", eventScenario.eventScenarioId);
                command.Parameters.AddWithValue("locationPrep", eventScenario.locationPreparation);
                command.Parameters.AddWithValue("eventDescription", eventScenario.eventDescription);
                command.Parameters.AddWithValue("eventPlan", eventScenario.eventPlan);

                command.ExecuteNonQuery();
            }
        }

        public List<IObserver> GetEventGuests(int eventId)
        {
            List<IObserver> guests = new List<IObserver>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT guest.* FROM guest JOIN event_guest ON guest.id_guest = event_guest.id_guest WHERE id_event = @eventId";
                    command.Parameters.AddWithValue("eventId", eventId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int guestId = reader.GetInt32(reader.GetOrdinal("id_guest"));
                            string firstName = reader.GetString(reader.GetOrdinal("fname_guest"));
                            string lastName = reader.GetString(reader.GetOrdinal("lname_guest"));
                            int age = reader.GetInt32(reader.GetOrdinal("age_guest"));
                            string phone = reader.GetString(reader.GetOrdinal("phone_guest"));
                            string email = reader.GetString(reader.GetOrdinal("email_guest"));

                            IObserver guest = new Guest(guestId, firstName, lastName, age, phone, email);
                            guests.Add(guest);
                        }
                    }
                    //можливо винести за юзінг комманда
                    return guests;
                }
            }
        }

        public List<Participant> GetEventParticipants(int eventId)
        {
            List<Participant> participants = new List<Participant>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT participant.* FROM participant JOIN event_participant ON participant.id_participant = event_participant.id_participant WHERE id_event = @eventId";
                    command.Parameters.AddWithValue("eventId", eventId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int participantId = reader.GetInt32(reader.GetOrdinal("id_participant"));
                            string firstName = reader.GetString(reader.GetOrdinal("fname_participant"));
                            string lastName = reader.GetString(reader.GetOrdinal("lname_participant"));
                            string speciality = reader.GetString(reader.GetOrdinal("spec_participant"));
                            string phone = reader.GetString(reader.GetOrdinal("phone_participant"));
                            string email = reader.GetString(reader.GetOrdinal("email_participant"));
                            decimal price = reader.GetInt32(reader.GetOrdinal("price_participant"));

                            Participant participant = new Participant(participantId, firstName, lastName, speciality, phone, email, price);
                            participants.Add(participant);
                        }
                    }
                    //можливо винести за юзінг комманда
                    return participants;
                }
            }
        }
    }
}
