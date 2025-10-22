
namespace VehicleFeeApi.Interfaces
{
    public interface IVehicleFeeCalculatorFactory
    {
        ICalculateFee CreateCalculator(string vehicleType);
    }
}