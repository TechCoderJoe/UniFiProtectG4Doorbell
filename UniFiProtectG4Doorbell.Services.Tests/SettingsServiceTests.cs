using UniFiProtectG4Doorbell.Data.Repositories;
using UniFiProtectG4Doorbell.Data.Tests.DataContext;

namespace UniFiProtectG4Doorbell.Services.Tests
{
    public class SettingsServiceTests
    {
        private readonly InMemoryDbContext _context;

        public SettingsServiceTests ()
        {
            _context = new InMemoryDbContext();
        }

        [Fact]
        public void ShouldGetProtectInfo()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            Models.ProtectInfo expected = new()
            {
                username = "protect_username",
                password = "protect_password",
                soundFileName = "sound.wav",
                soundProcess = "protect_sound_process",
                soundUploadPath = "protect_sound_upload_path"
            };

            //Act
            Models.ProtectInfo actual = settingsService.GetProtectInfo();

            //Assert
            Assert.Equal(expected.username, actual.username);
            Assert.Equal(expected.password, actual.password);
            Assert.Equal(expected.soundFileName, actual.soundFileName);
            Assert.Equal(expected.soundProcess, actual.soundProcess);
            Assert.Equal(expected.soundUploadPath, actual.soundUploadPath);
        }

        [Fact]
        public async Task ShouldAddProtectInfo()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            Models.ProtectInfo expected = new()
            {
                username = "protectAdd_username",
                password = "protectAdd_password",
                soundFileName = "soundAdd.wav",
                soundProcess = "protectAdd_process",
                soundUploadPath = "protectAdd_uploadPath"
            };

            //Act
            int rowsAffected = await settingsService.SetProtectInfo(expected);

            Models.ProtectInfo actual = settingsService.GetProtectInfo();

            //Assert
            Assert.Equal(expected.username, actual.username);
            Assert.Equal(expected.password, actual.password);
            Assert.Equal(expected.soundFileName, actual.soundFileName);
            Assert.Equal(expected.soundUploadPath, actual.soundUploadPath);
            Assert.Equal(expected.soundProcess, actual.soundProcess);
            Assert.True(rowsAffected == 5);
        }

        [Fact]
        public async Task ShouldUpdateProtectInfo()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            Models.ProtectInfo expected = new()
            {
                username = "protectUpdate_username",
                password = "protectUpdate_password",
                soundFileName = "soundUpdate.wav",
                soundProcess = "protectUpdate_process",
                soundUploadPath = "protectUpdate_uploadPath"
            };

            //Act
            int rowsAffected = await settingsService.SetProtectInfo(expected);

            Models.ProtectInfo actual = settingsService.GetProtectInfo();

            //Assert
            Assert.Equal(expected.username, actual.username);
            Assert.Equal(expected.password, actual.password);
            Assert.Equal(expected.soundFileName, actual.soundFileName);
            Assert.Equal(expected.soundUploadPath, actual.soundUploadPath);
            Assert.Equal(expected.soundProcess, actual.soundProcess);
            Assert.True(rowsAffected == 5);
        }

        [Fact]
        public void ShouldGetUniFiInfo()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            Models.UniFiInfo expected = new()
            {
                ipAddress = "192.168.1.1",
                username = "unifi_username",
                password = "unifi_password"
            };

            //Act
            Models.UniFiInfo actual = settingsService.GetUniFiInfo();

            //Assert
            Assert.Equal(expected.ipAddress, actual.ipAddress);
            Assert.Equal(expected.username, actual.username);
            Assert.Equal(expected.password, actual.password);
        }

        [Fact]
        public async Task ShouldAddUniFiInfo()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            Models.UniFiInfo expected = new()
            {
                ipAddress = "192.168.0.1",
                username = "UniFiAdd_username",
                password = "UniFiAdd_password"
            };

            //Act
            int rowsAffected = await settingsService.SetUniFiInfo(expected);

            Models.UniFiInfo actual = settingsService.GetUniFiInfo();

            //Assert
            Assert.Equal(expected.ipAddress, actual.ipAddress);
            Assert.Equal(expected.username, actual.username);
            Assert.Equal(expected.password, actual.password);
            Assert.True(rowsAffected == 3);
        }

        [Fact]
        public async Task ShouldUpdateUniFiInfo()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            Models.UniFiInfo expected = new()
            {
                ipAddress = "172.168.1.1",
                username = "UniFiUpdate_username",
                password = "UniFiUpdate_password"
            };

            //Act
            int rowsAffected = await settingsService.SetUniFiInfo(expected);

            Models.UniFiInfo actual = settingsService.GetUniFiInfo();

            //Assert
            Assert.Equal(expected.ipAddress, actual.ipAddress);
            Assert.Equal(expected.username, actual.username);
            Assert.Equal(expected.password, actual.password);
            Assert.True(rowsAffected == 3);
        }

        [Fact]
        public void ShouldGetIsFirstTimeSetup()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            bool expected = true;

            //Act
            bool actual = settingsService.GetIsFirstTimeSetup();

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task ShouldAddIsFirstTimeSetup()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            bool expected = true;

            //Act
            int rowsAffected = await settingsService.SetIsFirstTimeSetup(expected);

            bool actual = settingsService.GetIsFirstTimeSetup();

            //Assert
            Assert.Equal(expected, actual);
            Assert.True(rowsAffected == 1);
        }

        [Fact]
        public async Task ShouldUpdateIsFirstTimeSetup()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            bool expected = false;

            //Act
            int rowsAffected = await settingsService.SetIsFirstTimeSetup(expected);

            bool actual = settingsService.GetIsFirstTimeSetup();

            //Assert
            Assert.Equal(expected, actual);
            Assert.True(rowsAffected == 1);

        }

        [Fact]
        public void ShouldGetDoorbellCustomSoundFileName()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            string expected = "sound.wav";

            //Act
            string actual = settingsService.GetDoorbellCustomSoundFileName();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldGetCurrentSetupStep()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            int expected = 1;

            //Act
            int actual = settingsService.GetCurrentSetupStep();

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task ShouldAddCurrentSetupStep()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            int expected = 4;

            //Act
            int rowsAffected = await settingsService.SetCurrentSetupStep(expected);

            int actual = settingsService.GetCurrentSetupStep();

            //Assert
            Assert.Equal(expected, actual);
            Assert.True(rowsAffected == 1);
        }

        [Fact]
        public async Task ShouldUpdateCurrentSetupStep()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SettingsService settingsService = new(settingsRepository);

            int expected = 5;

            //Act
            int rowsAffected = await settingsService.SetCurrentSetupStep(expected);

            int actual = settingsService.GetCurrentSetupStep();

            //Assert
            Assert.Equal(expected, actual);
            Assert.True(rowsAffected == 1);

        }
    }
}
