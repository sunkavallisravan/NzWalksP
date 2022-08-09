using AutoMapper; 

namespace NzWalks.API.Profiles
{
    public class RegionProfiles : Profile
    {

        public RegionProfiles()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap(); 
        }
    }
}
