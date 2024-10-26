using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities.Common;

namespace demo_tt.Entities
{
    public class AffectedAreas : BaseEntity
    {
        public string AreaID { get; set; }
        public int UrgencyLevel { get; set; }
        public int TimeConstraint { get; set; }
        public ICollection<ResourceAffectedAreas> ResourceAffectedAreas { get; set; }
        public ICollection<Assignments> Assignments { get; set; }
        public ICollection<TravelTimeToAreas> TravelTimeToAreas { get; set; }
    }
}