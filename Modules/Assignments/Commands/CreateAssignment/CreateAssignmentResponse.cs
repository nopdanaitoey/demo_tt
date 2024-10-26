using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.DTOs.Assignments;
using demo_tt.Responses;

namespace demo_tt.Modules.Assignments.Commands.CreateAssignment
{
    public class CreateAssignmentResponse : BaseStatusResponse
    {
        public List<AssignmentDTOs> Assignments { get; set; } = new List<AssignmentDTOs>();
    }
   
}