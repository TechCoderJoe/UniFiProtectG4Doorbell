using UniFiProtectG4Doorbell.Data.Entities;
using UniFiProtectG4Doorbell.Models;
using UniFiProtectG4Doorbell.Services.Helpers;

namespace UniFiProtectG4Doorbell.Services.Tests
{
    public class ExtensionMethodsTests
    {

        [Fact]
        public void ShouldConvertDoorbellDeviceEntityToDto()
        {
            //Arrange
            DoorbellItem expected = new ()
            {
                doorbellId = 1,
                doorbellName = "Test",
                ipAddress = "192.168.0.105"
            };

            //Act
            DeviceInfo actual = expected.ToDTO();

            //Assert
            Assert.Equal(expected.doorbellId, actual.doorbellId);
            Assert.Equal(expected.doorbellName, actual.doorbellName);
            Assert.Equal(expected.ipAddress, actual.ipAddress);

        }

        [Fact]
        public void ShouldConvertDoorbellDeviceDtoToEntity()
        {
            //Arrange
            DeviceInfo expected = new ()
            {
                doorbellId = 1,
                doorbellName = "Test",
                ipAddress = "192.168.0.105"
            };

            //Act
            DoorbellItem actual = expected.ToEntity();

            //Assert
            Assert.Equal(expected.doorbellId, actual.doorbellId);
            Assert.Equal(expected.doorbellName, actual.doorbellName);
            Assert.Equal(expected.ipAddress, actual.ipAddress);

        }

        [Fact]
        public void ShouldConvertSoundEntityToDto()
        {
            //Arrange
            SoundItem expected = new()
            {
                 soundFileName = "Test",
                  soundId = 1
            };

            //Act
            SoundInfo actual = expected.ToDTO();

            //Assert
            Assert.Equal(expected.soundFileName, actual.soundFileName);
            Assert.Equal(expected.soundId, actual.soundId);

        }

        [Fact]
        public void ShouldConvertSoundDtoToEntity()
        {
            //Arrange
            SoundInfo expected = new()
            {
                 soundFileName = "Test",
                  soundId = 1
            };

            //Act
            SoundItem actual = expected.ToEntity();

            //Assert
            Assert.Equal(expected.soundFileName, actual.soundFileName);
            Assert.Equal(expected.soundId, actual.soundId);

        }

        [Fact]
        public void ShouldConvertDeviceSoundLinkEntityToDto()
        {
            //Arrange
            DoorbellSoundLink expected = new()
            {
                doorbellId = 3,
                doorbellSoundLinkId = 1,
                soundId = 2,
                isDefault = true,
                startDate = DateTime.Now.Date,
                endDate = DateTime.Now.Date.AddDays(5)
            };

            //Act
            DoorbellSoundLinkInfo actual = expected.ToDTO();

            //Assert
            Assert.Equal(expected.doorbellId, actual.doorbellId);
            Assert.Equal(expected.doorbellSoundLinkId, actual.doorbellSoundLinkId);
            Assert.Equal(expected.soundId, actual.soundId);
            Assert.Equal(expected.isDefault, actual.isDefault);
            Assert.Equal(expected.startDate, actual.startDate);
            Assert.Equal(expected.endDate, actual.endDate);
        }

        [Fact]
        public void ShouldConvertDeviceSoundLinkDtoToEntity()
        {
            //Arrange
            DoorbellSoundLinkInfo expected = new()
            {
                doorbellId = 3,
                doorbellSoundLinkId = 1,
                soundId = 2,
                isDefault = true,
                startDate = DateTime.Now.Date,
                endDate = DateTime.Now.Date.AddDays(5)
            };

            //Act
            DoorbellSoundLink actual = expected.ToEntity();

            //Assert
            Assert.Equal(expected.doorbellId, actual.doorbellId);
            Assert.Equal(expected.doorbellSoundLinkId, actual.doorbellSoundLinkId);
            Assert.Equal(expected.soundId, actual.soundId);
            Assert.Equal(expected.isDefault, actual.isDefault);
            Assert.Equal(expected.startDate, actual.startDate);
            Assert.Equal(expected.endDate, actual.endDate);

        }
    }
}
