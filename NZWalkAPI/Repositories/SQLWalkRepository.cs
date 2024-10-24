using Microsoft.EntityFrameworkCore;
using NZWalkAPI.Data;
using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repositories
{
    public class SQLWalkRepository: IWalkRepositoy
    {
        private readonly NZWalksDbContext _context;
        public SQLWalkRepository(NZWalksDbContext context) { 
            this._context = context;
        }      
        public async Task<Walk> CreateAsync(Walk walk) { 

            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkDomainModel = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkDomainModel == null)
            {
                return null;
            }

            _context.Walks.Remove(walkDomainModel);
            await _context.SaveChangesAsync();
            return walkDomainModel;



        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = _context.Walks.Include(x => x.Difficulty).Include(x => x.Region).AsQueryable();

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ?  walks.OrderBy(x => x.Name) : walks.OrderByDescending(x=> x.Name) ;
                }
                if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
           // return await _context.Walks.Include(x=>x.Difficulty).Include(x=>x.Region).ToListAsync();

        }

        public async Task<Walk?> GetAsync(Guid id)
        {
            return await _context.Walks.Include(x => x.Difficulty).Include(x => x.Region).FirstOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var walkDomainModel = await  _context.Walks.FirstOrDefaultAsync(x=> x.Id == id);  
            if(walkDomainModel == null)
            {
                return null;
            }

            walkDomainModel.Name = walk.Name;
            walkDomainModel.Description = walk.Description;
            walkDomainModel.DifficultyId = walk.DifficultyId;
            walkDomainModel.LengthInKm = walk.LengthInKm;
            walkDomainModel.WalkImageUrl = walk.WalkImageUrl;
            walkDomainModel.RegionId =walk.RegionId;

            await _context.SaveChangesAsync();

            return walkDomainModel;
         

        }
    }
}
