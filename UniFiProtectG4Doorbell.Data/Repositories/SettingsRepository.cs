using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UniFiProtectG4Doorbell.Data.Context;
using UniFiProtectG4Doorbell.Data.Entities;
using UniFiProtectG4Doorbell.Data.Interfaces;

namespace UniFiProtectG4Doorbell.Data.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private DoorbellContext _context;

        public SettingsRepository(DoorbellContext context) {
            _context = context;
        }

        public string GetProtectUsername()
        {
            return GetSettingValue(Helpers.SettingsPK.protectUsername);
        }

        public string GetProtectPassword()
        {
            return GetSettingValue(Helpers.SettingsPK.protectPassword);
        }

        public string GetProtectSoundUploadPath() {
            return GetSettingValue(Helpers.SettingsPK.protectSoundUploadPath);
        }

        public string GetProtectSoundProcess()
        {
            return GetSettingValue(Helpers.SettingsPK.protectSoundProcess);
        }

        public string GetUniFiIpAddress()
        {
            return GetSettingValue(Helpers.SettingsPK.unifiIpAddress);
        }

        public string GetUniFiUsername()
        {
            return GetSettingValue(Helpers.SettingsPK.unifiUsername);
        }

        public string GetUniFiPassword()
        {
            return GetSettingValue(Helpers.SettingsPK.unifiPassword);
        }

        public string GetDoorbellCustomSoundFileName()
        {
            return GetSettingValue(Helpers.SettingsPK.doorbellCustomSoundFileName);
        }

        public bool IsFirstTimeSetup()
        {
            return bool.Parse(GetSettingValue(Helpers.SettingsPK.firstTimeSetup));
        }

        public int GetCurrentSetupStep()
        {
            return int.Parse(GetSettingValue(Helpers.SettingsPK.currentSetupStep));
        }

        public async Task<int> SaveProtectInfo(Models.ProtectInfo protectInfo)
        {
            Dictionary<string, string> settingsInfo = new()
            {
                { Helpers.SettingsPK.protectUsername, protectInfo.username },
                { Helpers.SettingsPK.protectPassword, protectInfo.password },
                { Helpers.SettingsPK.doorbellCustomSoundFileName, protectInfo.soundFileName },
                { Helpers.SettingsPK.protectSoundProcess, protectInfo.soundProcess },
                { Helpers.SettingsPK.protectSoundUploadPath, protectInfo.soundUploadPath }
            };

            return await SaveSettings(settingsInfo);
        }

        public async Task<int> SaveUniFiInfo(string ipaddress, string username, string password)
        {
            Dictionary<string, string> settingsInfo = new()
            {
                { Helpers.SettingsPK.unifiIpAddress, ipaddress },
                { Helpers.SettingsPK.unifiUsername, username },
                { Helpers.SettingsPK.unifiPassword, password }
            };

            return await SaveSettings(settingsInfo);
        }

        public async Task<int> SaveFirstTimeSetup(bool value)
        {
            Dictionary<string, string> settingsInfo = new()
            {
                { Helpers.SettingsPK.firstTimeSetup, value.ToString() }
            };

            return await SaveSettings(settingsInfo);
        }

        public async Task<int> SaveCurrentSetupStep(int value)
        {
            Dictionary<string, string> settingsInfo = new()
            {
                { Helpers.SettingsPK.currentSetupStep, value.ToString() }
            };

            return await SaveSettings(settingsInfo);
        }

        private string GetSettingValue(string key)
        {
            string? value = _context.Settings.Where(w => w.settingsName == key).Select(s => s.settingsValue).FirstOrDefault();

            if (value == null)
            {
                throw new NullReferenceException($"{key} is null");
            }

            return value;
        }

        private async Task<int> SaveSettings(Dictionary<string, string> settingsInfo)
        {
            foreach (KeyValuePair<string,string> item in settingsInfo)
            {
                Entities.Settings? saveItem = _context.Settings.Where(w => w.settingsName == item.Key).FirstOrDefault();

                if (saveItem != null)
                {
                    saveItem.settingsValue = item.Value;
                }
                else
                {
                    saveItem = new Settings
                    {
                        settingsName = item.Key,
                        settingsValue = item.Value
                    };

                    _context.Settings.Add(saveItem);
                }
            }

            int rowsAffected = await _context.SaveChangesAsync();
            return rowsAffected;
        }
    }
}
