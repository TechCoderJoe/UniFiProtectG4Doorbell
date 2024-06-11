using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace UniFiProtectG4Doorbell.Models
{
    public class ProtectInfo
    {
        [Required(ErrorMessage = "Username is required")]
        [DisplayName("Protect Username")]
        public required string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Protect Password")]
        public required string password { get; set; }

        [Required(ErrorMessage = "Sound File Name is required")]
        [DisplayName("Sound File Name")]
        public required string soundFileName { get; set; }

        [Required(ErrorMessage = "Sound Upload Path is required")]
        [DisplayName("Sound Upload Path")]
        public required string soundUploadPath { get; set; }

        [Required(ErrorMessage = "Sound Process Name is required")]
        [DisplayName("Sound Process Name")]
        public required string soundProcess { get; set; }

    }
}
