using System.Net;
using UniFiProtectG4Doorbell.Data.Repositories;
using UniFiProtectG4Doorbell.Data.Tests.DataContext;
using UniFiProtectG4Doorbell.Models;

namespace UniFiProtectG4Doorbell.Services.Tests
{
    public class DeviceServiceTests
    {
        private readonly InMemoryDbContext _context;

        public DeviceServiceTests() 
        {  
            _context = new InMemoryDbContext(); 
        }

        [Fact]
        public void ShouldGetDeviceList()
        {
            //Arrange
            _context.Reset();
            _context.seedDevices();
            DeviceRepository deviceRepository = new (_context.DoorbellContext);
            SettingsRepository settingsRepository = new (_context.DoorbellContext);
            DeviceService deviceService = new(deviceRepository, settingsRepository);

            int expected = 2;

            //Act
            int actual = deviceService.GetDevices().Count;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldGetDeviceListNoResults()
        {
            //Arrange
            _context.Reset();
            DeviceRepository deviceRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            DeviceService deviceService = new(deviceRepository, settingsRepository);

            int expected = 0;

            //Act
            int actual = deviceService.GetDevices().Count;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeNullGetDeviceById()
        {
            //Arrange
            _context.Reset();
            DeviceRepository deviceRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            DeviceService deviceService = new(deviceRepository, settingsRepository);

            //Act
            DeviceInfo? actual = deviceService.GetDevice(1);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void ShouldGetDeviceById()
        {
            //Arrange
            _context.Reset();
            _context.seedDevices();
            DeviceRepository deviceRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            DeviceService deviceService = new(deviceRepository, settingsRepository);

            DeviceInfo expected = new ()
            {
                doorbellId = 1,
                doorbellName = "Front Door",
                ipAddress = "192.168.1.200"
            };

            //Act
            DeviceInfo? actual = deviceService.GetDevice(1);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.doorbellId, actual.doorbellId);
            Assert.Equal(expected.doorbellName, actual.doorbellName);
            Assert.Equal(expected.ipAddress, actual.ipAddress);
        }

        [Fact]
        public async Task ShouldAddDevice()
        {
            //Arrange
            _context.Reset();
            DeviceRepository deviceRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            DeviceService deviceService = new(deviceRepository, settingsRepository);

            DeviceInfo expected = new()
            {
                doorbellName = "Side Door",
                ipAddress = "192.168.1.215"
            };

            //Act
            int rowsAffected = await deviceService.SaveDevice(expected);

            DeviceInfo? actual = deviceService.GetDevice(1);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.doorbellName, actual.doorbellName);
            Assert.Equal(expected.ipAddress, actual.ipAddress);
            Assert.True(rowsAffected == 1);
        }

        [Fact]
        public async Task ShouldUpdateDevice()
        {
            //Arrange
            _context.Reset();
            _context.seedDevices();
            DeviceRepository deviceRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            DeviceService deviceService = new(deviceRepository, settingsRepository);

            DeviceInfo? expected = deviceService.GetDevice(1);

            //Assert on expect to remove possible null warning as the expected value shouldn't be null
            Assert.NotNull(expected);

            expected.doorbellName = "updatedName";
            expected.ipAddress = "192.168.1.105";

            //Act
            int rowsAffected = await deviceService.SaveDevice(expected);

            DeviceInfo? actual = deviceService.GetDevice(1);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.doorbellId, actual.doorbellId);
            Assert.Equal(expected.doorbellName, actual.doorbellName);
            Assert.Equal(expected.ipAddress, actual.ipAddress);
            Assert.True(rowsAffected == 1);
        }

        [Fact]
        public async Task ShouldRemoveDevice()
        {
            //Arrange
            _context.Reset();
            _context.seedDevices();
            DeviceRepository deviceRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            DeviceService deviceService = new(deviceRepository, settingsRepository);


            //Act
            int rowsAffected = await deviceService.RemoveDevice(1);
            DeviceInfo? actual = deviceService.GetDevice(1);

            //Assert
            Assert.Null(actual);
            Assert.True(rowsAffected == 1);

        }

        [Fact]
        public async Task ShouldRemoveDeviceWithSoundLink()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DeviceRepository deviceRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            DeviceService deviceService = new(deviceRepository, settingsRepository);


            //Act
            int rowsAffected = await deviceService.RemoveDevice(1);
            DeviceInfo? actual = deviceService.GetDevice(1);

            //Assert
            Assert.Null(actual);
            Assert.True(rowsAffected == 2);

        }

        [Theory]
        [InlineData("2001:DB8:00:0:0:0:1")]//Missing colons - missing colon between the third and fourth group
        [InlineData("2001::DB8:0:0:0:0:1")]//Extra colons
        [InlineData("1234:5678")]//Invalid number of groups - 2 groups
        [InlineData("2001:DB8:0:0:0:0:0:1:2")]//Invalid number of groups - 9 groups
        [InlineData("abcde:fghij:klmno:pqrst:uvwxy:zabc:defg:1234")] //Invalid characters - alphabetic characters except A-F
        [InlineData("G001:DB8:0:0:0:0:0:1")] //Out-of-range - invalid hexadecimal digit 'G'
        [InlineData("192.168.1")] //Incorrect number of octets - missing the fourth octet
        [InlineData("1.2.3.4.5")] //Incorrect number of octets - too many octet
        [InlineData("256.168.1.1")]//Values outside the valid range (0-255)
        [InlineData("192.168.1.256")]//Values outside the valid range (0-255)
        [InlineData("192.168.A.1")]//Non-numeric characters
        [InlineData("3")]
        public void ShouldFailValidtionOnInvalidIpAddress(string ipAddress)
        {
            //Arrange
            DeviceRepository deviceRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            DeviceService deviceService = new(deviceRepository, settingsRepository);

            DeviceInfo device = new DeviceInfo()
            {
                doorbellName = "Test",
                ipAddress = ipAddress
            };

            Validation expected = new (){ 
             isValid = false,
                key = "ipAddress",
                message = "Invalid IP Address"
            };

            //Act
            Validation actual = deviceService.isValid(device);

            //Assert
            Assert.Equal(expected.message, actual.message);
            Assert.Equal(expected.key, actual.key);
            Assert.Equal(expected.isValid, actual.isValid);  
        }

        [Fact]
        public void ShouldFailValidtionOnIpExisting()
        {
            //Arrange
            _context.Reset();
            _context.seedDevices();

            DeviceRepository deviceRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            DeviceService deviceService = new(deviceRepository, settingsRepository);

            DeviceInfo deviceInfo = new()
                {
                doorbellName = "Front Door 98",
                    ipAddress = "192.168.1.200"
                };

            Validation expected = new()
            {
                isValid = false,
                key = "ipAddress",
                message = $"IP Address of {deviceInfo.ipAddress} already exists"
            };

            //Act
            Validation actual = deviceService.isValid(deviceInfo);

            //Assert
            Assert.Equal(expected.message, actual.message);
            Assert.Equal(expected.key, actual.key);
            Assert.Equal(expected.isValid, actual.isValid);

        }

        [Fact]
        public void ShouldFailValidtionOnNameExisting()
        {
            //Arrange
            _context.Reset();
            _context.seedDevices();

            DeviceRepository deviceRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            DeviceService deviceService = new(deviceRepository, settingsRepository);

            DeviceInfo deviceInfo = new()
            {
                doorbellName = "Front Door",
                ipAddress = "192.168.1.230"
            };

            Validation expected = new()
            {
                isValid = false,
                key = "doorbellName",
                message = $"Device Name of {deviceInfo.doorbellName} already exists"
            };

            //Act
            Validation actual = deviceService.isValid(deviceInfo);

            //Assert
            Assert.Equal(expected.message, actual.message);
            Assert.Equal(expected.key, actual.key);
            Assert.Equal(expected.isValid, actual.isValid);

        }

        [Fact]
        public void ShouldFailValidtionOnSshConnection()
        {
            //Arrange
            _context.Reset();
            _context.seedSettings();
            DeviceRepository deviceRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            DeviceService deviceService = new(deviceRepository, settingsRepository);

            DeviceInfo deviceInfo = new()
            {
                doorbellName = "Front Door",
                ipAddress = "192.168.1.230"
            };

            Validation expected = new()
            {
                isValid = false,
                key = "ipAddress",
                message = $"Failed to connect to {deviceInfo.ipAddress}. Please check the IP address and the Protect username/password in the settings"
            };

            //Act
            Validation actual = deviceService.isValid(deviceInfo);

            //Assert
            Assert.Equal(expected.message, actual.message);
            Assert.Equal(expected.key, actual.key);
            Assert.Equal(expected.isValid, actual.isValid);

        }
    }
}
