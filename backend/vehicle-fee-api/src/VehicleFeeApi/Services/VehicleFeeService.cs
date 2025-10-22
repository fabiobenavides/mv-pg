using VehicleFeeApi.Calculators;
using VehicleFeeApi.Interfaces;
using VehicleFeeApi.Models;

namespace VehicleFeeApi.Services
{
    public class VehicleFeeService
    {
      private readonly IVehicleFeeCalculatorFactory _factory;

        public VehicleFeeService(IVehicleFeeCalculatorFactory factory)
 {
            _factory = factory;
 }

      public FeeResult CalculateFees(CalculateFeesRequest request)
        {
            var feeCalculator = _factory.CreateCalculator(request.VehicleType);
            var calculator = new VehicleFeeCalculator(feeCalculator, request.BasePrice);
            return calculator.CalculateFees();
        }
    }
}