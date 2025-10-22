using System;
using VehicleFeeApi.Calculators;
using VehicleFeeApi.Interfaces;

namespace VehicleFeeApi.Factories
{
    public class VehicleFeeCalculatorFactory : IVehicleFeeCalculatorFactory
    {
        private const string CommonVehicleType = "common";
        private const string LuxuryVehicleType = "luxury";

        public ICalculateFee CreateCalculator(string vehicleType)
        {
            return vehicleType.ToLower() switch
            {
                CommonVehicleType => new CommonVehicleFeeCalculator(),
                LuxuryVehicleType => new LuxuryVehicleFeeCalculator(),
                _ => throw new ArgumentException("Invalid vehicle type", nameof(vehicleType))
            };
        }
    }
}