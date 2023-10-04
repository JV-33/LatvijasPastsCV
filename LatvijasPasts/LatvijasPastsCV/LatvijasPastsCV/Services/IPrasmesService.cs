using Microsoft.EntityFrameworkCore;
using LatvijasPastsCV.Models;

namespace LatvijasPastsCV.Services
{
    public interface IPrasmesService
    {
        IEnumerable<Prasmes> GetAll();
        Prasmes GetById(int id);
        void Add(Prasmes prasmes);
        void Update(Prasmes prasmes);
        void Remove(Prasmes prasmes);
        void SaveChanges();
        DbSet<Prasmes> Prasmes { get; } 
    }
}