using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniFiProtectG4Doorbell.Data.Entities
{
    public class DoorbellItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int doorbellId { get; set; }

        public required string doorbellName { get; set; }

        public required string ipAddress { get; set; }

        public virtual ICollection<DoorbellSoundLink>? doorbellSoundsLinks { get; set; }
    }
}
