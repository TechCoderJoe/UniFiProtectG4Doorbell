using UniFiProtectG4Doorbell.Data.Entities;

namespace UniFiProtectG4Doorbell.Services.Helpers
{
    public static class ExtensionMethods
    {
        public static Models.DeviceInfo ToDTO(this DoorbellItem entity)
        {
            return new Models.DeviceInfo()
            {
                doorbellId = entity.doorbellId,
                ipAddress = entity.ipAddress,
                doorbellName = entity.doorbellName
            };
        }

        public static DoorbellItem ToEntity(this Models.DeviceInfo item)
        {
            return new DoorbellItem()
            {
                doorbellId = item.doorbellId,
                ipAddress = item.ipAddress,
                doorbellName = item.doorbellName
            };
        }

        public static Models.SoundInfo ToDTO(this SoundItem entity)
        {
            return new Models.SoundInfo()
            {
                soundId = entity.soundId,
                soundFileName = entity.soundFileName
            };

        }

        public static SoundItem ToEntity(this Models.SoundInfo item)
        {
            return new SoundItem()
            {
                 soundId = item.soundId,
                  soundFileName= item.soundFileName
            };
        }

        public static Models.DoorbellSoundLinkInfo ToDTO(this DoorbellSoundLink entity)
        {
            return new Models.DoorbellSoundLinkInfo()
            {
                doorbellId = entity.doorbellId,
                doorbellSoundLinkId = entity.doorbellSoundLinkId,
                endDate = entity.endDate,
                isDefault = entity.isDefault,
                soundId = entity.soundId,
                startDate = entity.startDate
            };
        }

        public static DoorbellSoundLink ToEntity(this Models.DoorbellSoundLinkInfo item)
        {
            return new DoorbellSoundLink()
            {
                doorbellId = item.doorbellId,
                doorbellSoundLinkId = item.doorbellSoundLinkId,
                endDate = item.endDate,
                isDefault = item.isDefault,
                soundId = item.soundId,
                startDate = item.startDate
            };
        }
    }
}
