using Microsoft.EntityFrameworkCore;
using NzWalks.API.Data;
using NzWalks.API.Models.Domain;

namespace NzWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NzWalksDBContext nzWalksDBContext;
        public WalkRepository(NzWalksDBContext nzWalksDBContext)
        {
            this.nzWalksDBContext = nzWalksDBContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            //asign new id 
            walk.Id = Guid.NewGuid();
            await nzWalksDBContext.Walks.AddAsync(walk);
            await nzWalksDBContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid Id)
        {
            var existingWalk = await nzWalksDBContext.Walks.FindAsync(Id);
            if (existingWalk == null)
            {
                return null;
            }

            nzWalksDBContext.Remove(existingWalk);
            await nzWalksDBContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await nzWalksDBContext.Walks
                .Include(x => x.region)
                .Include(x => x.walkDifficulty).ToListAsync();
        }
        public Task<Walk> GetAsync(Guid Id)
        {
            return nzWalksDBContext.Walks
                 .Include(x => x.region)
                 .Include(x => x.walkDifficulty).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Walk> UpdateAsync(Guid Id, Walk walk)
        {
            var existingWalk = await nzWalksDBContext.Walks.FindAsync(Id);

            if (existingWalk != null)
            {
                existingWalk.Length = walk.Length;
                existingWalk.Name = walk.Name;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.RegionId = walk.RegionId;
                await nzWalksDBContext.SaveChangesAsync();
                return existingWalk;
            }
            return null;

        }
    }
}
