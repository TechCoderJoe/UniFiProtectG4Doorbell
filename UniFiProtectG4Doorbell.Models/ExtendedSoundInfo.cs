using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFiProtectG4Doorbell.Models
{
    public class ExtendedSoundInfo : SoundInfo
    {
        public List<DeviceInfo>? devices { get; set; }
    }
}
