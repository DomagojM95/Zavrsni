using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using PlaninarskiDnevnik.Models;

namespace PlaninarskiDnevnik.Data
{
    public class PlaninarskiDnevnikContext:DbContext
    {
        public PlaninarskiDnevnikContext(DbContextOptions<PlaninarskiDnevnikContext> opcije)

            : base(opcije){



        }

        public DbSet<Planina> Planina { get; set; }

        public DbSet<Planinar> Planinar { get; set; }

        public DbSet<Izlet> Izlet { get; set; }
        public DbSet<Dnevnik> Dnevnik { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Izlet>().HasOne(p => p.Planina);
            modelBuilder.Entity<Dnevnik>().HasOne(i => i.Izlet);
            modelBuilder.Entity<Dnevnik>().HasOne(z => z.Planinar);
        }
    }
}