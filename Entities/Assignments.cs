using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities.Common;

namespace demo_tt.Entities
{
    public class Assignments : BaseEntity
    {
        public Guid AreaID { get; set; }
        public Guid TruckID { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("AreaID")]
        public AffectedAreas AffectedAreas { get; set; }
        
        [ForeignKey("TruckID")]
        public Trucks Trucks { get; set; }
    }
}