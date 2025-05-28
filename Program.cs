using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using SignalTesterApp.Services;

namespace SignalTesterApp
{
    public class Program
    {
        public static void Main()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json", optional: false)
                .Build();

            string? connStr = config.GetConnectionString("MySql");

            if (string.IsNullOrWhiteSpace(connStr))
                throw new InvalidOperationException("Hiányzik a MySql kapcsolati string az appsettings.json fájlból!");

            Console.WriteLine($"Kapcsolati string: {connStr}");

            var decoder = new DecoderService();
            var dbService = new DatabaseService(connStr);

            try
            {
                var lines = File.ReadAllLines("Data/sample_data.csv")[1..];
                foreach (var line in lines)
                {
                    try
                    {
                        var parts = line.Split(';');
                        var type = parts[0];
                        var raw1 = parts[1];
                        var raw2 = parts.Length > 2 ? parts[2] : null;

                        var (input, output) = decoder.Decode(type, raw1, raw2);

                        Console.WriteLine($"[{type}] {input} → {output}");

                        dbService.SaveDecodedValue(type, raw1, raw2, output);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Hiba a sor feldolgozásakor: '{line}' → {ex.Message}");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Hiba: Nem található a 'Data/sample_data.csv' fájl.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Általános hiba: {ex.Message}");
            }
        }
    }
}

