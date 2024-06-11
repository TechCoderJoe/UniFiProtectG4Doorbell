using Microsoft.EntityFrameworkCore;
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
    public class SoundRepository : ISoundRepository
    {
        private DoorbellContext _context;

        public SoundRepository(DoorbellContext context)
        {
            _context = context;
        }

        public async Task<int> AddSound(SoundItem soundItem)
        {
            _context.Sounds.Add(soundItem);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }

        public SoundItem GetSoundItemById(int id)
        {
            SoundItem? item = _context.Sounds.Where(w => w.soundId == id).FirstOrDefault();

            if (item == null)
            {
                throw new NullReferenceException($"No sound found with id of {id}");
            }

            return item;
        }

        public List<SoundItem> GetSounds()
        {
            List<SoundItem> items = _context.Sounds.ToList();

            if (items == null)
            {
                items = new();
            }

            return items;
        }

        public async Task<int> RemoveSound(SoundItem soundItem)
        {
            _context.Sounds.Remove(soundItem);
            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }
    }
}
