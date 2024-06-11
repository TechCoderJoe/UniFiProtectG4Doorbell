
using UniFiProtectG4Doorbell.Data.Entities;
using UniFiProtectG4Doorbell.Data.Interfaces;
using UniFiProtectG4Doorbell.Services.Helpers;
using UniFiProtectG4Doorbell.Services.Interfaces;

namespace UniFiProtectG4Doorbell.Services
{
    public class SoundService : ISoundService
    {
        private readonly ISoundRepository _soundRepository;

        public SoundService(ISoundRepository soundRepository)
        {
            _soundRepository = soundRepository;
        }

        public List<Models.SoundInfo> GetSounds()
        {
            return _soundRepository.GetSounds().Select(s => s.ToDTO()).ToList();
        }

        public Models.SoundInfo? GetSound(int soundInfoId)
        {
            try
            {
                SoundItem entity = _soundRepository.GetSoundItemById(soundInfoId);
                return entity.ToDTO();
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> SaveSound(Models.SoundInfo soundInfo)
        {
            return await _soundRepository.AddSound(soundInfo.ToEntity());
        }

        public async Task<int> RemoveSound(int soundInfoId)
        {
            SoundItem entity = _soundRepository.GetSoundItemById(soundInfoId);
            return await _soundRepository.RemoveSound(entity);
        }
    }
}
