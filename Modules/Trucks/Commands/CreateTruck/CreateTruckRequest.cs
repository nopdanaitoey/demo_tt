

namespace demo_tt.Modules.Trucks.Commands.CreateTruck
{
    public class CreateTruckRequest : IRequest<CreateTruckResponse>
    {
        public string TruckID { get; set; }
        public List<CreateTruckRequestResourceTruck> Resources { get; set; }
        public List<CreateTruckRequestTravelTimeToAreas> TimeTruckToAreas { get; set; }
    }

    public class CreateTruckRequestResourceTruck
    {
        public Guid ResourceID { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateTruckRequestTravelTimeToAreas
    {
        public Guid AreaID { get; set; }
        public int TravelTime { get; set; }
    }
}