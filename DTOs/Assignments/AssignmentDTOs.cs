using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_tt.DTOs.Assignments
{
    public class AssignmentDTOs
    {
        public string AreaID { get; set; }
        public string TruckID { get; set; }
        public List<AssignmentResourceDTOs> ResourcesDelivered { get; set; }
    }


    public class AssignmentResourceDTOs
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}