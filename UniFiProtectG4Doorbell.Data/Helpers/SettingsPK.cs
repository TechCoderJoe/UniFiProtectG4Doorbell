using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFiProtectG4Doorbell.Data.Helpers
{
    internal class SettingsPK
    {
        public static string protectUsername => "protect_username";

        public static string protectPassword => "protect_password";

        public static string protectSoundUploadPath => "protect_sound_upload_path";

        public static string protectSoundProcess => "protect_sound_process";

        public static string unifiIpAddress => "unifi_ip_address";

        public static string unifiUsername => "unifi_username";

        public static string unifiPassword => "unifi_password";

        public static string doorbellCustomSoundFileName = "doorbell_custom_sound_file_name";

        public static string firstTimeSetup = "first_time_setup";

        public static string currentSetupStep = "current_setup_step";
    }
}
