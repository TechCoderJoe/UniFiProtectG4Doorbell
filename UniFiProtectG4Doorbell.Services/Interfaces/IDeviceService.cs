
namespace UniFiProtectG4Doorbell.Services.Interfaces
{
    public interface IDeviceService
    {
        List<Models.DeviceInfo> GetDevices();

        Models.DeviceInfo? GetDevice(int deviceInfoId);

        Task<int> SaveDevice(Models.DeviceInfo deviceInfo);

        Task<int> RemoveDevice(int deviceInfoId);

        Models.Validation isValid(Models.DeviceInfo deviceInfo);
    }
}
