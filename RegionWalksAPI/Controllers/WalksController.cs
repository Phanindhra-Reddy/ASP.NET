using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegionWalksAPI.Controllers.Repositories;
using RegionWalksAPI.Models.Domain;
using RegionWalksAPI.Models.Dto;

namespace RegionWalksAPI.Controllers
{
    [ApiController]
    [Route("nzl-Walks")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;

        public WalksController(IWalkRepository walkRepository)
        {
            this.walkRepository = walkRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            var walks =  await walkRepository.GetAllAsync();

            var walksDto = new List<WalksDto>();

            walks.ToList().ForEach(walkI =>
           {
            var walkDto = new WalksDto()
            {
                Id = walkI.Id,
                Length = walkI.Length,
                Name = walkI.Name,
                RegionId = walkI.RegionId,
                WalkDifficultyId = walkI.WalkDifficultyId,
                Region = new Region()
                {
                    Id = walkI.Region.Id,
                    Code = walkI.Region.Code,
                    Name = walkI.Region.Name,
                    Area = walkI.Region.Area,
                    Lat = walkI.Region.Lat,
                    Long = walkI.Region.Long,
                    Population = walkI.Region.Population
                },
                WalkDifficulty = new WalkDifficulty()
                {
                    Id = walkI.WalkDifficulty.Id,
                    Code = walkI.WalkDifficulty.Code
                }
            };
            walksDto.Add(walkDto);
           });
           return Ok(walksDto);
        }
    
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync([FromRoute]Guid id)
        {
            var walk = await walkRepository.GetAsync(id);

            if(walk == null)
            {
                return NotFound();
            }

            var walksDto = new List<Models.Dto.WalksDto>();

            var walkDto = new Models.Dto.WalksDto()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
                Region = new Region()
                {
                    Id = walk.Region.Id,
                    Code = walk.Region.Code,
                    Name = walk.Region.Name,
                    Area = walk.Region.Area,
                    Lat = walk.Region.Lat,
                    Long = walk.Region.Long,
                    Population = walk.Region.Population
                },
                WalkDifficulty = new WalkDifficulty()
                {
                    Id = walk.WalkDifficulty.Id,
                    Code = walk.WalkDifficulty.Code
                }
            };
            walksDto.Add(walkDto);
            return Ok(walksDto);

        }
    
        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest) 
        {
            //convert dto object to repository
            var walk = new Walk()
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId

            };
            //pass domain object to repository
            walk = await walkRepository.AddAsync(walk);
            //convert domain to dto
            var walkDto = new Walk
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId,
            };
            //send dto to client
            return CreatedAtAction(nameof(GetWalkAsync),new{id = walkDto.Id},walkDto);
            //return Ok(walkDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id,[FromBody] UpdateWalkRequest updateWalkRequest)
        {
            //convert dto to domain object
            var walkDomain = new Walk
            {
                Length = updateWalkRequest.Length,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId
            };
            //pass details to repository
            walkDomain = await walkRepository.UpdateAsync(id,walkDomain);
            //handle null
            if(walkDomain == null)
            {
                return NotFound();
            }
            //convert back to dto
            var walkDTO = new Walk
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId
            };
            //return response
            return Ok(walkDTO);

        }
    
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //get walk from database
            var walk = await walkRepository.DeleteAsync(id);
            //if null notfound
            if(walk == null){
                return NotFound();
            }
            //convert response back to DTO
            var walksDto = new List<Models.Dto.WalksDto>();
            var walkDto = new WalksDto()
            {
                Id = walk.Id,
                Name = walk.Name,
                Length = walk.Length,
                RegionId = walk.RegionId,
                WalkDifficultyId = walk.WalkDifficultyId
            };
            walksDto.Add(walkDto);
            return Ok(walksDto);
        }
    }
}