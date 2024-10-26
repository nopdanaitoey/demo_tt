using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities.Common;

namespace demo_tt.Entities
{
    public class TravelTimeToAreas : BaseEntity
    {
        public Guid TruckID { get; set; }
        public Guid AreaID { get; set; }
        public int TravelTime { get; set; }
        [ForeignKey("TruckID")]
        public Trucks Trucks { get; set; }
        [ForeignKey("AreaID")]
        public AffectedAreas AffectedAreas { get; set; }
    }
}