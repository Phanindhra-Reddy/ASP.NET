using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegionWalksAPI.Models.Domain;
using RegionWalksAPI.Models.Dto;

namespace RegionWalksAPI.Controllers.Repositories
{
    [ApiController]
    [Route("nzl-WalkDifficulty")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficultiesAsync()
        {
            // var walkDifficulty = await walkDifficultyRepository.GetAllAsync();
            // var walkDifficultiesDto = new List<Models.Dto.WalkDifficultyDto>();
            // walkDifficulty.ToList().ForEach(walkD =>
            // {
            //     var walkDifficultyDto = new WalkDifficultyDto()
            //     {
            //         Id = walkD.Id,
            //         Code = walkD.Code
            //     };
            //     walkDifficultiesDto.Add(walkDifficultyDto);
            // });
            // return Ok(walkDifficultiesDto);
            return Ok(await walkDifficultyRepository.GetAllAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyAsync")]
        public async Task<IActionResult> GetWalkDifficultyAsync([FromRoute]Guid id)
        {
            var walkDifficulty = await walkDifficultyRepository.GetAsync(id);
            if(walkDifficulty == null){
                return NotFound();
            }
             var walkDifficultiesDto = new List<Models.Dto.WalkDifficultyDto>();
             var walkDifficultyDto = new WalkDifficultyDto()
                {
                    Id = walkDifficulty.Id,
                    Code = walkDifficulty.Code
                };
                walkDifficultiesDto.Add(walkDifficultyDto);
            return Ok(walkDifficultiesDto);
        }
    
        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync([FromBody] AddWalkDifficultyRequest addWalkDifficultyRequest)
        {
            var walkdifficulty = new WalkDifficulty()
            {
                Code = addWalkDifficultyRequest.Code
            };
            walkdifficulty = await walkDifficultyRepository.AddAsync(walkdifficulty);
            var walkdifficultyDto = new WalkDifficulty
            {
                Id = walkdifficulty.Id,
                Code = walkdifficulty.Code
            };
            return CreatedAtAction(nameof(GetWalkDifficultyAsync),new{id = walkdifficultyDto.Id},walkdifficultyDto);

        }
    
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync([FromRoute] Guid id,[FromBody] UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            var walkDifficultyDom = new WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };
            walkDifficultyDom = await walkDifficultyRepository.UpdateAsync(id,walkDifficultyDom);
            if(walkDifficultyDom == null)
            {
                return NotFound();
            }
            var walkdifficultyDto = new WalkDifficulty
            {
                Id = walkDifficultyDom.Id,
                Code = walkDifficultyDom.Code
            };
            return Ok(walkdifficultyDto);
        }
    
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            var walkdifficult = await walkDifficultyRepository.DeleteAsync(id);
            //if null notfound
            if(walkdifficult == null){
                return NotFound();
            }
            //convert response back to DTO
            var walksDifficultDto = new List<Models.Dto.WalkDifficultyDto>();
            var walkDifficultDto = new WalkDifficultyDto()
            {
                Id = walkdifficult.Id,
                Code = walkdifficult.Code
            };
            walksDifficultDto.Add(walkDifficultDto);
            return Ok(walksDifficultDto);
        }

    }
}