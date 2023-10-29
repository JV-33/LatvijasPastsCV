using LatvijasPastsCV.Models;

namespace LatvijasPastsCV.Services
{
    public class CVDataService : ICVDataService
    {
        private readonly ICVDbContext _context;

        public CVDataService(ICVDbContext context)
        {
            _context = context;
        }

        public CV GetCVById(int id)
        {
            var cv = new CV
            {
                Pamatdati = _context.Pamatdati.FirstOrDefault(pd => pd.ID == id),
            };
            return cv;
        }

        public void AddCV(CV cv)
        {
            _context.Pamatdati.Add(cv.Pamatdati);
            if (cv.Adrese != null)
            {
                _context.Adreses.Add(cv.Adrese);
            }

            foreach (var izglitiba in cv.Izglitiba)
            {
                _context.Izglitiba.Add(izglitiba);
            }

            foreach (var darbaPieredze in cv.DarbaPieredzes)
            {
                _context.DarbaPieredze.Add(darbaPieredze);
            }

            foreach (var prasme in cv.Prasmes)
            {
                _context.Prasmes.Add(prasme);
            }

            _context.SaveChanges();
        }
    }
}