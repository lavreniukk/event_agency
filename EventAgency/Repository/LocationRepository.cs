using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace EventAgency.Repository
{
    public class LocationRepository : IRepository<Location>
    {
        private string _connectionString;

        public LocationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Location> GetAll()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM location";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int locationId = reader.GetInt32(reader.GetOrdinal("id_location"));
                            string address = reader.GetString(reader.GetOrdinal("address_location"));
                            string city = reader.GetString(reader.GetOrdinal("city_location"));
                            string country = reader.GetString(reader.GetOrdinal("country_location"));
                            string type = reader.GetString(reader.GetOrdinal("type_location"));
                            int maxCapacity = reader.GetInt32(reader.GetOrdinal("max_capacity"));

                            Location location = new Location(locationId, address, city, country, type, maxCapacity);
                            yield return location;
                        }
                    }
                }
            }
        }

        public Location GetById(int locationId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM location WHERE id_location = @locationId";
                    command.Parameters.AddWithValue("locationId", locationId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string address = reader.GetString(reader.GetOrdinal("address_location"));
                            string city = reader.GetString(reader.GetOrdinal("city_location"));
                            string country = reader.GetString(reader.GetOrdinal("country_location"));
                            string type = reader.GetString(reader.GetOrdinal("type_location"));
                            int maxCapacity = reader.GetInt32(reader.GetOrdinal("max_capacity"));

                            return new Location(locationId, address, city, country, type, maxCapacity);
                        }
                    }
                }
            }
            return null;
        }

        public void Add(Location newLocation)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO location (address_location, city_location, country_location, type_location, max_capacity) " +
                    "VALUES (@addressLocation, @cityLocation, @countryLocation, @locationType, @maxCapacity)";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("addressLocation", newLocation.address);
                command.Parameters.AddWithValue("cityLocation", newLocation.city);
                command.Parameters.AddWithValue("countryLocation", newLocation.country);
                command.Parameters.AddWithValue("locationType", newLocation.locationType);
                command.Parameters.AddWithValue("maxCapacity", newLocation.maxCapacity);

                command.ExecuteNonQuery();
            }
        }

        public void Remove(int locationId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM location WHERE id_location = @locationId";

                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                command.Parameters.AddWithValue("locationId", locationId);

                command.ExecuteNonQuery();
            }
        }

        public void Update(Location updatedLocation)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                string query = "UPDATE location SET address_location = @addressLocation, city_location = @cityLocation, country_location = @countryLocation, type_location = @locationType, max_capacity = @maxCapacity WHERE id_location = @locationId";
                NpgsqlCommand command = new NpgsqlCommand(query, connection);

                command.Parameters.AddWithValue("locationId", updatedLocation.locationId);
                command.Parameters.AddWithValue("addressLocation", updatedLocation.address);
                command.Parameters.AddWithValue("cityLocation", updatedLocation.city);
                command.Parameters.AddWithValue("countryLocation", updatedLocation.country);
                command.Parameters.AddWithValue("locationType", updatedLocation.locationType);
                command.Parameters.AddWithValue("maxCapacity", updatedLocation.maxCapacity);

                command.ExecuteNonQuery();
            }
        }
    }
}
