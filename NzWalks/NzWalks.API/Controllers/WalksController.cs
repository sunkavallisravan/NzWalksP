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
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper,
            IRegionRepository regionRepository,IWalkDifficultyRepository walkDifficultyRepository )
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultyRepository = walkDifficultyRepository;
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

            if(!await ValidateAddWalkAsync(addWalkRequest))
            {
                return BadRequest(ModelState);
            }

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


            if (!await ValidateUpdateWalkAsync(updateWalkRequest))
            {
                return BadRequest(ModelState);
            }

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


        private async Task<bool> ValidateAddWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            if (addWalkRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest),
                    $"AddWalk data is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(addWalkRequest.Name), $"{nameof(addWalkRequest.Name)} Cannot be null or empty or whitespace");
            }            
            if (addWalkRequest.Length > 0)
            {
                ModelState.AddModelError(nameof(addWalkRequest.Length), $"{nameof(addWalkRequest.Length)} Cannot be less than zero");
            }

            var region = await regionRepository.GetAsync(addWalkRequest.RegionId);

            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest),
                    $" Invalid regionId.");
                return false;
            }
            var walkdifficulty = await walkDifficultyRepository.GetAsync(addWalkRequest.WalkDifficultyId);

            if (walkdifficulty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest),
                   $" Invalid Walkdifficultyid.");
                return false;
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;

        }


        private async Task<bool> ValidateUpdateWalkAsync(Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            if (updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest),
                    $"UpdateWalk data is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateWalkRequest.Name))
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Name), $"{nameof(updateWalkRequest.Name)} Cannot be null or empty or whitespace");
            }
            if (updateWalkRequest.Length > 0)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Length), $"{nameof(updateWalkRequest.Length)} Cannot be less than zero");
            }

            var region = await regionRepository.GetAsync(updateWalkRequest.RegionId);

            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest),
                    $" Invalid regionId.");
                return false;
            }
            var walkdifficulty = await walkDifficultyRepository.GetAsync(updateWalkRequest.WalkDifficultyId);

            if (walkdifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest),
                   $" Invalid Walkdifficultyid.");
                return false;
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;

        }

    }
}
