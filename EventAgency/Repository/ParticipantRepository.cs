using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAgency.State;
using Npgsql;

namespace EventAgency.Repository
{
    public class ParticipantRepository : IRepository<Participant>
    {
        private string _connectionString;


        public ParticipantRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Participant> GetAll()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM participant";

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
                            decimal price = reader.GetDecimal(reader.GetOrdinal("price_participant"));

                            Participant participant = new Participant(participantId, firstName, lastName, speciality, phone, email, price);
                            yield return participant;
                        }
                    }
                }
            }
        }

        public Participant GetById(int participantId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM participant WHERE id_participant = @participantId";
                    command.Parameters.AddWithValue("participantId", participantId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string firstName = reader.GetString(reader.GetOrdinal("fname_participant"));
                            string lastName = reader.GetString(reader.GetOrdinal("lname_participant"));
                            string speciality = reader.GetString(reader.GetOrdinal("spec_participant"));
                            string phone = reader.GetString(reader.GetOrdinal("phone_participant"));
                            string email = reader.GetString(reader.GetOrdinal("email_participant"));
                            decimal price = reader.GetDecimal(reader.GetOrdinal("price_participant"));

                            Participant participant = new Participant(participantId, firstName, lastName, speciality, phone, email, price);
                            return participant;
                        }
                    }
                }
            }
            return null;
        }

        public void Add(Participant newParticipant)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO participant (fname_participant, lname_participant, spec_participant, phone_participant, email_participant, price_participant) " +
                    "VALUES (@fnameParticipant, @lnameParticipant, @specParticipant, @phoneParticipant, @emailParticipant, @priceParticipant)";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("fnameParticipant", newParticipant.firstName);
                command.Parameters.AddWithValue("lnameParticipant", newParticipant.lastName);
                command.Parameters.AddWithValue("specParticipant", newParticipant.speciality);
                command.Parameters.AddWithValue("phoneParticipant", newParticipant.phoneNum);
                command.Parameters.AddWithValue("emailParticipant", newParticipant.email);
                command.Parameters.AddWithValue("priceParticipant", newParticipant.perHourPrice);

                command.ExecuteNonQuery();
            }
        }

        public void Remove(int participantId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM participant WHERE id_participant = @participantId";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("participantId", participantId);

                command.ExecuteNonQuery();
            }
        }

        public void Update(Participant updatedParticipant)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE participant SET fname_participant = @fnameParticipant, lname_participant = @lnameParticipant, spec_participant = @specParticipant, phone_participant = @phoneParticipant, email_participant = @emailParticipant, price_participant = @priceParticipant WHERE id_participant = @participantId";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("participantId", updatedParticipant.participantId);
                command.Parameters.AddWithValue("fnameParticipant", updatedParticipant.firstName);
                command.Parameters.AddWithValue("lnameParticipant", updatedParticipant.lastName);
                command.Parameters.AddWithValue("specParticipant", updatedParticipant.speciality);
                command.Parameters.AddWithValue("phoneParticipant", updatedParticipant.phoneNum);
                command.Parameters.AddWithValue("emailParticipant", updatedParticipant.email);
                command.Parameters.AddWithValue("priceParticipant", updatedParticipant.perHourPrice);

                command.ExecuteNonQuery();
            }
        }
        
        public void AssignParticipantToEvent(Event toEvent, Participant participant, int workHours)
        {
            toEvent.State.AssignParticipantToEvent(toEvent, participant, workHours, _connectionString);
        }
    }
}
