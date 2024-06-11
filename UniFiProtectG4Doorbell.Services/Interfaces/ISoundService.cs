
namespace UniFiProtectG4Doorbell.Services.Interfaces
{
    public interface ISoundService
    {
        List<Models.SoundInfo> GetSounds();

        Models.SoundInfo? GetSound(int soundInfoId);

        Task<int> SaveSound(Models.SoundInfo soundInfo);

        Task<int> RemoveSound(int soundInfoId);
    }
}
