using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UniFiProtectG4Doorbell.Models
{
    public class UniFiInfo
    {
        [Required(ErrorMessage ="IP Address is required")]
        [DisplayName("UniFi IP Address")]
        public required string ipAddress { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [DisplayName("UniFi Username")]
        public required string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DisplayName("UniFi Password")]
        public required string password { get; set; }
    }
}
