using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFiProtectG4Doorbell.Data.Entities
{
    public class SoundItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int soundId { get; set; }

        public required string soundFileName { get; set; }

        public virtual ICollection<DoorbellSoundLink>? doorbellSoundsLinks { get; set; }
    }
}
