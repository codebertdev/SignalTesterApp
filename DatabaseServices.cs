using MySql.Data.MySqlClient;
using System;

namespace SignalTesterApp.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void SaveDecodedValue(string type, string input, string output)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = new MySqlCommand(
                "INSERT INTO decoded_data (type, input, output) VALUES (@type, @input, @output)", conn);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@input", input);
            cmd.Parameters.AddWithValue("@output", output);

            cmd.ExecuteNonQuery();
        }
    }
}
