using UniFiProtectG4Doorbell.Data.Entities;

namespace UniFiProtectG4Doorbell.Data.Interfaces
{
    public interface IDoorbellSoundLinkRepository
    {
        List<DoorbellSoundLink> GetDoorbellSoundLinks();

        DoorbellSoundLink GetDoorbellSoundLinkById(int id);

        List<DoorbellSoundLink> GetDoorbellSoundLinkByDoorbellId(int id);

        List<DoorbellSoundLink> GetDoorbellSoundLinkBySoundId(int id);


        Task<int> AddDoorbellSoundLink(DoorbellSoundLink doorbellSoundLinkItem);

        Task<int> UpdateDoorbellSoundLink(DoorbellSoundLink doorbellSoundLinkItem);

        Task<int> RemoveDoorbellSoundLink(DoorbellSoundLink doorbellSoundLinkItem);
    }
}
