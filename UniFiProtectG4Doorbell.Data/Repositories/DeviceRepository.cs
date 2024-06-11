using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniFiProtectG4Doorbell.Data.Context;
using UniFiProtectG4Doorbell.Data.Interfaces;
using UniFiProtectG4Doorbell.Data.Entities;

namespace UniFiProtectG4Doorbell.Data.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private DoorbellContext _context;

        public DeviceRepository(DoorbellContext context)
        {
            _context = context;
        }

        public List<DoorbellItem> GetDevices()
        {
            List<DoorbellItem> items = _context.Devices.ToList();

            if (items == null)
            {
                items = new();
            }

            return items;
        }

        public DoorbellItem GetDeviceItemById(int id)
        {
            DoorbellItem? item = _context.Devices.Where(w=>w.doorbellId == id).FirstOrDefault();

            if (item == null)
            {
                throw new NullReferenceException($"No device found with id of {id}");
            }

            return item;
        }

        public async Task<int> AddDevice(DoorbellItem deviceItem)
        {
            _context.Devices.Add(deviceItem);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }

        public async Task<int> UpdateDevice(DoorbellItem deviceItem)
        {
            _context.Update(deviceItem);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }

        public async Task<int> RemoveDevice(DoorbellItem deviceItem)
        {
            _context.Devices.Remove(deviceItem);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }
    }
}
