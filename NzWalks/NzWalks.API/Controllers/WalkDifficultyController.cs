using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NzWalks.API.Models.Domain;
using NzWalks.API.Repositories;

namespace NzWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var walkDifficulty = await walkDifficultyRepository.GetAllAsync();
            var walkDifficultyDto = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficulty);
            return Ok(walkDifficultyDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);
            if (walkDifficulty == null) return NotFound();
            var walkDifficultyDto = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);
            return Ok(walkDifficultyDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {

            if (!ValidateAddwalkDifficultyAsync(addWalkDifficultyRequest))
            {
                return BadRequest(ModelState); 
            }

            // convert dto to domine object
            var WalkDifficultyDomine = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code,
            };
            // pass domine object to repository to persist

            WalkDifficultyDomine = await walkDifficultyRepository.AddAsync(WalkDifficultyDomine);

            // convert domine object back to dto

            var WalkDifficultyDto = new Models.DTO.WalkDifficulty()
            {
                Id = WalkDifficultyDomine.Id,
                Code = WalkDifficultyDomine.Code,
            };



            // send dto response back to client 

            return CreatedAtAction(nameof(GetWalkDifficultyAsync), new { id = WalkDifficultyDto.Id }, WalkDifficultyDto);


        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (!ValidateUpdatewalkDifficultyAsync(updateWalkDifficultyRequest))
            {
                return BadRequest(ModelState);
            }

            // requestdto to domine model
            var WalkDifficultyDomine = new Models.Domain.WalkDifficulty()
            {
                Code = updateWalkDifficultyRequest.Code,
            };
            // pass detailes to repository
            WalkDifficultyDomine = await walkDifficultyRepository.UpdateAsync(id, WalkDifficultyDomine);
            // convert data back to dto
            if (WalkDifficultyDomine == null)
            {
                return NotFound();
            }
            var WalkDifficultyDto = new Models.DTO.WalkDifficulty()
            {
                Id = WalkDifficultyDomine.Id,
                Code = WalkDifficultyDomine.Code,
            };
            return Ok(WalkDifficultyDto);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.DeleteAsync(id);
            if (walkDifficulty == null) return NotFound();
            var walkDifficultyDto = new Models.DTO.WalkDifficulty()
            {
                Id = walkDifficulty.Id,
                Code = walkDifficulty.Code,               
            };
            return Ok(walkDifficultyDto);
        }

        private bool ValidateAddwalkDifficultyAsync(Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            if (addWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest),
                    $"AddwalkDifficulty data is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyRequest.Code), $"{nameof(addWalkDifficultyRequest.Code)} Cannot be null or empty or whitespace");
            }            
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;

        }
        private bool ValidateUpdatewalkDifficultyAsync(Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            if (updateWalkDifficultyRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest),
                    $"updateWalkDifficultyRequest data is required.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequest.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequest.Code), $"{nameof(updateWalkDifficultyRequest.Code)} Cannot be null or empty or whitespace");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;

        }

    }
}
