using LatvijasPastsCV.Models;
using LatvijasPastsCV.DBData;

namespace LatvijasPastsCV.Services
{
    public class IzglitibaService : IIzglitibaService
    {
        private readonly CVDbContext _context;

        public IzglitibaService(CVDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Izglitiba> GetAll()
        {
            return _context.Izglitiba.ToList();
        }

        public Izglitiba GetById(int id)
        {
            return _context.Izglitiba.Find(id);
        }

        public void Add(Izglitiba izglitiba)
        {
            _context.Izglitiba.Add(izglitiba);
            _context.SaveChanges();
        }

        public void Update(Izglitiba izglitiba)
        {
            _context.Izglitiba.Update(izglitiba);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var izglitiba = _context.Izglitiba.Find(id);
            if (izglitiba != null)
            {
                _context.Izglitiba.Remove(izglitiba);
                _context.SaveChanges();
            }
        }
    }
}