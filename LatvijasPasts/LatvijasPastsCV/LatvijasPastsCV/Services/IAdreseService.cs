using LatvijasPastsCV.Models;

namespace LatvijasPastsCV.Services
{
    public interface IAdreseService
    {
        IEnumerable<Adrese> GetAll();
        Adrese GetById(int id);
        void Add(Adrese adrese);
        void Update(Adrese adrese);
        void Delete(int id);
    }
}