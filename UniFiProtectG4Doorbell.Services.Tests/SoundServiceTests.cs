using UniFiProtectG4Doorbell.Data.Repositories;
using UniFiProtectG4Doorbell.Data.Tests.DataContext;
using UniFiProtectG4Doorbell.Models;

namespace UniFiProtectG4Doorbell.Services.Tests
{
    public class SoundServiceTests
    {
        private readonly InMemoryDbContext _context;

        public SoundServiceTests() 
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
            SoundService soundService = new(soundRepository);

            int expected = 2;

            //Act
            int actual = soundService.GetSounds().Count;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldGetSoundListNoResults()
        {
            //Arrange
            _context.Reset();
            SoundRepository soundRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SoundService soundService = new(soundRepository);

            int expected = 0;

            //Act
            int actual = soundRepository.GetSounds().Count;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldBeNullGetSoundById()
        {
            //Arrange
            _context.Reset();
            SoundRepository soundRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SoundService soundService = new(soundRepository);

            //Act
            SoundInfo? actual = soundService.GetSound(1);

            //Assert
            Assert.Null(actual);
        }

        [Fact]
        public void ShouldGetSoundById()
        {
            //Arrange
            _context.Reset();
            _context.seedSounds();
            SoundRepository soundRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SoundService soundService = new(soundRepository);

            SoundInfo expected = new ()
            {
                 soundId = 1,
                  soundFileName = "sound1.wav"
            };

            //Act
            SoundInfo? actual = soundService.GetSound(1);

            //Assert
            Assert.NotNull(actual);
            Assert.Equal(expected.soundId, actual.soundId);
            Assert.Equal(expected.soundFileName, actual.soundFileName);
        }

        [Fact]
        public async Task ShouldAddSound()
        {
            //Arrange
            _context.Reset();
            SoundRepository soundRepository = new(_context.DoorbellContext);
            SettingsRepository settingsRepository = new(_context.DoorbellContext);
            SoundService soundService = new(soundRepository);

            SoundInfo expected = new()
            {
                soundFileName = "AddSound.wav"
            };

            //Act
            int rowsAffected = await soundService.SaveSound(expected);

            SoundInfo? actual = soundService.GetSound(1);

            //Assert
            Assert.NotNull(actual);
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
            SoundService soundService = new(soundRepository);

            //Act
            int rowsAffected = await soundService.RemoveSound(1);
            SoundInfo? actual = soundService.GetSound(1);

            //Assert
            Assert.Null(actual);
            Assert.True(rowsAffected == 1);

        }

        [Fact]
        public async Task ShouldRemoveSoundWithSoundLink()
        {
            //Arrange
            _context.Reset();
            _context.seedSoundLink();
            SoundRepository soundRepository = new(_context.DoorbellContext);
            SoundService soundService = new(soundRepository);


            //Act
            int rowsAffected = await soundService.RemoveSound(1);
            SoundInfo? actual = soundService.GetSound(1);

            //Assert
            Assert.Null(actual);
            Assert.True(rowsAffected == 4);

        }
    }
}
