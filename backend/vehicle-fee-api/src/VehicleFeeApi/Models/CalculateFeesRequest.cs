using VehicleFeeApi.Enums;

namespace VehicleFeeApi.Models
{
    public class CalculateFeesRequest
    {
        public decimal BasePrice { get; set; }
        public VehicleType VehicleType { get; set; }
    }
}