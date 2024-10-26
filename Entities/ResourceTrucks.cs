using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.Entities.Common;

namespace demo_tt.Entities
{
    public class ResourceTrucks : BaseEntity
    {
        public Guid ResourceID { get; set; }
        public Guid TruckID { get; set; }
        public int Quantity { get; set; }
        public int AvailableQuantity { get; set; }
        [ForeignKey("ResourceID")]
        public MasterResource MasterResource { get; set; }
        [ForeignKey("TruckID")]
        public Trucks Trucks { get; set; }
    }
}