
namespace UniFiProtectG4Doorbell.Services.Interfaces
{
    public interface ISettingsService
    {
        Models.ProtectInfo GetProtectInfo();

        Task<int> SetProtectInfo(Models.ProtectInfo protectInfo);

        Models.UniFiInfo GetUniFiInfo();

        Task<int> SetUniFiInfo(Models.UniFiInfo uniFiInfo);

        bool GetIsFirstTimeSetup();

        Task<int> SetIsFirstTimeSetup(bool value);

        string GetDoorbellCustomSoundFileName();

        int GetCurrentSetupStep();

        Task<int> SetCurrentSetupStep(int stepNumber);
    }
}
