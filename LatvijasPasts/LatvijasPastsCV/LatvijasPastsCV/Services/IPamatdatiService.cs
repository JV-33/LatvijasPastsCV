using LatvijasPastsCV.Models;

namespace LatvijasPastsCV.Services
{
    public interface IPamatdatiService
    {
        IEnumerable<Pamatdati> GetAll();
        Pamatdati GetById(int id);
        void Add(Pamatdati pamatdati);
        void Update(Pamatdati pamatdati);
        void Delete(int id);
    }
}