using Microsoft.EntityFrameworkCore;
using PlaninarskiDnevnik.Models;

namespace PlaninarskiDnevnik.Data
{
    public class PlaninaContext:DbContext
    {
        public PlaninaContext(DbContextOptions<PlaninaContext> opcije)

            : base(opcije){



        }

        public DbSet<Planina> Planina { get; set; }
    }
}
