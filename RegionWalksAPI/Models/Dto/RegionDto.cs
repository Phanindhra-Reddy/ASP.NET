using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RegionWalksAPI.Models.Domain;

namespace RegionWalksAPI.Models.Dto
{
    public class RegionDto
    {
        public Guid Id { get; set; }
        public string? Code {get; set; }
        public string? Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long {get; set; }
        public long Population { get; set; }
        //Navigation property
        //one region can have multiple walks
        public IEnumerable<Walk>? Walks { get; set; }
        
    }
}