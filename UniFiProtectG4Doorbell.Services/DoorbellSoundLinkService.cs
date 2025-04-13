using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniFiProtectG4Doorbell.Data.Entities;
using UniFiProtectG4Doorbell.Data.Interfaces;
using UniFiProtectG4Doorbell.Data.Repositories;
using UniFiProtectG4Doorbell.Models;
using UniFiProtectG4Doorbell.Services.Helpers;
using UniFiProtectG4Doorbell.Services.Interfaces;

namespace UniFiProtectG4Doorbell.Services
{
    public class DoorbellSoundLinkService : IDoorbellSoundLinkService
    {
        private readonly IDoorbellSoundLinkRepository _doorbellSoundLinkRepository;

        public DoorbellSoundLinkService(IDoorbellSoundLinkRepository doorbellSoundLinkRepository)
        {
            _doorbellSoundLinkRepository = doorbellSoundLinkRepository;
        }

        public List<DoorbellSoundLinkInfo> GetDoorbellSoundLinks()
        {
            return _doorbellSoundLinkRepository.GetDoorbellSoundLinks().Select(s=> s.ToDTO() ).ToList();
        }

        public ExtendedDeviceInfo? GetDoorbellSoundLinkByDoorbellId(int doorbellId)
        {
            List<DoorbellSoundLink> doorbellSoundLinks = _doorbellSoundLinkRepository.GetDoorbellSoundLinkByDoorbellId(doorbellId);

            if (doorbellSoundLinks == null || !doorbellSoundLinks.Any())
            {
                return null;
            }

            ExtendedDeviceInfo? result = doorbellSoundLinks
                .GroupBy(g => g.doorbell)
                .Where(w => w.Key != null)
                .Select(d => new ExtendedDeviceInfo()
                {
                    doorbellName = d.Key!.doorbellName,
                    doorbellId = d.Key!.doorbellId,
                    ipAddress = d.Key!.ipAddress,
                    sounds = d.Where(s => s.sound != null).Select(s => s.sound!.ToDTO()).ToList()
                })
                .FirstOrDefault();

            return result;
        }

        public DoorbellSoundLinkInfo? GetDoorbellSoundLinkByLinkId(int linkId)
        {
            try
            {
                DoorbellSoundLink entity = _doorbellSoundLinkRepository.GetDoorbellSoundLinkById(linkId);
                return entity.ToDTO();
            }
            catch
            {
                return null;
            }
        }

        public ExtendedSoundInfo? GetDoorbellSoundLinkBySoundId(int soundId)
        {
            List<DoorbellSoundLink> doorbellSoundLinks = _doorbellSoundLinkRepository.GetDoorbellSoundLinkBySoundId(soundId);

            if (doorbellSoundLinks == null || !doorbellSoundLinks.Any())
            {
                return null;
            }

            ExtendedSoundInfo? result = doorbellSoundLinks
                .GroupBy(g => g.sound)
                .Where(w => w.Key != null)
                .Select(s => new ExtendedSoundInfo()
                {
                    soundFileName = s.Key!.soundFileName,
                    soundId = s.Key!.soundId,
                    devices = s.Where(d => d.doorbell != null).Select(d => d.doorbell!.ToDTO()).ToList()
                })
                .FirstOrDefault();

            return result;
        }

        public List<ExtendedDeviceInfo> GetDoorbellSoundLinksGroupByDoorbell()
        {
            List<DoorbellSoundLink> doorbellSoundLinks = _doorbellSoundLinkRepository.GetDoorbellSoundLinks();

            if (doorbellSoundLinks == null || !doorbellSoundLinks.Any())
            {
                return new List<ExtendedDeviceInfo>();
            }

            List<ExtendedDeviceInfo> result = doorbellSoundLinks
                .GroupBy(g => g.doorbell)
                .Where(w => w.Key != null) //Filter out groups with null keys.
                .Select(d => new ExtendedDeviceInfo()
                {
                    doorbellName = d.Key!.doorbellName,
                    doorbellId = d.Key!.doorbellId,
                    ipAddress = d.Key!.ipAddress,
                    sounds = d.Where(s => s.sound != null).Select(s => s.sound!.ToDTO()).ToList()
                })
                .ToList();

            return result;

        }

        public List<ExtendedSoundInfo> GetDoorbellSoundLinksGroupBySound()
        {
            List<DoorbellSoundLink> doorbellSoundLinks = _doorbellSoundLinkRepository.GetDoorbellSoundLinks();

            if (doorbellSoundLinks == null || !doorbellSoundLinks.Any())
            {
                return new List<ExtendedSoundInfo>();
            }

            List<ExtendedSoundInfo> result = doorbellSoundLinks
                .GroupBy(g => g.sound)
                .Where(w => w.Key != null)
                .Select(s => new ExtendedSoundInfo()
                {
                    soundFileName = s.Key!.soundFileName,
                    soundId = s.Key!.soundId,
                    devices = s.Where(d => d.doorbell != null).Select(d => d.doorbell!.ToDTO()).ToList()
                })
                .ToList();

            return result;
        }

        private bool hasOverlap(DateTime? startDate, DateTime? endDate, IEnumerable<DoorbellSoundLink> items)
        {
            throw new NotImplementedException();
        }

        public Validation isValid(DoorbellSoundLinkInfo? DoorbellSoundLinkInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<int> RemoveDoorbellSoundLink(int linkId)
        {
            DoorbellSoundLink entity = _doorbellSoundLinkRepository.GetDoorbellSoundLinkById(linkId);
            return await _doorbellSoundLinkRepository.RemoveDoorbellSoundLink(entity);
        }

        public async Task<int> SaveDoorbellSoundLink(DoorbellSoundLinkInfo doorbellSoundLinkInfo)
        {
            if (doorbellSoundLinkInfo.doorbellSoundLinkId > 0)
            {
                DoorbellSoundLink entity = _doorbellSoundLinkRepository.GetDoorbellSoundLinkById(doorbellSoundLinkInfo.doorbellSoundLinkId);

                entity.doorbellId = doorbellSoundLinkInfo.doorbellId;
                entity.soundId = doorbellSoundLinkInfo.soundId;
                entity.startDate = doorbellSoundLinkInfo.startDate;
                entity.endDate = doorbellSoundLinkInfo.endDate;
                entity.isDefault = doorbellSoundLinkInfo.isDefault;
                return await _doorbellSoundLinkRepository.UpdateDoorbellSoundLink(entity);
            }
            else
            {
                return await _doorbellSoundLinkRepository.AddDoorbellSoundLink(doorbellSoundLinkInfo.ToEntity());
            }
        }
    }
}
