using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.DTOs.Assignments;

namespace demo_tt.Modules.Assignments.Queries.GetListAssignment
{
    public class GetListAssignmentResponse
    {
        public List<AssignmentDTOs> Assignments { get; set; } = new List<AssignmentDTOs>();
    }
}