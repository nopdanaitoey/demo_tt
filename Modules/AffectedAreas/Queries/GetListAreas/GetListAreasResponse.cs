using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.DTOs.AffectedAreas;

namespace demo_tt.Modules.AffectedAreas.Queries.GetListAreas
{
    public class GetListAreasResponse
    {
        public List<AffectedAreasDTOs> AffectedAreas { get; set; }
    }
}