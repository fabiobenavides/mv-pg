using VehicleFeeApi.Calculators;
using VehicleFeeApi.Interfaces;
using VehicleFeeApi.Models;
using VehicleFeeApi.Enums;
using System;

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
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.BasePrice <= 0)
            {
                throw new ArgumentException("Base price must be greater than zero", nameof(request.BasePrice));
            }

            if (!Enum.IsDefined(typeof(VehicleType), request.VehicleType))
            {
                throw new ArgumentException("Invalid vehicle type", nameof(request.VehicleType));
            }

            var feeCalculator = _factory.CreateCalculator(request.VehicleType);
            var calculator = new VehicleFeeCalculator(feeCalculator, request.BasePrice);
            return calculator.CalculateFees();
        }
    }
}