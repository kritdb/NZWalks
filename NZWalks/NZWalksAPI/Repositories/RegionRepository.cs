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
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
           return await nzWalksDbContext.Regions.ToListAsync();
        }
    }
}
