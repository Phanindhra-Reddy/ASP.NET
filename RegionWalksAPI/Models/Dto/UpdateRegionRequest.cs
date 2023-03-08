using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegionWalksAPI.Models.Dto
{
    public class UpdateRegionRequest
    {
        public string? Code {get; set; }
        public string? Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long {get; set; }
        public long Population { get; set; }
    }
}