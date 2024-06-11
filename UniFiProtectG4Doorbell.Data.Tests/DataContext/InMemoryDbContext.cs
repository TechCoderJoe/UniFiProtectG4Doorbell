using Microsoft.EntityFrameworkCore;
using UniFiProtectG4Doorbell.Data.Context;

namespace UniFiProtectG4Doorbell.Data.Tests.DataContext
{
    public class InMemoryDbContext : IDisposable
    {
        public DoorbellContext DoorbellContext { get; private set; }

        
        public InMemoryDbContext()
        {
            Reset();

            if(DoorbellContext == null)
            {
                throw new NullReferenceException("DoorbellContext is not set in the InMemoryDbContext constructor.");
            }
        }

        public void Reset()
        {
            CreateDatabase();
        }

        public void seedSettings()
        {
            DoorbellContext.Settings.Add(new Entities.Settings() { 
                settingsName = Helpers.SettingsPK.unifiIpAddress,
                settingsValue = "192.168.1.1"
            });

            DoorbellContext.Settings.Add(
                new Entities.Settings() { 
                     settingsName = Helpers.SettingsPK.unifiUsername,
                    settingsValue = "unifi_username"
                });

            DoorbellContext.Settings.Add(
                new Entities.Settings()
                {
                    settingsName = Helpers.SettingsPK.unifiPassword,
                    settingsValue = "unifi_password"
                });

            DoorbellContext.Settings.Add(
                new Entities.Settings()
                {
                    settingsName = Helpers.SettingsPK.protectUsername,
                    settingsValue = "protect_username"
                });

            DoorbellContext.Settings.Add(
                new Entities.Settings()
                {
                    settingsName = Helpers.SettingsPK.protectPassword,
                    settingsValue = "protect_password"
                });

            DoorbellContext.Settings.Add(
                 new Entities.Settings() { 
                     settingsName = Helpers.SettingsPK.protectSoundUploadPath,
                     settingsValue = "protect_sound_upload_path"
                 }
                );

            DoorbellContext.Settings.Add(
                 new Entities.Settings()
                 {
                     settingsName = Helpers.SettingsPK.protectSoundProcess,
                     settingsValue = "protect_sound_process"
                 }
                );

            DoorbellContext.Settings.Add(
                new Entities.Settings()
                {
                    settingsName = Helpers.SettingsPK.firstTimeSetup,
                    settingsValue = true.ToString()
                });

            DoorbellContext.Settings.Add(
                new Entities.Settings()
                {
                    settingsName = Helpers.SettingsPK.doorbellCustomSoundFileName,
                    settingsValue = "sound.wav"
                });

            DoorbellContext.Settings.Add(
                new Entities.Settings()
                {
                     settingsName = Helpers.SettingsPK.currentSetupStep,
                      settingsValue = "1"
                });

            DoorbellContext.SaveChanges();

        }

        public void seedDevices()
        {
            DoorbellContext.Devices.Add(
                new Entities.DoorbellItem()
                {
                    doorbellName = "Front Door",
                    ipAddress = "192.168.1.200"
                });

            DoorbellContext.Devices.Add(
                new Entities.DoorbellItem()
                {
                    doorbellName = "Back Door",
                    ipAddress = "192.168.1.201"
                });

            
            DoorbellContext.SaveChanges();
        }

        public void seedSoundLink()
        {
            Entities.DoorbellItem device1 = new()
            {
                doorbellName = "Front Door",
                ipAddress = "192.168.1.200"
            };

            DoorbellContext.Devices.Add(device1);

            Entities.DoorbellItem device2 = new()
            {
                doorbellName = "Back Door",
                ipAddress = "192.168.1.201"
            };



            DoorbellContext.Devices.Add(device2);

            Entities.DoorbellItem device3 = new ()
                {
                    doorbellName = "Side Door",
                    ipAddress = "192.168.1.150"
                };

            DoorbellContext.Devices.Add(device3);


            Entities.SoundItem sound1 = new()
            { 
             soundFileName = "boo.wav"
            };

            DoorbellContext.Sounds.Add(sound1);

            Entities.SoundItem sound2 = new()
            {
                soundFileName = "hohoho.wav"
            };

            DoorbellContext.Sounds.Add(sound2);

            Entities.SoundItem sound3 = new()
            {
                soundFileName = "scary.wav"
            };

            DoorbellContext.Sounds.Add(sound3);

            DoorbellContext.DeviceSoundLinks.Add(new Entities.DoorbellSoundLink()
            {
                doorbell = device1,
                sound = sound1,
                isDefault = false,
                startDate = DateTime.Now.Date,
                endDate = DateTime.Now.Date.AddDays(10)
            }) ;

            DoorbellContext.DeviceSoundLinks.Add(new Entities.DoorbellSoundLink()
            {
                doorbell = device2,
                sound = sound2,
                isDefault = false,
                startDate = DateTime.Now.Date.AddDays(15),
                endDate = DateTime.Now.Date.AddDays(20)
            });

            DoorbellContext.DeviceSoundLinks.Add(new Entities.DoorbellSoundLink()
            {
                doorbell = device2,
                sound = sound3,
                isDefault = false,
                startDate = DateTime.Now.Date.AddDays(30),
                endDate = DateTime.Now.Date.AddDays(40)
            });

            DoorbellContext.DeviceSoundLinks.Add(new Entities.DoorbellSoundLink()
            {
                doorbell = device3,
                sound = sound1,
                isDefault = false,
                startDate = DateTime.Now.Date.AddDays(5),
                endDate = DateTime.Now.Date.AddDays(10)
            });


            DoorbellContext.DeviceSoundLinks.Add(new Entities.DoorbellSoundLink()
            {
                doorbell = device3,
                sound = sound1,
                isDefault = true,
                startDate = null,
                endDate = null
            });

            DoorbellContext.DeviceSoundLinks.Add(new Entities.DoorbellSoundLink()
            {
                doorbell = device3,
                sound = sound3,
                isDefault = false,
                startDate = DateTime.Now.Date.AddDays(30),
                endDate = DateTime.Now.Date.AddDays(40)
            });

            DoorbellContext.DeviceSoundLinks.Add(new Entities.DoorbellSoundLink()
            {
                doorbell = device3,
                sound = sound3,
                isDefault = false,
                startDate = DateTime.Now.Date.AddDays(50),
                endDate = DateTime.Now.Date.AddDays(60)
            });


            DoorbellContext.SaveChanges();
        }

        public void seedSounds()
        {
            DoorbellContext.Sounds.Add(new Entities.SoundItem()
            {
                soundFileName = "sound1.wav"
            });

            DoorbellContext.Sounds.Add(new Entities.SoundItem()
            {
                soundFileName = "sound2.wav"
            });

            DoorbellContext.SaveChanges();
        }
        
        public void Dispose()
        {
            DoorbellContext.Dispose();
        }


        private void CreateDatabase()
        {
            var options = new DbContextOptionsBuilder<DoorbellContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            DoorbellContext = new DoorbellContext(options);
            DoorbellContext.Database.EnsureDeleted();
            DoorbellContext.Database.EnsureCreated();
        }

        
    }
}
