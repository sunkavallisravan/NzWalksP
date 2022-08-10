using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Repositories;

namespace NzWalks.API.Controllers
{
    [ApiController]
    [Route("controller")]

    public class WalksController : Controller
    {

        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks()
        {
            //fetch data from db
            var walksDomine = await walkRepository.GetAllAsync();
            //convert domine walks to dto walks
            var walksDto = mapper.Map<List<Models.DTO.Walk>>(walksDomine);
            // return response
            return Ok(walksDto);
        }


        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            //fetch data from db
            var walkDomine = await walkRepository.GetAsync(id);
            //convert domine walks to dto walks
            var walkDto = mapper.Map<Models.DTO.Walk>(walkDomine);
            // return response
            return Ok(walkDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            // convert dto to domine object
            var walkDomine = new Models.Domain.Walk
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId

            };
            // pass domine object to repository to persist

            walkDomine = await walkRepository.AddAsync(walkDomine);

            // convert domine object back to dto

            var walkDto = new Models.DTO.Walk()
            {
                Id = walkDomine.Id,
                WalkDifficultyId = walkDomine.WalkDifficultyId,
                Length = walkDomine.Length,
                Name = walkDomine.Name,
                RegionId = walkDomine.RegionId,
            };



            // send dto response back to client 

            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDto.Id }, walkDto);


        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            // convert dto to domine object 

            var walkDomine = new Models.Domain.Walk()
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };

            // pass detailes to repository - get domine object in response (or null)

            walkDomine = await walkRepository.UpdateAsync(id, walkDomine);

            // Handle null --Notfound , COnvert back to dto and return response 

            if (walkDomine == null)
            {
                return NotFound();
            }
            else
            {
                var walkDto = new Models.DTO.Walk()
                {
                    Id = walkDomine.Id,
                    Length = walkDomine.Length,
                    Name = walkDomine.Name,
                    RegionId = walkDomine.RegionId,
                    WalkDifficultyId = walkDomine.WalkDifficultyId
                };
                return Ok(walkDto);
            }

        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
           var walkDomie = await walkRepository.DeleteAsync(id);

            if (walkDomie == null)
            {
                return NotFound();
            }

           var walkDto = mapper.Map<Models.DTO.Walk>(walkDomie);

            return Ok(walkDto);
        }
    }
}
