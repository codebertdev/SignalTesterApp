namespace SignalTesterApp.Services
{
    public interface IDecoderService
    {
        (string Input, string Output) Decode(string type, string raw1, string? raw2);
    }
}
