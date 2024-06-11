using UniFiProtectG4Doorbell.Data.Entities;

namespace UniFiProtectG4Doorbell.Data.Interfaces
{
    public interface ISoundRepository
    {
        List<SoundItem> GetSounds();

        SoundItem GetSoundItemById(int id);

        Task<int> AddSound(SoundItem soundItem);

        Task<int> RemoveSound(SoundItem soundItem);
    }
}
