using AutoMapper;
namespace NzWalks.API.Profiles
{
    public class WalkProfiles : Profile
    {
        public WalkProfiles()
        {
            CreateMap<Models.Domain.Walk, Models.DTO.Walk>().ReverseMap();
            CreateMap<Models.Domain.WalkDifficulty, Models.DTO.WalkDifficulty>().ReverseMap();
        }
    }
}
