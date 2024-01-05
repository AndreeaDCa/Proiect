using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proiect.Models;

namespace Proiect.Data
{
    public class ProiectContext : DbContext
    {
        public ProiectContext (DbContextOptions<ProiectContext> options)
            : base(options)
        {
        }

        public DbSet<Proiect.Models.MakeupArtist> MakeupArtist { get; set; } = default!;

        public DbSet<Proiect.Models.Service>? Service { get; set; }

        public DbSet<Appointment>? Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MakeupArtist>()
        .HasMany(m => m.Appointments)
        .WithOne(a => a.MakeupArtist)
        .HasForeignKey(a => a.MakeupArtistID)
        .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Proiect.Models.User>? User { get; set; }

        public DbSet<Proiect.Models.Review>? Review { get; set; }

        public DbSet<Proiect.Models.Product>? Product { get; set; }
        //public object Appointment { get; internal set; }
    }
}
