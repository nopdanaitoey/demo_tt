using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_tt.DTOs.Trucks;

namespace demo_tt.Modules.Trucks.Queries.GetListTrucks
{
    public class GetListTrucksResponse
    {
        public List<TrucksDTOs> Trucks { get; set; } = new List<TrucksDTOs>();
    }
}