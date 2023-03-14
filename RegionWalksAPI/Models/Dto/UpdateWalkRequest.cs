using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegionWalksAPI.Models.Dto
{
    public class UpdateWalkRequest
    {
        public string? Name{ get; set; }
        public double Length { get; set; }
        public Guid RegionId{ get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}