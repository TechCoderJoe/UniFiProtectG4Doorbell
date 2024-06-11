
using UniFiProtectG4Doorbell.Data.Repositories;
using UniFiProtectG4Doorbell.Data.Tests.DataContext;


namespace UniFiProtectG4Doorbell.Data.Tests
{
    public class SettingsRepositoryTests
    {
        private readonly InMemoryDbContext _context;

        public SettingsRepositoryTests() 
        {  
            _context = new InMemoryDbContext(); 
        }

        [Fact]
        public void ShouldGetProtectUsername()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new (_context.DoorbellContext);
            
            string expected = "protect_username";

            //Act
            string actual = settingsRepository.GetProtectUsername();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowNullExceptionProtectUsername()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new (_context.DoorbellContext);

            Action actionCall = () => settingsRepository.GetProtectUsername();

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
        }

        [Fact]
        public void ShouldGetProtectPassword()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new (_context.DoorbellContext);

            string expected = "protect_password";

            //Act
            string actual = settingsRepository.GetProtectPassword();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowNullExceptionProtectPassword()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new (_context.DoorbellContext);

            Action actionCall = () => settingsRepository.GetProtectPassword();

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
        }

        [Fact]
        public void ShouldGetProtectSoundUploadPath()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            string expected = "protect_sound_upload_path";

            //Act
            string actual = settingsRepository.GetProtectSoundUploadPath();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowNullExceptionProtectSoundUploadPath()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            Action actionCall = () => settingsRepository.GetProtectSoundUploadPath();

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
        }


        [Fact]
        public void ShouldGetProtectSoundProcess()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            string expected = "protect_sound_process";

            //Act
            string actual = settingsRepository.GetProtectSoundProcess();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowNullExceptionProtectSoundProcess()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            Action actionCall = () => settingsRepository.GetProtectSoundProcess();

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
        }

        [Fact]
        public async Task ShouldAddProtectInfo()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new (_context.DoorbellContext);

            
            Models.ProtectInfo expected = new()
            {
                password = "ProtectAddPassword",
                username = "ProtectAddUsername",
                soundFileName = "ProtectAddFileName",
                soundProcess = "ProtectAddSoundProcess",
                soundUploadPath = "ProtectAddSoundUploadPath"
            };


            //Act
            int rowsAffected = await settingsRepository.SaveProtectInfo(expected);

            string actualUsername = settingsRepository.GetProtectUsername();
            string actualPassword = settingsRepository.GetProtectPassword();
            string actualSoundFileName = settingsRepository.GetDoorbellCustomSoundFileName();
            string actualSoundProcess = settingsRepository.GetProtectSoundProcess();
            string actualSoundUploadPath = settingsRepository.GetProtectSoundUploadPath();

            //Assert
            Assert.Equal(expected.username, actualUsername);
            Assert.Equal(expected.password, actualPassword);
            Assert.Equal(expected.soundFileName, actualSoundFileName);
            Assert.Equal(expected.soundUploadPath, actualSoundUploadPath);
            Assert.Equal(expected.soundProcess, actualSoundProcess);
            Assert.True(rowsAffected == 5);
        }


        [Fact]
        public async Task ShouldUpdateProtectInfo()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new (_context.DoorbellContext);

            Models.ProtectInfo expected = new()
            {
                password = "ProtectUpdatePassword",
                username = "ProtectUpdateUsername",
                soundFileName = "ProtectUpdatedFileName",
                soundProcess = "ProtectUpdatedSoundProcess",
                soundUploadPath = "ProtectUpdatedSoundUploadPath"
            };

            //Act
            int rowsAffected = await settingsRepository.SaveProtectInfo(expected);

            string actualUsername = settingsRepository.GetProtectUsername();
            string actualPassword = settingsRepository.GetProtectPassword();
            string actualSoundFileName = settingsRepository.GetDoorbellCustomSoundFileName();
            string actualSoundProcess = settingsRepository.GetProtectSoundProcess();
            string actualSoundUploadPath = settingsRepository.GetProtectSoundUploadPath();

            //Assert
            Assert.Equal(expected.username, actualUsername);
            Assert.Equal(expected.password, actualPassword);
            Assert.Equal(expected.soundFileName, actualSoundFileName);
            Assert.Equal(expected.soundUploadPath, actualSoundUploadPath);
            Assert.Equal(expected.soundProcess, actualSoundProcess);
            Assert.True(rowsAffected == 5);

        }

        [Fact]
        public void ShouldGetUniFiIpAddress()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            string expected = "192.168.1.1";

            //Act
            string actual = settingsRepository.GetUniFiIpAddress();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowNullExceptionUniFiIpAddress()
        {

            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            Action actionCall = () => settingsRepository.GetUniFiIpAddress();

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
        }

        [Fact]
        public void ShouldGetUniFiUsername()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            string expected = "unifi_username";

            //Act
            string actual = settingsRepository.GetUniFiUsername();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowNullExceptionUniFiUsername()
        {

            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            Action actionCall = () => settingsRepository.GetUniFiUsername();

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
        }

        [Fact]
        public void ShouldGetUniFiPassword()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            string expected = "unifi_password";

            //Act
            string actual = settingsRepository.GetUniFiPassword();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowNullExceptionUniFiPassword()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            Action actionCall = () => settingsRepository.GetUniFiPassword();

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
        }

        [Fact]
        public async Task ShouldAddUniFiInfo()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            string expectedIpAddress = "192.168.0.1";
            string expectedUsername = "UniFiAddUsername";
            string expectedPassword = "UniFiAddPassword";

            //Act
            int rowsAffected = await settingsRepository.SaveUniFiInfo(expectedIpAddress,expectedUsername, expectedPassword);

            string actualIpAddress = settingsRepository.GetUniFiIpAddress();
            string actualUsername = settingsRepository.GetUniFiUsername();
            string actualPassword = settingsRepository.GetUniFiPassword();

            //Assert
            Assert.Equal(expectedIpAddress, actualIpAddress);
            Assert.Equal(expectedUsername, actualUsername);
            Assert.Equal(expectedPassword, actualPassword);
            Assert.True(rowsAffected == 3);
        }

        [Fact]
        public async Task ShouldUpdateUniFiInfo()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            string expectedIpAddress = "172.168.0.1";
            string expectedUsername = "UniFiUpdateUsername";
            string expectedPassword = "UniFiUpdatePassword";

            //Act
            int rowsAffected = await settingsRepository.SaveUniFiInfo(expectedIpAddress, expectedUsername, expectedPassword);
            
            string actualIpAddress = settingsRepository.GetUniFiIpAddress();
            string actualUsername = settingsRepository.GetUniFiUsername();
            string actualPassword = settingsRepository.GetUniFiPassword();

            //Assert
            Assert.Equal(expectedIpAddress, actualIpAddress);
            Assert.Equal(expectedUsername, actualUsername);
            Assert.Equal(expectedPassword, actualPassword);
            Assert.True(rowsAffected == 3);

        }

        [Fact]
        public void ShouldGetDoorbellCustomSoundFileName()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            string expected = "sound.wav";

            //Act
            string actual = settingsRepository.GetDoorbellCustomSoundFileName();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowNullExceptionDoorbellCustomSoundFileName()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            Action actionCall = () => settingsRepository.GetDoorbellCustomSoundFileName();

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
        }

        [Fact]
        public void ShouldGetIsFirstTimeSetup()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            bool expected = true;

            //Act
            bool actual = settingsRepository.IsFirstTimeSetup();

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task ShouldAddIsFirstTimeSetup()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            bool expected = true;

            //Act
            int rowsAffected = await settingsRepository.SaveFirstTimeSetup(expected);

            bool actual = settingsRepository.IsFirstTimeSetup();

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

            bool expected = false;

            //Act
            int rowsAffected = await settingsRepository.SaveFirstTimeSetup(expected);

            bool actual = settingsRepository.IsFirstTimeSetup();

            //Assert
            Assert.Equal(expected, actual);
            Assert.True(rowsAffected == 1);

        }

        [Fact]
        public void ShouldGetCurrentSetupStep()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            int expected = 1;

            //Act
            int actual = settingsRepository.GetCurrentSetupStep();

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task ShouldAddCurrentSetupStep()
        {
            //Arrange
            _context.Reset();
            SettingsRepository settingsRepository = new(_context.DoorbellContext);

            int expected = 2;

            //Act
            int rowsAffected = await settingsRepository.SaveCurrentSetupStep(expected);

            int actual = settingsRepository.GetCurrentSetupStep();

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

            int expected = 4;

            //Act
            int rowsAffected = await settingsRepository.SaveCurrentSetupStep(expected);

            int actual = settingsRepository.GetCurrentSetupStep();

            //Assert
            Assert.Equal(expected, actual);
            Assert.True(rowsAffected == 1);

        }
    }
}
