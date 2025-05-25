using System;

namespace SignalTesterApp.Services
{
    public class DecoderService : IDecoderService
    {
        public (string Input, string Output) Decode(string type, string raw1, string? raw2)
        {
            if (string.IsNullOrWhiteSpace(raw1))
                throw new ArgumentException($"{nameof(raw1)} argument is invalid!");

            switch (type.ToLowerInvariant())
            {
                case "multi":
                    if (raw2 is null)
                        throw new ArgumentException("Hiányzik a második érték (raw2) a 'multi' típushoz");

                    var highByte = Convert.ToByte(raw1, 16);
                    var lowByte = Convert.ToByte(raw2, 16);
                    var scaledValue = ((highByte << 8) | lowByte) / 10.0f;

                    return ($"{raw1} {raw2}", $"{scaledValue}");

                case "bcd":
                    var bcdByte = Convert.ToByte(raw1, 16);
                    var decodedValue = ((bcdByte >> 4) * 10) + (bcdByte & 0x0F);

                    return ($"{raw1}", $"{decodedValue}");

                case "twos":
                    var signedShort = unchecked((short)Convert.ToUInt16(raw1, 16));
                    var value = signedShort / 100f;

                    return ($"{raw1}", $"{value}");

                case "bitfield":
                    try
                    {
                        var binary = raw1.StartsWith("0b") ? raw1[2..] : raw1;
                        var flags = Convert.ToByte(binary, 2);

                        return ($"{raw1}", $"Hiba: {(flags & 0x01) != 0}, Túlmelegedés: {(flags & 0x08) != 0}, Elérhető: {(flags & 0x80) != 0}");
                    }
                    catch (FormatException)
                    {
                        return ($"{raw1}", "Érvénytelen bitfield formátum");
                    }

                case "adc":
                    if (!int.TryParse(raw1, out var adcRaw))
                        throw new ArgumentException("Érvénytelen ADC érték");

                    var voltage = adcRaw / 1023f * 5f;

                    return ($"{raw1}", $"{voltage:F2} V");

                default:
                    return ($"{raw1}", "Ismeretlen típus");
            }
        }
    }
}
