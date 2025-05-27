using System;
using Xunit;
using SignalTesterApp.Services;

namespace SignalTesterApp.Tests
{
    public class DecoderServiceTests
    {
        private readonly DecoderService _decoderService = new DecoderService();

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Decode_InvalidRaw1_ThrowsArgumentException(string raw1)
        {
            Assert.Throws<ArgumentException>(() => _decoderService.Decode("bcd", raw1, null));
        }

        [Theory]
        [InlineData("bcd", "2A", null, "42")]
        [InlineData("adc", "1023", null, "5.00 V")]
        [InlineData("twos", "FF9C", null, "-1.00")]
        [InlineData("multi", "01", "2C", "30.0")]
        public void Decode_ValidInput_ReturnsExpectedOutput(string type, string raw1, string? raw2, string expectedOutput)
        {
            var result = _decoderService.Decode(type, raw1, raw2);
            Assert.Equal(expectedOutput, result.Output);
        }

        [Fact]
        public void Decode_BitfieldValid_ReturnsFlags()
        {
            var result = _decoderService.Decode("bitfield", "0b10001001", null);
            Assert.Equal("Hiba: True, Túlmelegedés: True, Elérhetõ: True", result.Output);
        }

        [Fact]
        public void Decode_MissingRaw2ForMulti_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _decoderService.Decode("multi", "01", null));
        }

        [Fact]
        public void Decode_UnknownType_ReturnsError()
        {
            var result = _decoderService.Decode("valami", "12", null);
            Assert.Equal("Ismeretlen típus", result.Output);
        }
    }
}
