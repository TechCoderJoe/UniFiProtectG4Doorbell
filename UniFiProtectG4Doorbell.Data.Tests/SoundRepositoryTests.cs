
using UniFiProtectG4Doorbell.Data.Repositories;
using UniFiProtectG4Doorbell.Data.Tests.DataContext;

namespace UniFiProtectG4Doorbell.Data.Tests
{
    public class SoundRepositoryTests
    {
        private readonly InMemoryDbContext _context;

        public SoundRepositoryTests() 
        {  
            _context = new InMemoryDbContext(); 
        }

        [Fact]
        public void ShouldGetSoundList()
        {
            //Arrange
            _context.Reset();
            _context.seedSounds();
            SoundRepository soundRepository = new (_context.DoorbellContext);

            int expected = 2;

            //Act
            int actual = soundRepository.GetSounds().Count;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldThrowNullExceptionOnGetSoundById()
        {
            //Arrange
            _context.Reset();
            SoundRepository soundRepository = new(_context.DoorbellContext);

            Action actionCall = () => soundRepository.GetSoundItemById(1);

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
        }

        [Fact]
        public void ShouldGetSoundById()
        {
            //Arrange
            _context.Reset();
            _context.seedSounds();
            SoundRepository soundRepository = new(_context.DoorbellContext);

            Entities.SoundItem expected = new ()
            {
                 soundFileName = "sound1.wav"
            };

            //Act
            Entities.SoundItem actual = soundRepository.GetSoundItemById(1);

            //Assert
            Assert.Equal(expected.soundFileName, actual.soundFileName);
        }

        [Fact]
        public async Task ShouldAddSound()
        {
            //Arrange
            _context.Reset();
            SoundRepository soundRepository = new(_context.DoorbellContext);

            Entities.SoundItem expected = new()
            {
                 soundFileName = "addSound.wav"
            };

            //Act
            int rowsAffected = await soundRepository.AddSound(expected);

            Entities.SoundItem actual = soundRepository.GetSoundItemById(1);

            //Assert
            Assert.Equal(expected.soundFileName, actual.soundFileName);
            Assert.True(rowsAffected == 1);
        }

        
        [Fact]
        public async Task ShouldRemoveSound()
        {
            //Arrange
            _context.Reset();
            _context.seedSounds();
            SoundRepository soundRepository = new(_context.DoorbellContext);

            Entities.SoundItem expected = soundRepository.GetSoundItemById(1);

            //Act
            int rowsAffected = await soundRepository.RemoveSound(expected);

            Action actionCall = () => soundRepository.GetSoundItemById(1);

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
            Assert.True(rowsAffected == 1);

        }

        [Fact]
        public async Task ShouldRemoveSoundWithSoundLink()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            SoundRepository soundRepository = new(_context.DoorbellContext);

            Entities.SoundItem expected = soundRepository.GetSoundItemById(1);

            //Act
            int rowsAffected = await soundRepository.RemoveSound(expected);

            Action actionCall = () => soundRepository.GetSoundItemById(1);

            //Act
            Exception actual = Record.Exception(actionCall);

            //Assert
            Assert.IsType<NullReferenceException>(actual);
            Assert.True(rowsAffected == 4);

        }
    }
}
