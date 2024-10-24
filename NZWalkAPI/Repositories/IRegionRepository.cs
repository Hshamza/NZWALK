using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllSync();

        Task<Region?> GetById(Guid id);

        Task<Region> Create(Region region);
      
        Task<Region?> Update(Guid id,Region region);

        Task<Region?> Delete(Guid id);
    }
}
