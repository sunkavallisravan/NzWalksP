using Microsoft.EntityFrameworkCore;
using NzWalks.API.Data;
using NzWalks.API.Models.Domain;

namespace NzWalks.API.Repositories
{
    public class WalkDIfficultyRepository : IWalkDifficultyRepository
    {
        private readonly NzWalksDBContext nzWalksDBContext;
        public WalkDIfficultyRepository(NzWalksDBContext nzWalksDBContext)
        {
            this.nzWalksDBContext = nzWalksDBContext;
        }
        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nzWalksDBContext.AddAsync(walkDifficulty);
            await nzWalksDBContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nzWalksDBContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            var existingWalkDifficulty = await nzWalksDBContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalkDifficulty == null)
            {
                return null;
            }

            return existingWalkDifficulty;
        }
        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await nzWalksDBContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalkDifficulty == null)
            {
                return null;
            }         
            existingWalkDifficulty.Code = walkDifficulty.Code;            
            await nzWalksDBContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var walkdifficulty = await nzWalksDBContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
            if (walkdifficulty == null)
            {
                return null;
            }
            nzWalksDBContext.WalkDifficulty.Remove(walkdifficulty);
            await nzWalksDBContext.SaveChangesAsync();
            return walkdifficulty;
        }

    }
}
