using Microsoft.EntityFrameworkCore;
using NZWalkAPI.Data;
using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext _context;
        public SQLRegionRepository(NZWalksDbContext context) { 
            this._context = context; 
        }

        public async Task<Region> Create(Region region)
        {
           await  _context.Regions.AddAsync(region);
           await _context.SaveChangesAsync();
            return region;    
        }

        public async Task<Region?> Delete(Guid id)
        {
            var isFound = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (isFound == null)
            {
                return null;
            }

            _context.Regions.Remove(isFound);
           await _context.SaveChangesAsync();
            return isFound;
        }

        public async Task<List<Region>> GetAllSync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> GetById(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> Update(Guid id, Region region)
        {
            var isFound = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(isFound == null)
            {
                return null;
            }

            isFound.Code = region.Code;
            isFound.Name = region.Name;
            isFound.RegionImageUrl = region.RegionImageUrl;

            await _context.SaveChangesAsync();
            return isFound;
        }
    }
}
