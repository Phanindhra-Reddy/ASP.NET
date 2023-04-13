using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RegionWalksAPI.Controllers.Repositories;
using RegionWalksAPI.Models.Domain;
using RegionWalksAPI.Models.Dto;

namespace RegionWalksAPI.Controllers
{
    [ApiController]
    [Route("nzl-Regions")]
    public class RegionsController : Controller
    {
        public IRegionRepository regionRepository;
        public RegionsController(IRegionRepository regionRepository)
            {
            this.regionRepository = regionRepository;
                
            }
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            
            // var regions = new List<Region>()
            // {
            //     new Region
            //     {
            //         Id = Guid.NewGuid(),
            //         Name = "Wellington",
            //         Code = "WLG",
            //         Area = 225566,
            //         Lat = -1.8822,
            //         Long = 299.88,
            //         Population = 500000
            //     },
            //     new Region
            //     {
            //         Id = Guid.NewGuid(),
            //         Name = "AuckLand",
            //         Code = "AKL",
            //         Area = 225544,
            //         Lat = -1.5822,
            //         Long = 399.88,
            //         Population = 570000
            //     }
            // };
            // return Ok(regions);
            var regions =  await regionRepository.GetAllAsync();
        //    return Ok(regions);
           //return Dto regions
           var regionsDto = new List<Models.Dto.RegionDto>();
           regions.ToList().ForEach(region =>
           {
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
                // Walks = region.Walks
            };
            regionsDto.Add(regionDto);

           });
            return Ok(regionsDto);
        }
    
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);
            if(region == null){
                return NotFound();
            }
            var regionsDto = new List<Models.Dto.RegionDto>();
           
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
                Walks = region.Walks
            };
            regionsDto.Add(regionDto);
            return Ok(regionsDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            if(!validateAddRegionAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }
            
            //Request to Domain Model
            var region = new Region()
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };
            //Pass details to repository
            region = await regionRepository.AddAsync(region);
            //convert back to dto
            var regionDTO = new Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            return CreatedAtAction(nameof(GetRegionAsync),new{id = regionDTO.Id},regionDTO);
        }
        
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //get region from database
            var region = await regionRepository.DeleteAsync(id);
            //if null notfound
            if(region == null){
                return NotFound();
            }
            //convert response back to DTO
            var regionsDto = new List<Models.Dto.RegionDto>();
           
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
                Walks = region.Walks
            };
            regionsDto.Add(regionDto);
            //return ok response
             return Ok(regionsDto);
        }
    
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id,[FromBody] UpdateRegionRequest updateRegionRequest)
        {
            //convert DTO to Domain Model
            var region = new Region()
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };
            //Update Region using repository
             region = await regionRepository.UpdateAsync(id,region);
            //If null notFound
            if(id == null){
                return NotFound();
            }
            //Convert to DTO
            var regionDTO = new Region
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            //return OK response
            return Ok(regionDTO);

        }

        private bool validateAddRegionAsync(AddRegionRequest addRegionRequest)
        {
            if(addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest),
                $"Region Data is not sufficient");
                return false;
            }
            if(string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code),
                 $"{nameof(addRegionRequest.Code)} cannot be null oe empty or white space.");
            }
            if(string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name),
                 $"{nameof(addRegionRequest.Name)} cannot be null oe empty or white space.");
            }
            if(addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area),
                $"{nameof(addRegionRequest.Area)} cannot be less than or equal to zero!");
            }
            if(addRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Lat),
                $"{nameof(addRegionRequest.Lat)} cannot be less than or equal to zero!");
            }
            if(addRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Long),
                $"{nameof(addRegionRequest.Long)} cannot be less than or equal to zero!");
            }
            if(addRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population),
                $"{nameof(addRegionRequest.Population)} cannot be less than zero!");
            }
            if(ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        
    }
} 