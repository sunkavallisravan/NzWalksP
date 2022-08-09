using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Models.Domain;
using NzWalks.API.Repositories;

namespace NzWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {

            #region

            //var regions = new List<Region>()
            //{
            //    new Region
            //    {
            //        Id= Guid.NewGuid(),
            //        Name="Test",
            //        Area=123,
            //        Code="Test123",
            //        Lat=1.12546,
            //        Long=-123.23,                    
            //        Population=789456,

            //    },
            //     new Region
            //    {
            //        Id= Guid.NewGuid(),
            //        Name="Test1",
            //        Area=1231,
            //        Code="Test1231",
            //        Lat=1.125467,
            //        Long=-123.234,
            //        Population=7894569,

            //    }
            //};
            #endregion staticdata 

            var regions = await regionRepository.GetAllAsync();

            //var regionsDto = new List<Models.DTO.Region>();

            ////Return Region DTO

            //regions.ToList().ForEach(regions =>
            //{
            //    var regionDto = new Models.DTO.Region()
            //    {
            //        Id = regions.Id,
            //        Name=regions.Name,
            //        Code=regions.Code,
            //        Area=regions.Area,
            //        Lat=regions.Lat,
            //        Long=regions.Long,  
            //        Population=regions.Population,
            //    };
            //    regionsDto.Add(regionDto);
            //});

            // return Ok(regionsDto); 

            var regionsDto = mapper.Map<List<Models.DTO.Region>>(regions);
            return Ok(regionsDto);

        }
       
    }
}
