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
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
           return await nzWalksDBContext.Regions.ToListAsync(); 
        }
    }
}
