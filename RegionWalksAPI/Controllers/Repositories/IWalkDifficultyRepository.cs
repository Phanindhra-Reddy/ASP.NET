using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegionWalksAPI.Models.Domain;

namespace RegionWalksAPI.Controllers.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty> GetAsync(Guid id);
        Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> UpdateAsync(Guid id,WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> DeleteAsync(Guid id);
    }
}