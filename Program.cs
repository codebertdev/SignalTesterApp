using System;
using System.IO;
using SignalTesterApp.Services;

namespace SignalTesterApp
{
    public class Program
    {
        public static void Main()
        {
            var decoder = new DecoderService();

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

