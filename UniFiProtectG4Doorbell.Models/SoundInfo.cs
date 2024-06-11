using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UniFiProtectG4Doorbell.Models
{
    public class SoundInfo
    {
        public int soundId { get; set; }

        public required string soundFileName { get; set; }
    }
}
