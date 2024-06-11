using UniFiProtectG4Doorbell.Data.Entities;

namespace UniFiProtectG4Doorbell.Data.Interfaces
{
    public interface IDeviceRepository
    {
        List<Entities.DoorbellItem> GetDevices();

        Entities.DoorbellItem GetDeviceItemById(int id);

        Task<int> AddDevice(DoorbellItem deviceItem);

        Task<int> UpdateDevice(DoorbellItem deviceItem);

        Task<int> RemoveDevice(DoorbellItem deviceItem);
    }
}
