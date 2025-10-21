using VehicleFeeApi.Interfaces;
using VehicleFeeApi.Models;
using VehicleFeeApi.Factories;

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
            var calculator = _factory.CreateCalculator(request.VehicleType);
            return calculator.CalculateFees(request);
        }
    }
}