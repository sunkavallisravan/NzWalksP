using Microsoft.EntityFrameworkCore;
using NzWalks.API.Models.Domain;

namespace NzWalks.API.Data
{
    public class NzWalksDBContext :DbContext
    {
        public NzWalksDBContext(DbContextOptions<NzWalksDBContext> options) :base(options)  
        {

        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
    }
}
