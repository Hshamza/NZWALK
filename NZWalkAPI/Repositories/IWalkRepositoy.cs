﻿using NZWalkAPI.Models.Domain;

namespace NZWalkAPI.Repositories
{
    public interface IWalkRepositoy
    {
        Task<Walk> CreateAsync(Walk walk);

        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber =1, int pageSize =1000);

        Task<Walk?> GetAsync(Guid id);

        Task<Walk?> UpdateAsync(Guid id,Walk walk);

        Task<Walk?> DeleteAsync(Guid id);
    }
}