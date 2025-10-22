using VehicleFeeApi.Calculators;
using VehicleFeeApi.Factories;
using VehicleFeeApi.Interfaces;
using VehicleFeeApi.Models;

namespace VehicleFeeApi.Services
{
    public class VehicleFeeService
    {
        private readonly VehicleFeeCalculatorFactory _factory;

        public VehicleFeeService(VehicleFeeCalculatorFactory factory)
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