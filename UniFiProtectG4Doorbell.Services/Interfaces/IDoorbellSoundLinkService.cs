
using UniFiProtectG4Doorbell.Models;

namespace UniFiProtectG4Doorbell.Services.Interfaces
{
    public interface IDoorbellSoundLinkService
    {
        List<Models.DoorbellSoundLinkInfo> GetDoorbellSoundLinks();

        List<Models.ExtendedDeviceInfo> GetDoorbellSoundLinksGroupByDoorbell();

        List<Models.ExtendedSoundInfo> GetDoorbellSoundLinksGroupBySound();

        Models.DoorbellSoundLinkInfo? GetDoorbellSoundLinkByLinkId(int linkId);

        Models.ExtendedDeviceInfo? GetDoorbellSoundLinkByDoorbellId(int doorbellId);

        Models.ExtendedSoundInfo? GetDoorbellSoundLinkBySoundId(int soundId);

        Task<int> SaveDoorbellSoundLink(DoorbellSoundLinkInfo doorbellSoundLinkInfo);

        Task<int> RemoveDoorbellSoundLink(int linkId);

        Models.Validation isValid(Models.DoorbellSoundLinkInfo DoorbellSoundLinkInfo);
    }
}
