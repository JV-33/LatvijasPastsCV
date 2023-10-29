using LatvijasPastsCV.Models;

namespace LatvijasPastsCV.Services
{
    public interface IDarbaPieredzeService
    {
        IEnumerable<DarbaPieredze> GetAll();
        DarbaPieredze GetById(int id);
        void Add(DarbaPieredze darbaPieredze);
        void Update(DarbaPieredze darbaPieredze);
        void Delete(int id);
    }
}