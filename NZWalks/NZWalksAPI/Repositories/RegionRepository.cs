using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nzWalksDbContext;

        public RegionRepository(NZWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nzWalksDbContext.Regions.AddAsync(region);
            await nzWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(region == null)
            {
                return null;
            }

            // Delete region from database
            nzWalksDbContext.Regions.Remove(region);

            await nzWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nzWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(existingRegion == null)
            {
                return null;
            }

            existingRegion.Code=    region.Code;
            existingRegion.Name= region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long= region.Long;
            existingRegion.Population= region.Population;

            await nzWalksDbContext.SaveChangesAsync();

            return existingRegion;            

        }
    }
}
