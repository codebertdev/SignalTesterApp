using System;
using System.IO;

namespace SignalTesterApp
{
    public static class DecoderService
    {
        public static (string Input, string Output) Decode(string type, string raw1, string? raw2)
        {
            switch (type.ToLower())
            {
                case "multi":
                    if (raw2 == null)
                        return ($"{raw1}", "Hiányzik a második érték (raw2) a 'multi' típushoz");

                    var high = Convert.ToByte(raw1, 16);
                    var low = Convert.ToByte(raw2, 16);
                    float scaled = ((high << 8) | low) / 10.0f;
                    return ($"{raw1} {raw2}", $"{scaled}");

                case "bcd":
                    byte bcd = Convert.ToByte(raw1, 16);
                    int decoded = ((bcd >> 4) * 10) + (bcd & 0x0F);
                    return ($"{raw1}", $"{decoded}");

                case "twos":
                    short signed = unchecked((short)Convert.ToUInt16(raw1, 16));
                    float val = signed / 100f;
                    return ($"{raw1}", $"{val}");

                case "bitfield":
                    try
                    {
                        var cleaned = raw1.StartsWith("0b") ? raw1[2..] : raw1;
                        byte bits = Convert.ToByte(cleaned, 2);
                        return ($"{raw1}", $"Error: {(bits & 0x01) != 0}, Overheat: {(bits & 0x08) != 0}, Ready: {(bits & 0x80) != 0}");
                    }
                    catch
                    {
                        return ($"{raw1}", "Érvénytelen bitfield formátum");
                    }

                case "adc":
                    if (!int.TryParse(raw1, out int adc))
                        return ($"{raw1}", "Érvénytelen ADC érték");

                    float voltage = adc / 1023f * 5f;
                    return ($"{raw1}", $"{voltage:F2} V");

                default:
                    return ($"{raw1}", "Ismeretlen típus");
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
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

                        var (input, output) = DecoderService.Decode(type, raw1, raw2);
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
