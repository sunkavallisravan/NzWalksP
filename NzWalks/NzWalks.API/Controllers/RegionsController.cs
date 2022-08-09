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
        public async Task<IActionResult> GetAllRegionsAsync()
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

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if (region == null) return NotFound();
            var regionDto = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // requestdto to domine model
            var region = new Models.Domain.Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population
            };
            // pass detailes to repository
            region = await regionRepository.AddAsync(region);
            // convert data back to dto
            var regionDto = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDto.Id }, regionDto);

            
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult>DeleteRegion(Guid id)
        {
            var region = await regionRepository.DeleteAsync(id);
            if (region == null) return NotFound();
            var regionDto = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            return Ok(regionDto);
        }



        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult>UpdateRegionAsync([FromRoute] Guid id,[FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            // requestdto to domine model
            var region = new Models.Domain.Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population
            };
            // pass detailes to repository
            region = await regionRepository.UpdateAsync(id,region);
            // convert data back to dto
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };

            return Ok(regionDto);
        }
    }
}
