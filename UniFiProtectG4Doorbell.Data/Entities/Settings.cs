using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFiProtectG4Doorbell.Data.Entities
{
    public class Settings
    {
        [Key]
        public required string settingsName { get; set; }

        public required string settingsValue { get; set; }
    }
}
