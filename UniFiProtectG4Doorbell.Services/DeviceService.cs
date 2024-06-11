
using UniFiProtectG4Doorbell.Data.Entities;
using UniFiProtectG4Doorbell.Data.Interfaces;
using UniFiProtectG4Doorbell.Services.Helpers;
using UniFiProtectG4Doorbell.Services.Interfaces;

namespace UniFiProtectG4Doorbell.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ISettingsRepository _settingsRepository;

        public DeviceService(IDeviceRepository deviceRepository, ISettingsRepository settingsRepository)
        {
            _deviceRepository = deviceRepository;
            _settingsRepository = settingsRepository;
        }

        public List<Models.DeviceInfo> GetDevices()
        {
            return _deviceRepository.GetDevices().Select(s => s.ToDTO()).ToList();
        }

        public Models.DeviceInfo? GetDevice(int deviceInfoId)
        {
            try
            {
                DoorbellItem entity = _deviceRepository.GetDeviceItemById(deviceInfoId);
                return entity.ToDTO();
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> SaveDevice(Models.DeviceInfo deviceInfo)
        {
            if(deviceInfo.doorbellId > 0)
            {
                DoorbellItem entity = _deviceRepository.GetDeviceItemById(deviceInfo.doorbellId);

                entity.doorbellName = deviceInfo.doorbellName;
                entity.ipAddress = deviceInfo.ipAddress;
                return await _deviceRepository.UpdateDevice(entity);
            }
            else
            {
                return await _deviceRepository.AddDevice(deviceInfo.ToEntity());
            }
        }

        public async Task<int> RemoveDevice(int deviceInfoId)
        {
            DoorbellItem entity = _deviceRepository.GetDeviceItemById(deviceInfoId);
            return await _deviceRepository.RemoveDevice(entity);
        }

        public Models.Validation isValid(Models.DeviceInfo deviceInfo)
        {
            Models.Validation result = new() {
                isValid = true
            };

            if(!Helpers.sshManager.isIpValid(deviceInfo.ipAddress))
            {
                result.isValid = false;
                result.message = "Invalid IP Address";
                result.key = "ipAddress";
                return result;
            }

            bool ipExisits = _deviceRepository.GetDevices().Any(a => 
               string.Equals(a.ipAddress, deviceInfo.ipAddress, StringComparison.OrdinalIgnoreCase)
               && a.doorbellId != deviceInfo.doorbellId);

            if(ipExisits)
            {
                result.isValid = false;
                result.message = $"IP Address of {deviceInfo.ipAddress} already exists";
                result.key = "ipAddress";
                return result;
            }

            bool nameExisits = _deviceRepository.GetDevices().Any(a =>
                string.Equals(a.doorbellName, deviceInfo.doorbellName, StringComparison.OrdinalIgnoreCase)
               && a.doorbellId != deviceInfo.doorbellId);

            if (nameExisits)
            {
                result.isValid = false;
                result.message = $"Device Name of {deviceInfo.doorbellName} already exists";
                result.key = "doorbellName";
                return result;
            }

            bool connectionTest = sshManager.testConnection(deviceInfo.ipAddress, _settingsRepository.GetProtectUsername(), _settingsRepository.GetProtectPassword());

            if(!connectionTest)
            {
                result.isValid = false;
                result.message = $"Failed to connect to {deviceInfo.ipAddress}. Please check the IP address and the Protect username/password in the settings";
                result.key = "ipAddress";
                return result;
            }

            return result;
        }
    }
}
