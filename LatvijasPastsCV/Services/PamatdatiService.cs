using LatvijasPastsCV.Models;
using LatvijasPastsCV.DBData;

namespace LatvijasPastsCV.Services
{
    public class PamatdatiService : IPamatdatiService
    {
        private readonly CVDbContext _context;

        public PamatdatiService(CVDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Pamatdati> GetAll()
        {
            return _context.Pamatdati.ToList();
        }

        public Pamatdati GetById(int id)
        {
            return _context.Pamatdati.Find(id);
        }

        public void Add(Pamatdati pamatdati)
        {
            _context.Pamatdati.Add(pamatdati);
            _context.SaveChanges();
        }

        public void Update(Pamatdati pamatdati)
        {
            _context.Pamatdati.Update(pamatdati);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var pamatdati = _context.Pamatdati.Find(id);
            if (pamatdati != null)
            {
                _context.Pamatdati.Remove(pamatdati);
                _context.SaveChanges();
            }
        }
    }
}