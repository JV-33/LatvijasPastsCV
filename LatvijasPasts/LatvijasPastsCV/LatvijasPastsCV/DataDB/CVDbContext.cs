using Microsoft.EntityFrameworkCore;
using LatvijasPastsCV.Models;

namespace LatvijasPastsCV.DBData
{
    public class CVDbContext : DbContext, ICVDbContext
    {
        public CVDbContext(DbContextOptions<CVDbContext> options) : base(options)
        {
        }

        public DbSet<Pamatdati> Pamatdati { get; set; }
        public DbSet<Izglitiba> Izglitiba { get; set; }
        public DbSet<DarbaPieredze> DarbaPieredze { get; set; }
        public DbSet<Prasmes> Prasmes { get; set; }
        public DbSet<Adrese> Adreses { get; set; }
        public DbSet<CV> CV { get; set; }
    }
}