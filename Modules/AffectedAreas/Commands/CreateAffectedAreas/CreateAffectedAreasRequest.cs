using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_tt.Modules.AffectedAreas.Commands.CreateAffectedAreas
{
    public class CreateAffectedAreasRequest : IRequest<CreateAffectedAreasResponse>
    {
        public string AreaID { get; set; }
        public int UrgencyLevel { get; set; }
        public int TimeConstraint { get; set; }
        public List<CreateAffectedAreasRequestRequiredResource> RequiredResources { get; set; }
    }



    public class CreateAffectedAreasRequestRequiredResource
    {
        public Guid ResourceID { get; set; }
        public int Quantity { get; set; }
    }
}