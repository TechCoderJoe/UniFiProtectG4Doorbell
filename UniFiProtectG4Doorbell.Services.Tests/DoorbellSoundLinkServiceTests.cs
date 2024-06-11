using System.Net;
using UniFiProtectG4Doorbell.Data.Entities;
using UniFiProtectG4Doorbell.Data.Repositories;
using UniFiProtectG4Doorbell.Data.Tests.DataContext;
using UniFiProtectG4Doorbell.Models;

namespace UniFiProtectG4Doorbell.Services.Tests
{
    public class DoorbellSoundLinkServiceTests
    {
        private readonly InMemoryDbContext _context;

        public DoorbellSoundLinkServiceTests() 
        {  
            _context = new InMemoryDbContext(); 
        }

        [Fact]
        public void ShouldGetDoorbellSoundLinks()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            int expected = 7;

            //Act
            int actual = doorbellSoundLinkService.GetDoorbellSoundLinks().Count();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldGetDoorbellSoundLinksNoResults()
        {
            //Arrange
            _context.Reset();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            int expected = 0;

            //Act
            int actual = doorbellSoundLinkService.GetDoorbellSoundLinks().Count();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeNullGetDoorbellSoundLinkByDoorbellId()
        {
            //Arrange
            _context.Reset();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            //Act
            ExtendedDeviceInfo? actual = doorbellSoundLinkService.GetDoorbellSoundLinkByDoorbellId(1);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void ShouldGetDoorbellSoundLinkByDoorbellId()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new (_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            ExtendedDeviceInfo expectedDevice = new() {
                doorbellName = "Back Door",
                ipAddress = "192.168.1.201"
            };

            int expectedSoundCount = 2;

            //Act
            ExtendedDeviceInfo? actualDevice = doorbellSoundLinkService.GetDoorbellSoundLinkByDoorbellId(2);

            //Assert
            Assert.NotNull(actualDevice);
            Assert.Equal(expectedDevice.doorbellName, actualDevice.doorbellName);
            Assert.Equal(expectedDevice.ipAddress, actualDevice.ipAddress);
            Assert.NotNull(actualDevice.sounds);
            Assert.Equal(expectedSoundCount, actualDevice.sounds.Count());

        }

        [Fact]
        public void ShouldBeNullGetDoorbellSoundLinkByLinkId()
        {
            //Arrange
            _context.Reset();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            //Act
            DoorbellSoundLinkInfo? actual = doorbellSoundLinkService.GetDoorbellSoundLinkByLinkId(1);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void ShouldGetDoorbellSoundLinkByLinkId()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            DoorbellSoundLink expected = new()
            {
                doorbellId = 2,
                soundId = 3,
                isDefault = false,
                startDate = DateTime.Now.Date.AddDays(30),
                endDate = DateTime.Now.Date.AddDays(40)
            } ;

            //Act
            DoorbellSoundLinkInfo? actual = doorbellSoundLinkService.GetDoorbellSoundLinkByLinkId(3);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.doorbellId, actual.doorbellId);
            Assert.Equal(expected.soundId, actual.soundId);
            Assert.Equal(expected.startDate, actual.startDate);
            Assert.Equal(expected.endDate, actual.endDate);
            Assert.Equal(expected.isDefault, actual.isDefault);
        }

        [Fact]
        public void ShouldBeNullGetDoorbellSoundLinkBySoundId()
        {
            //Arrange
            _context.Reset();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            //Act
            ExtendedSoundInfo? actual = doorbellSoundLinkService.GetDoorbellSoundLinkBySoundId(1);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void ShouldGetDoorbellSoundLinkBySoundId()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            ExtendedSoundInfo expectedSound = new()
            {
                soundFileName = "boo.wav",
                 soundId = 1
            };

            int expectedSoundCount = 3;

            //Act
            ExtendedSoundInfo? actualSound = doorbellSoundLinkService.GetDoorbellSoundLinkBySoundId(1);

            //Assert
            Assert.NotNull(actualSound);
            Assert.Equal(expectedSound.soundFileName, actualSound.soundFileName);
            Assert.NotNull(actualSound.devices);
            Assert.Equal(expectedSoundCount, actualSound.devices.Count());
        }

        [Fact]
        public void ShouldGetDoorbellSoundLinksGroupByDoorbellNoResults()
        {
            //Arrange
            _context.Reset();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            int expected = 0;

            //Act
            int actual = doorbellSoundLinkService.GetDoorbellSoundLinksGroupByDoorbell().Count();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldGetDoorbellSoundLinksGroupByDoorbell()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            int expected = 3;

            //Act
            int actual = doorbellSoundLinkService.GetDoorbellSoundLinksGroupByDoorbell().Count();

            //Assert
            Assert.Equal(expected, actual);
        }



        [Fact]
        public void ShouldGetDoorbellSoundLinksGroupBySoundNoResults()
        {
            //Arrange
            _context.Reset();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            int expected = 0;

            //Act
            int actual = doorbellSoundLinkService.GetDoorbellSoundLinksGroupBySound().Count();

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void ShouldGetDoorbellSoundLinksGroupBySound()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            int expected = 3;

            //Act
            int actual = doorbellSoundLinkService.GetDoorbellSoundLinksGroupBySound().Count();

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ShouldRemoveDoorbellSoundLink()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);


            //Act
            int rowsAffected = await doorbellSoundLinkService.RemoveDoorbellSoundLink(1);
            DoorbellSoundLinkInfo? actual = doorbellSoundLinkService.GetDoorbellSoundLinkByLinkId(1);

            //Assert
            Assert.Null(actual);
            Assert.True(rowsAffected == 1);

        }

        [Fact]
        public async Task ShouldAddDoorbellSoundLink()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            DoorbellSoundLinkInfo expected = new()
            {
                doorbellId = 1,
                soundId = 3,
                isDefault = false,
                startDate = DateTime.Now.Date.AddDays(200),
                endDate = DateTime.Now.Date.AddDays(205)
            };

            //Act
            int rowsAffected = await doorbellSoundLinkService.SaveDoorbellSoundLink(expected);

            DoorbellSoundLinkInfo actual = doorbellSoundLinkService.GetDoorbellSoundLinkByLinkId(8);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.doorbellId, actual.doorbellId);
            Assert.Equal(expected.soundId, actual.soundId);
            Assert.Equal(expected.startDate, actual.startDate);
            Assert.Equal(expected.endDate, actual.endDate);
            Assert.Equal(expected.isDefault, actual.isDefault);
            Assert.True(rowsAffected == 1);
        }

        [Fact]
        public async Task ShouldUpdateDoorbellSoundLink()
        {
            //Arrange
            _context.Reset();
            _context.seedDevices();
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            DoorbellSoundLinkInfo? expected = doorbellSoundLinkService.GetDoorbellSoundLinkByLinkId(1);

            //Assert on expect to remove possible null warning as the expected value shouldn't be null
            Assert.NotNull(expected);

            expected.doorbellId = 2;
            expected.soundId = 3;
            expected.isDefault = true;
            expected.startDate = DateTime.Now.Date.AddDays(200);
            expected.endDate = DateTime.Now.Date.AddDays(205);

            //Act
            int rowsAffected = await doorbellSoundLinkService.SaveDoorbellSoundLink(expected);

            DoorbellSoundLinkInfo? actual = doorbellSoundLinkService.GetDoorbellSoundLinkByLinkId(1);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.doorbellId, actual.doorbellId);
            Assert.Equal(expected.soundId, actual.soundId);
            Assert.Equal(expected.startDate, actual.startDate);
            Assert.Equal(expected.endDate, actual.endDate);
            Assert.Equal(expected.isDefault, actual.isDefault);
            Assert.True(rowsAffected == 1);
        }

        [Fact]
        public void ShouldFailValidtionValidation()
        {

            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            DoorbellSoundLinkInfo doorbellSoundLinkInfo = doorbellSoundLinkService.GetDoorbellSoundLinkByLinkId(1);

            //Act
            Validation actual = doorbellSoundLinkService.isValid(doorbellSoundLinkInfo);

            //Assert
            Assert.False(actual.isValid);
        }

        [Fact]
        public void ShouldPassValidtionValidation()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);
            DoorbellSoundLinkService doorbellSoundLinkService = new(doorbellSoundLinkRepository);

            DoorbellSoundLinkInfo doorbellSoundLinkInfo = doorbellSoundLinkService.GetDoorbellSoundLinkByLinkId(1);

            //Act
            Validation actual = doorbellSoundLinkService.isValid(doorbellSoundLinkInfo);

            //Assert
            Assert.True(actual.isValid);
        }


    }
}
