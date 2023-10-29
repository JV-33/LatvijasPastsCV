using LatvijasPastsCV.Models;

namespace LatvijasPastsCV.Services
{
    public interface ICVDataService
    {
        CV GetCVById(int id);
        void AddCV(CV cv);
    }
}