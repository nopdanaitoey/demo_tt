using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities.Common;

namespace demo_tt.Entities
{
    public class Trucks : BaseEntity
    {
        public string TruckID { get; set; }
        public ICollection<ResourceTrucks> ResourceTrucks { get; set; }
        public ICollection<Assignments> Assignments { get; set; }
        public ICollection<TravelTimeToAreas> TravelTimeToAreas { get; set; }

    }
}