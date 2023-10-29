using LatvijasPastsCV.Models;

namespace LatvijasPastsCV.Services
{
    public interface IIzglitibaService
    {
        IEnumerable<Izglitiba> GetAll();
        Izglitiba GetById(int id);
        void Add(Izglitiba izglitiba);
        void Update(Izglitiba izglitiba);
        void Delete(int id);
    }
}