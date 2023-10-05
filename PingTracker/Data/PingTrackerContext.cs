using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PingTracker.Models;

namespace PingTracker.Data
{
    public class PingTrackerContext : DbContext
    {
        public PingTrackerContext (DbContextOptions<PingTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<PingTracker.Models.Website> Websites { get; set; } = default!;
        public DbSet<PingResult> PingResults { get; set; }
    }
}
