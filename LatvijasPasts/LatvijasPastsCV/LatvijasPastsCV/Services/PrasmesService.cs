using Microsoft.EntityFrameworkCore;
using LatvijasPastsCV.Models;

namespace LatvijasPastsCV.Services
{
    public class PrasmesService : IPrasmesService
    {
        private readonly ICVDbContext _context;

        public PrasmesService(ICVDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Prasmes> GetAll()
        {
            return _context.Prasmes.ToList();
        }

        public Prasmes GetById(int id)
        {
            return _context.Prasmes.Find(id);
        }

        public void Add(Prasmes prasme)
        {
            _context.Prasmes.Add(prasme);
            _context.SaveChanges();
        }

        public void Update(Prasmes prasme)
        {
            _context.Prasmes.Update(prasme);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var prasme = _context.Prasmes.Find(id);
            if (prasme != null)
            {
                _context.Prasmes.Remove(prasme);
                _context.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Remove(Prasmes prasmes)
        {
            _context.Prasmes.Remove(prasmes);
        }

        public DbSet<Prasmes> Prasmes => _context.Prasmes;
    }
}