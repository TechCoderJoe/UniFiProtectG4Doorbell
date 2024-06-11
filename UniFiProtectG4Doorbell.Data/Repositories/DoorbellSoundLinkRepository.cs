using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniFiProtectG4Doorbell.Data.Context;
using UniFiProtectG4Doorbell.Data.Entities;
using UniFiProtectG4Doorbell.Data.Interfaces;

namespace UniFiProtectG4Doorbell.Data.Repositories
{
    public class DoorbellSoundLinkRepository : IDoorbellSoundLinkRepository
    {
        private DoorbellContext _context;

        public DoorbellSoundLinkRepository(DoorbellContext context)
        {
            _context = context;
        }

        public List<DoorbellSoundLink> GetDoorbellSoundLinks()
        {
            List<Entities.DoorbellSoundLink> items = _context.DeviceSoundLinks.ToList();

            if (items == null)
            {
                items = new();
            }

            return items;
        }

        public DoorbellSoundLink GetDoorbellSoundLinkById(int id)
        {
            DoorbellSoundLink? item = _context.DeviceSoundLinks.Where(w => w.doorbellSoundLinkId == id).FirstOrDefault();

            if (item == null)
            {
                throw new NullReferenceException($"No sound link found with id of {id}");
            }

            return item;
        }

        public List<DoorbellSoundLink> GetDoorbellSoundLinkByDoorbellId(int id)
        {
            List<DoorbellSoundLink> items = _context.DeviceSoundLinks.Where(w => w.doorbellId == id).ToList();

            if (items == null)
            {
                items = new();
            }

            return items;
        }

        public List<DoorbellSoundLink> GetDoorbellSoundLinkBySoundId(int id)
        {
            List<DoorbellSoundLink> items = _context.DeviceSoundLinks.Where(w => w.soundId == id).ToList();

            if (items == null)
            {
                items = new();
            }

            return items;
        }

        public async Task<int> AddDoorbellSoundLink(DoorbellSoundLink doorbellSoundLinkItem)
        {
            _context.DeviceSoundLinks.Add(doorbellSoundLinkItem);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }

        public async Task<int> UpdateDoorbellSoundLink(DoorbellSoundLink doorbellSoundLinkItem)
        {
            _context.Update(doorbellSoundLinkItem);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }

        public async Task<int> RemoveDoorbellSoundLink(DoorbellSoundLink doorbellSoundLinkItem)
        {
            _context.DeviceSoundLinks.Remove(doorbellSoundLinkItem);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }
    }
}
