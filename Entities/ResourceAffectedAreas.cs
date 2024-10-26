using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities.Common;

namespace demo_tt.Entities
{
    public class ResourceAffectedAreas : BaseEntity
    {
        public Guid AreaID { get; set; }
        public Guid ResourceID { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("AreaID")]
        public AffectedAreas AffectedAreas { get; set; }
        [ForeignKey("ResourceID")]
        public MasterResource MasterResource { get; set; }

    }
}