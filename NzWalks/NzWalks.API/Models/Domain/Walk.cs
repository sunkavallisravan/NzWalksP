namespace NzWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Length { get; set; }

        public Guid RegionId { get; set; }

        public Guid WalkDifficultyId { get; set; }  


        // Navigation property

        public Region region { get; set; }


        public WalkDifficulty walkDifficulty { get; set; }
    }
}
