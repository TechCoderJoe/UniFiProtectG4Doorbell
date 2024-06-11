
using UniFiProtectG4Doorbell.Data.Interfaces;
using UniFiProtectG4Doorbell.Services.Interfaces;

namespace UniFiProtectG4Doorbell.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _settingsRepository;

        public SettingsService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public Models.ProtectInfo GetProtectInfo()
        {
            string username;
            string password;
            string soundFileName;
            string soundProcess;
            string soundUploadPath;
            try
            {
                username = _settingsRepository.GetProtectUsername();
                password = _settingsRepository.GetProtectPassword();
                soundFileName = _settingsRepository.GetDoorbellCustomSoundFileName();
                soundProcess = _settingsRepository.GetProtectSoundProcess();
                soundUploadPath = _settingsRepository.GetProtectSoundUploadPath();
            }
            catch //Set default values
            {
                username = "ubnt";
                password = string.Empty;
                soundFileName = "custom.wav";
                soundProcess = "ubnt_sounds_leds";
                soundUploadPath = "var/etc/sounds/";
            }

            return new Models.ProtectInfo {
                password = password, 
                username = username, 
                soundFileName = soundFileName, 
                soundProcess = soundProcess, 
                soundUploadPath = soundUploadPath 
            };
        }

        public async Task<int> SetProtectInfo(Models.ProtectInfo protectInfo)
        {
            return await _settingsRepository.SaveProtectInfo(protectInfo);
        }

        public Models.UniFiInfo GetUniFiInfo()
        {
            string ipAddress = _settingsRepository.GetUniFiIpAddress();
            string username = _settingsRepository.GetUniFiUsername();
            string password = _settingsRepository.GetUniFiPassword();

            return new Models.UniFiInfo { ipAddress = ipAddress, password = password, username = username };
        }

        public async Task<int> SetUniFiInfo(Models.UniFiInfo uniFiInfo)
        {
            return await _settingsRepository.SaveUniFiInfo(uniFiInfo.ipAddress, uniFiInfo.username, uniFiInfo.password);
        }

        public bool GetIsFirstTimeSetup()
        {
            try
            {
                return _settingsRepository.IsFirstTimeSetup();
            }
            catch
            {
                return true;
            }
        }

        public async Task<int> SetIsFirstTimeSetup(bool value)
        {
            return await _settingsRepository.SaveFirstTimeSetup(value);
        }
        public string GetDoorbellCustomSoundFileName()
        {
            return _settingsRepository.GetDoorbellCustomSoundFileName();
        }

        public int GetCurrentSetupStep()
        {
            return _settingsRepository.GetCurrentSetupStep();
        }

        public async Task<int> SetCurrentSetupStep(int stepNumber)
        {
            try
            {
                return await _settingsRepository.SaveCurrentSetupStep(stepNumber);
            }
            catch
            {
                return 1;
            }
        }
    }
}
