using SixMinApi.Models;

namespace SixMinApi.Data
{
    public interface IPlatformRepo 
    {
        Task SaveChanges();
        Task<Platform?> GetPlatformById(int id);
        Task<IEnumerable<Platform>> GetAllPlatforms();
        Task CreatePlatform(Platform pm);
        Task UpdatePlatform(Platform pm);

        void DeletePlatform(Platform pm);

    }
}