using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniFiProtectG4Doorbell.Data.Entities;

namespace UniFiProtectG4Doorbell.Data.Context
{
    public class DoorbellContext : DbContext
    {
        public DoorbellContext()
        {
        }

        public DoorbellContext(DbContextOptions<DoorbellContext> options):base(options)
        {
        }

        public DbSet<DoorbellItem> Devices { get; set; }

        public DbSet<SoundItem> Sounds { get; set; }

        public DbSet<DoorbellSoundLink> DeviceSoundLinks { get; set; }

        public DbSet<Settings> Settings { get; set; }


    }
}
