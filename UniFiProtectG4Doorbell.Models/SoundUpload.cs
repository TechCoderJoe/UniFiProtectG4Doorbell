using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UniFiProtectG4Doorbell.Models
{
    public class SoundUpload
    {
        [Required(ErrorMessage = "Name is required")]
        [DisplayName("Sound Name")]
        public required string soundFileName { get; set; }

        [Required]
        public required IFormFile Upload { get; set; }


    }
}
