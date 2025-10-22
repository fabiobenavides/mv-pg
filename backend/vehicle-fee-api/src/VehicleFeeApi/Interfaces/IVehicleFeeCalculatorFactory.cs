using VehicleFeeApi.Enums;

namespace VehicleFeeApi.Interfaces
{
  public interface IVehicleFeeCalculatorFactory
    {
        ICalculateFee CreateCalculator(VehicleType vehicleType);
    }
}