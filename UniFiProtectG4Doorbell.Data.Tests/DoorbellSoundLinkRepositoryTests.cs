
using UniFiProtectG4Doorbell.Data.Repositories;
using UniFiProtectG4Doorbell.Data.Tests.DataContext;
using Xunit.Sdk;


namespace UniFiProtectG4Doorbell.Data.Tests
{
    public class DoorbellSoundLinkRepositoryTests
    {
        private readonly InMemoryDbContext _context;

        public DoorbellSoundLinkRepositoryTests() 
        {  
            _context = new InMemoryDbContext(); 
        }

        [Fact]
        public void ShouldGetDoorbellSoundLinkList()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new (_context.DoorbellContext);

            int expected = 7;

            //Act
            int actual = doorbellSoundLinkRepository.GetDoorbellSoundLinks().Count;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldGetDoorbellSoundLinkListByDoorbellId()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();

            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);

            int expected = 4;

            //Act
            int actual = doorbellSoundLinkRepository.GetDoorbellSoundLinkByDoorbellId(3).Count;

            //Assert
            Assert.Equal(expected, actual);
        }



        [Fact]
        public void ShouldGetDoorbellSoundLinkListBySoundId()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();

            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);

            int expected = 3;

            //Act
            int actual = doorbellSoundLinkRepository.GetDoorbellSoundLinkBySoundId(3).Count;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowNullExceptionOnGetDoorbellSoundLinkById()
        {
            //Arrange
            _context.Reset();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);

            Action actionCall = () => doorbellSoundLinkRepository.GetDoorbellSoundLinkById(1);

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
        }

        [Fact]
        public void ShouldGetDoorbellSoundLinkById()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);

            Entities.DoorbellSoundLink expected = new()
            {
                doorbellId = 1,
                soundId = 1,
                doorbellSoundLinkId = 1,
                isDefault = false,
                startDate = DateTime.Now.Date,
                endDate = DateTime.Now.Date.AddDays(10)

            };

            //Act
            Entities.DoorbellSoundLink actual = doorbellSoundLinkRepository.GetDoorbellSoundLinkById(1);

            //Assert
            Assert.Equal(expected.doorbellId, actual.doorbellId);
            Assert.Equal(expected.doorbellSoundLinkId, actual.doorbellSoundLinkId);
            Assert.Equal(expected.isDefault, actual.isDefault);
            Assert.Equal(expected.soundId, actual.soundId);
            Assert.Equal(expected.startDate, actual.startDate);
            Assert.Equal(expected.endDate, actual.endDate);
        }

        [Fact]
        public async Task ShouldAddDoorbellSoundLink()
        {
            //Arrange
            _context.Reset();
            _context.seedDevices();
            _context.seedSounds();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);

            Entities.DoorbellSoundLink expected = new()
            {
                doorbellSoundLinkId = 1,
                doorbellId = 1,
                soundId = 1,
                startDate = DateTime.Now.Date.AddDays(50),
                endDate = DateTime.Now.Date.AddDays(60),
                isDefault = false
            };

            //Act
            int rowsAffected = await doorbellSoundLinkRepository.AddDoorbellSoundLink(expected);

            Entities.DoorbellSoundLink actual = doorbellSoundLinkRepository.GetDoorbellSoundLinkById(1);

            //Assert
            Assert.Equal(expected.doorbellId, actual.doorbellId);
            Assert.Equal(expected.doorbellSoundLinkId, actual.doorbellSoundLinkId);
            Assert.Equal(expected.isDefault, actual.isDefault);
            Assert.Equal(expected.soundId, actual.soundId);
            Assert.Equal(expected.startDate, actual.startDate);
            Assert.Equal(expected.endDate, actual.endDate);
            Assert.True(rowsAffected == 1);
        }

        [Fact]
        public async Task ShouldUpdateDoorbellSoundLinkDevice()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();

            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);

            Entities.DoorbellSoundLink expected = doorbellSoundLinkRepository.GetDoorbellSoundLinkById(1);
            expected.startDate = DateTime.Now.Date.AddDays(150);
            expected.endDate = DateTime.Now.Date.AddDays(155);
            expected.isDefault = true;
            expected.soundId = 3;
            expected.doorbellId = 2;

            //Act
            int rowsAffected = await doorbellSoundLinkRepository.UpdateDoorbellSoundLink(expected);

            Entities.DoorbellSoundLink actual = doorbellSoundLinkRepository.GetDoorbellSoundLinkById(1);

            //Assert
            Assert.Equal(expected.doorbellId, actual.doorbellId);
            Assert.Equal(expected.doorbellSoundLinkId, actual.doorbellSoundLinkId);
            Assert.Equal(expected.isDefault, actual.isDefault);
            Assert.Equal(expected.soundId, actual.soundId);
            Assert.Equal(expected.startDate, actual.startDate);
            Assert.Equal(expected.endDate, actual.endDate);
            Assert.True(rowsAffected == 1);
        }

        [Fact]
        public async Task ShouldRemoveDoorbellSoundLink()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            DoorbellSoundLinkRepository doorbellSoundLinkRepository = new(_context.DoorbellContext);

            Entities.DoorbellSoundLink expected = doorbellSoundLinkRepository.GetDoorbellSoundLinkById(1);

            //Act
            int rowsAffected = await doorbellSoundLinkRepository.RemoveDoorbellSoundLink(expected);

            Action actionCall = () => doorbellSoundLinkRepository.GetDoorbellSoundLinkById(1);

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
            Assert.True(rowsAffected == 1);

        }

    }
}
