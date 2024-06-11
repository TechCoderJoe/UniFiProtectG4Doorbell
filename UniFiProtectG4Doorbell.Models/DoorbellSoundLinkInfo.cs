using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFiProtectG4Doorbell.Models
{
    public class DoorbellSoundLinkInfo
    {
        public int doorbellSoundLinkId { get; set; }

        public int soundId { get; set; }

        public int doorbellId { get; set; }

        public bool isDefault { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }
    }
}
