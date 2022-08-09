using Microsoft.EntityFrameworkCore;
using NzWalks.API.Data;
using NzWalks.API.Models.Domain;

namespace NzWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NzWalksDBContext nzWalksDBContext;
        public RegionRepository(NzWalksDBContext nzWalksDBContext)
        {
            this.nzWalksDBContext = nzWalksDBContext; 
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await nzWalksDBContext.AddAsync(region);
            await nzWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await nzWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            nzWalksDBContext.Regions.Remove(region);
            await nzWalksDBContext.SaveChangesAsync();
            return region; 
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
           return await nzWalksDBContext.Regions.ToListAsync(); 
        }

        public  async Task<Region> GetAsync(Guid id)
        {
            var existingRegion = await nzWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }

            return existingRegion;
        }

        public async  Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await nzWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
           
            existingRegion.Name = region.Name;
            existingRegion.Code = region.Code;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await nzWalksDBContext.SaveChangesAsync();
            return region;
        }
    }
}
