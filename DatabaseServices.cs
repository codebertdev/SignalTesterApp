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

        public void SaveDecodedValue(string inputType, string raw1, string? raw2, string output)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = new MySqlCommand(
                "INSERT INTO decoded_data (input_type, raw1, raw2, output) VALUES (@inputType, @raw1, @raw2, @output)", conn);
            cmd.Parameters.AddWithValue("@inputType", inputType);
            cmd.Parameters.AddWithValue("@raw1", raw1);
            cmd.Parameters.AddWithValue("@raw2", (object?)raw2 ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@output", output);

            cmd.ExecuteNonQuery();
        }

    }
}
