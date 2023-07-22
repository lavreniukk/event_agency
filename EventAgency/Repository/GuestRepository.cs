using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventAgency.State;
using Npgsql;

namespace EventAgency.Repository
{
    public class GuestRepository : IRepository<Guest>
    {
        private string _connectionString;

        public GuestRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Guest> GetAll()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM guest";

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

                            Guest guest = new Guest(guestId, firstName, lastName, age, phone, email);
                            yield return guest;
                        }
                    }
                }
            }
        }

        public Guest GetById(int guestId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM guest WHERE id_guest = @guestId";
                    command.Parameters.AddWithValue("guestId", guestId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string firstName = reader.GetString(reader.GetOrdinal("fname_guest"));
                            string lastName = reader.GetString(reader.GetOrdinal("lname_guest"));
                            int age = reader.GetInt32(reader.GetOrdinal("age_guest"));
                            string phone = reader.GetString(reader.GetOrdinal("phone_guest"));
                            string email = reader.GetString(reader.GetOrdinal("email_guest"));

                            Guest guest = new Guest(guestId, firstName, lastName, age, phone, email);
                            return guest;
                        }
                    }
                }
            }
            return null;
        }

        public void Add(Guest newGuest)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO guest (fname_guest, lname_guest, age_guest, phone_guest, email_guest) " +
                    "VALUES (@fnameGuest, @lnameGuest, @ageGuest, @phoneGuest, @emailGuest)";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("fnameGuest", newGuest.firstName);
                command.Parameters.AddWithValue("lnameGuest", newGuest.lastName);
                command.Parameters.AddWithValue("ageGuest", newGuest.age);
                command.Parameters.AddWithValue("phoneGuest", newGuest.phoneNum);
                command.Parameters.AddWithValue("emailGuest", newGuest.email);

                command.ExecuteNonQuery();
            }
        }

        public void Update(Guest updatedGuest)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE guest SET fname_guest = @fnameGuest, lname_guest = @lnameGuest, age_guest = @ageGuest, phone_guest = @phoneGuest, email_guest = @emailGuest WHERE id_guest = @guestId";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("guestId", updatedGuest.guestId);
                command.Parameters.AddWithValue("fnameGuest", updatedGuest.firstName);
                command.Parameters.AddWithValue("lnameGuest", updatedGuest.lastName);
                command.Parameters.AddWithValue("ageGuest", updatedGuest.age);
                command.Parameters.AddWithValue("phoneGuest", updatedGuest.phoneNum);
                command.Parameters.AddWithValue("emailGuest", updatedGuest.email);

                command.ExecuteNonQuery();
            }
        }

        public void Remove(int guestId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM guest WHERE id_guest = @guestId";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("guestId", guestId);

                command.ExecuteNonQuery();
            }
        }

        public void AssignGuestToEvent(Event toEvent, Guest guest, string guestStatus)
        {
            toEvent.State.AssignGuestToEvent(toEvent, guest, guestStatus, _connectionString);
        }
    }
}
