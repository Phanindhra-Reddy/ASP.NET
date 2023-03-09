using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RegionWalksAPI.Data;
using RegionWalksAPI.Models.Domain;

namespace RegionWalksAPI.Controllers.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await 
            nZWalksDbContext.Walks
            .Include(x => x.Region)
            .Include(x => x.WalkDifficulty)
            .ToListAsync();
        }
        public async Task<Models.Domain.Walk> GetAsync(Guid id)
        {
            return await nZWalksDbContext.Walks
            .Include(x => x.Region)
            .Include(x => x.WalkDifficulty)
            .FirstOrDefaultAsync(x => x.Id == id);
            
        }
        public async Task<Walk> AddAsync(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nZWalksDbContext.Walks.AddAsync(walk);
            await nZWalksDbContext.SaveChangesAsync();
            return walk;
        }
    }
}