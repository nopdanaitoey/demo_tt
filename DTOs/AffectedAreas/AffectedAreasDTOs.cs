using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_tt.DTOs.AffectedAreas
{
    public class AffectedAreasDTOs
    {
        public Guid Id { get; set; }
        public string AreaID { get; set; }
        public int UrgencyLevel { get; set; }
        public int TimeConstraint { get; set; }
        public List<ResourceAffectedAreasDTOs> ResourceAffectedAreas { get; set; }
    }
    public class ResourceAffectedAreasDTOs
    {


        public Guid ResourceID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}