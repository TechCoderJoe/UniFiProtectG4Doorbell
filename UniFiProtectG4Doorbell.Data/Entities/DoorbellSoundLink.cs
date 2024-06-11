using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFiProtectG4Doorbell.Data.Entities
{
    public class DoorbellSoundLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int doorbellSoundLinkId { get; set; }

        public int soundId { get; set; }

        public int doorbellId { get; set; }

        public bool isDefault { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public virtual SoundItem? sound { get; set; }

        public virtual DoorbellItem? doorbell { get; set; }
    }
}
