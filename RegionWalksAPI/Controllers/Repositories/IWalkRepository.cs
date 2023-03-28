using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegionWalksAPI.Models.Domain;

namespace RegionWalksAPI.Controllers.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllAsync();
        Task<Models.Domain.Walk> GetAsync(Guid id);
        Task<Walk> AddAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid id,Walk walk);
        Task<Walk> DeleteAsync(Guid id);
    }
}