using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_tt.DTOs.Trucks
{
    public class TrucksDTOs
    {
        public Guid Id { get; set; }
        public string TruckID { get; set; }
        public List<TrucksResourceDTOs> ResourceTrucks { get; set; }
        public List<TrucksTravelTimeToAreasDTOs> TravelTimeToAreas { get; set; }
    }
    public class TrucksResourceDTOs
    {
        public Guid ResourceID { get; set; }
        public string Name { get; set; }
        public int AvailableQuantity { get; set; }
    }
    public class TrucksTravelTimeToAreasDTOs
    {

        
        public string AreaID { get; set; }
        public int TravelTime { get; set; }
    }
}