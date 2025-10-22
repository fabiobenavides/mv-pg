using System;
using VehicleFeeApi.Calculators;
using VehicleFeeApi.Interfaces;
using VehicleFeeApi.Enums;

namespace VehicleFeeApi.Factories
{
    public class VehicleFeeCalculatorFactory : IVehicleFeeCalculatorFactory
    {
        public ICalculateFee CreateCalculator(VehicleType vehicleType)
        {
            return vehicleType switch
            {
                VehicleType.Common => new CommonVehicleFeeCalculator(),
                VehicleType.Luxury => new LuxuryVehicleFeeCalculator(),
                _ => throw new ArgumentException("Invalid vehicle type", nameof(vehicleType))
            };
        }
    }
}