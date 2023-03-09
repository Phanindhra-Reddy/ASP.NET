using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RegionWalksAPI.Data;
using RegionWalksAPI.Models.Domain;

namespace RegionWalksAPI.Controllers.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await 
            nZWalksDbContext.Regions
            .Include(x => x.Walks)
            .ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }
    
        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await GetAsync(id);
            if(region == null){
                return null;
            }
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            //var existingRegion = await getAsync(id);
            if(existingRegion == null){
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Long = region.Long;
            existingRegion.Lat = region.Lat;
            existingRegion.Area = region.Area;
            existingRegion.Population = region.Population;
            await nZWalksDbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}