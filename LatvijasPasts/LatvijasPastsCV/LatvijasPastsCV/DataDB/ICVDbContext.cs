using Microsoft.EntityFrameworkCore;
using LatvijasPastsCV.Models;

public interface ICVDbContext
{
    DbSet<Pamatdati> Pamatdati { get; set; }
    DbSet<Izglitiba> Izglitiba { get; set; }
    DbSet<DarbaPieredze> DarbaPieredze { get; set; }
    DbSet<Prasmes> Prasmes { get; set; }
    DbSet<Adrese> Adreses { get; set; }
    DbSet<CV> CV { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}