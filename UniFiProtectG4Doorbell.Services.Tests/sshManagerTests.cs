using UniFiProtectG4Doorbell.Services.Helpers;

namespace UniFiProtectG4Doorbell.Services.Tests
{
    public class sshManagerTests
    {
        [Theory]
        [InlineData("127.0.0.1")]//ipv4 localhost
        [InlineData("192.168.0.105")]//ipv4
        [InlineData("0:0:0:0:0:0:0:1")]//ipv6 localhost
        [InlineData("::1")]//ipv6 localhost shortcut
        [InlineData("6b67:2902:0d11:a207:e934:73c7:19b5:1f73")]//ipv6
        [InlineData("2001:db8:3333:4444:5555:6666:7777:8888")]//ipv6
        public void ShouldPassIsIpValid(string ipAddress)
        {
            //Arrange

            //Act
            bool actual = sshManager.isIpValid(ipAddress);

            //Assert
            Assert.True(actual);
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
        public void ShouldFailIsIpValid(string ipAddress)
        {
            //Arrange

            //Act
            bool actual = sshManager.isIpValid(ipAddress);

            //Assert
            Assert.False(actual);
        }

    }
}
