
using UniFiProtectG4Doorbell.Data.Repositories;
using UniFiProtectG4Doorbell.Data.Tests.DataContext;


namespace UniFiProtectG4Doorbell.Data.Tests
{
    public class DeviceRepositoryTests
    {
        private readonly InMemoryDbContext _context;

        public DeviceRepositoryTests() 
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

            int expected = 2;

            //Act
            int actual = deviceRepository.GetDevices().Count;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowNullExceptionOnGetDeviceById()
        {
            //Arrange
            _context.Reset();
            DeviceRepository deviceRepository = new(_context.DoorbellContext);

            Action actionCall = () => deviceRepository.GetDeviceItemById(1);

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
        }

        [Fact]
        public void ShouldGetDeviceById()
        {
            //Arrange
            _context.Reset();
            _context.seedDevices();
            DeviceRepository deviceRepository = new(_context.DoorbellContext);

            Entities.DoorbellItem expected = new ()
            {
                doorbellId = 1,
                doorbellName = "Front Door",
                ipAddress = "192.168.1.200"
            };

            //Act
            Entities.DoorbellItem actual = deviceRepository.GetDeviceItemById(1);

            //Assert
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

            Entities.DoorbellItem expected = new()
            {
                doorbellName = "Side Door",
                ipAddress = "192.168.1.215"
            };

            //Act
            int rowsAffected = await deviceRepository.AddDevice(expected);

            Entities.DoorbellItem actual = deviceRepository.GetDeviceItemById(1);

            //Assert
            Assert.Equal(expected.doorbellId, actual.doorbellId);
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

            Entities.DoorbellItem expected = deviceRepository.GetDeviceItemById(1);
            expected.doorbellName = "updatedName";
            expected.ipAddress = "192.168.1.105";

            //Act
            int rowsAffected = await deviceRepository.UpdateDevice(expected);

            Entities.DoorbellItem actual = deviceRepository.GetDeviceItemById(1);

            //Assert
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

            Entities.DoorbellItem expected = deviceRepository.GetDeviceItemById(1);

            //Act
            int rowsAffected = await deviceRepository.RemoveDevice(expected);

            Action actionCall = () => deviceRepository.GetDeviceItemById(1);

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
            Assert.True(rowsAffected == 1);

        }

        [Fact]
        public async Task ShouldRemoveDeviceWithSoundLink()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DeviceRepository deviceRepository = new(_context.DoorbellContext);

            Entities.DoorbellItem expected = deviceRepository.GetDeviceItemById(1);

            //Act
            int rowsAffected = await deviceRepository.RemoveDevice(expected);

            Action actionCall = () => deviceRepository.GetDeviceItemById(1);

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
            Assert.True(rowsAffected == 2);

        }
    }
}
