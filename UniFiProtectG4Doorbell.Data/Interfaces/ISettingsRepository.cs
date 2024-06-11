
namespace UniFiProtectG4Doorbell.Data.Interfaces
{
    public interface ISettingsRepository
    {
        string GetProtectUsername();

        string GetProtectPassword();

        string GetProtectSoundUploadPath();

        string GetProtectSoundProcess();

        string GetUniFiIpAddress();

        string GetUniFiUsername();

        string GetUniFiPassword();

        string GetDoorbellCustomSoundFileName();

        bool IsFirstTimeSetup();

        int GetCurrentSetupStep();

        Task<int> SaveProtectInfo(Models.ProtectInfo protectInfo);

        Task<int> SaveUniFiInfo(string ipaddress, string username, string password);

        Task<int> SaveFirstTimeSetup(bool value);

        Task<int> SaveCurrentSetupStep(int value);
    }
}
