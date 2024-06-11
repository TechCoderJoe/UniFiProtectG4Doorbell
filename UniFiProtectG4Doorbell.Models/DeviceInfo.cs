
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UniFiProtectG4Doorbell.Models
{
    public class DeviceInfo
    {
        public int doorbellId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [DisplayName("Device Name")]
        public required string doorbellName { get; set; }

        [Required(ErrorMessage = "IP is required")]
        [DisplayName("IP Address")]
        public required string ipAddress { get; set; }

        

    }
}

