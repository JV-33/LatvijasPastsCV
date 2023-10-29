using LatvijasPastsCV.Models;

namespace LatvijasPastsCV.Services
{
    public class DarbaPieredzeService : IDarbaPieredzeService
    {
        private readonly ICVDbContext _context;

        public DarbaPieredzeService(ICVDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DarbaPieredze> GetAll()
        {
            return _context.DarbaPieredze.ToList();
        }

        public DarbaPieredze GetById(int id)
        {
            return _context.DarbaPieredze.Find(id);
        }

        public void Add(DarbaPieredze darbaPieredze)
        {
            if (darbaPieredze == null)
            {
                throw new ArgumentNullException(nameof(darbaPieredze), "DarbaPieredze cannot be null.");
            }

            _context.DarbaPieredze.Add(darbaPieredze);
            _context.SaveChanges();
        }


        public void Update(DarbaPieredze darbaPieredze)
        {
            if (darbaPieredze == null)
            {
                throw new ArgumentNullException(nameof(darbaPieredze), "DarbaPieredze cannot be null.");
            }

            _context.DarbaPieredze.Update(darbaPieredze);
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var darbaPieredze = _context.DarbaPieredze.Find(id);
            if (darbaPieredze != null)
            {
                _context.DarbaPieredze.Remove(darbaPieredze);
                _context.SaveChanges();
            }
        }
    }
}