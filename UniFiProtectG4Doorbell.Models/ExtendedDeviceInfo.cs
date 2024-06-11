using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFiProtectG4Doorbell.Models
{
    public class ExtendedDeviceInfo : DeviceInfo
    {
        public List<SoundInfo>? sounds { get; set; }
    }
}
