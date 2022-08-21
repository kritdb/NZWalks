﻿using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

      

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            // Assign new id
            walkDifficulty.Id = Guid.NewGuid();

            await nZWalksDbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var existingWalkDiff = await nZWalksDbContext.WalkDifficulty.FindAsync(id);

            if (existingWalkDiff == null)
            {
                return null;
            }

            nZWalksDbContext.WalkDifficulty.Remove(existingWalkDiff);
            await nZWalksDbContext.SaveChangesAsync();

            return existingWalkDiff;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDbContext.WalkDifficulty.ToListAsync();   
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
           return await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDiff = await nZWalksDbContext.WalkDifficulty.FindAsync(id);

            if (existingWalkDiff != null)
            {
                existingWalkDiff.Code = walkDifficulty.Code;
               
                await nZWalksDbContext.SaveChangesAsync();
                return existingWalkDiff;
            }

            return null;
        }
    }
}
