using VehicleFeeApi.Models;

namespace VehicleFeeApi.Interfaces
{
    public interface IVehicleFeeCalculator
    {
        FeeResult CalculateFees();
    }
}