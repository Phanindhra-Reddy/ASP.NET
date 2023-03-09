using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegionWalksAPI.Models.Domain;

namespace RegionWalksAPI.Models.Dto
{
    public class WalksDto
    {
        public Guid Id { get; set; }
        public string? Name{ get; set; }
        public double Length { get; set; }
        public Guid RegionId{ get; set; }
        public Guid WalkDifficultyId { get; set; }

        //Navigation properties
        public Region? Region { get; set; }
        public WalkDifficulty? WalkDifficulty { get; set; }
    }
}