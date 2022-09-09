using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDifficulty = await walkDifficultyRepository.GetAllAsync();

            // Convert Domain to DTOs
            var walkDifficultyDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);

            if (walkDifficulty == null)
            {
                return NotFound("Walk Difficulty not found");
            }

            // Convert Domain to DTOs
            var walkDifficultyDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] Models.DTO.AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            // Convert DTO to Domain object
            var walkDomainDiff = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficultyRequest.Code
            };

            // Pass domain object to Repository to persist this
            walkDomainDiff = await walkDifficultyRepository.AddAsync(walkDomainDiff);

            // Convert Domain object back to DTO
            var walkDiffDTO = new Models.DTO.WalkDifficulty
            {
                Id = walkDomainDiff.Id,
                Code = walkDomainDiff.Code
            };

            // Send DTO response back to Client
            return CreatedAtAction(nameof(GetWalkDifficultyById), new { id = walkDiffDTO.Id }, walkDiffDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id,
            [FromBody] Models.DTO.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            // Convert DTO to domain object
            var walkDiffDomain = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code

            };

            // Pass details to Repository - Get domain object in response ( or null)
            walkDiffDomain = await walkDifficultyRepository.UpdateAsync(id, walkDiffDomain);

            // Handle null (not found)
            if (walkDiffDomain == null)
            {
                return NotFound("This walk difficulty is not available");
            }

            // Convert back Domain to DTO
            var walkDiffDTO = new Models.DTO.WalkDifficulty
            {
                Id = walkDiffDomain.Id,
                Code = walkDiffDomain.Code
            };

            // Return Response
            return Ok(walkDiffDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            // Call Repository to delete walk
            var walkDiffDomain = await walkDifficultyRepository.DeleteAsync(id);

            if (walkDiffDomain == null)
            {
                return NotFound("Walk Difficulty not found");
            }

            var walkDiffDTO = mapper.Map<Models.DTO.WalkDifficulty>(walkDiffDomain);
            return Ok(walkDiffDTO);

        }
    }
}
