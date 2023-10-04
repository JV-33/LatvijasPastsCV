using LatvijasPastsCV.Models;

namespace LatvijasPastsCV.Services
{
    public class AdreseService : IAdreseService
    {
        private readonly ICVDbContext _context;

        public AdreseService(ICVDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Adrese> GetAll()
        {
            return _context.Adreses.ToList();
        }

        public Adrese GetById(int id)
        {
            return _context.Adreses.Find(id);
        }

        public void Add(Adrese adrese)
        {
            if (adrese == null)
            {
                throw new ArgumentNullException(nameof(adrese), "Adrese cannot be null");
            }

            _context.Adreses.Add(adrese);
            _context.SaveChanges();
        }


        public void Update(Adrese adrese)
        {
            if (adrese == null)
            {
                throw new ArgumentNullException(nameof(adrese), "Adrese cannot be null");
            }

            _context.Adreses.Update(adrese);
            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var adrese = _context.Adreses.Find(id);
            if (adrese != null)
            {
                _context.Adreses.Remove(adrese);
                _context.SaveChanges();
            }
        }
    }
}