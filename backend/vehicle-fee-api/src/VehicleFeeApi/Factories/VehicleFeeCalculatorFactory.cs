using System;
using VehicleFeeApi.Calculators;
using VehicleFeeApi.Interfaces;

namespace VehicleFeeApi.Factories
{
    public class VehicleFeeCalculatorFactory
    {
        public ICalculateFee CreateCalculator(string vehicleType)
        {
            return vehicleType.ToLower() switch
            {
                "common" => new CommonVehicleFeeCalculator(),
                "luxury" => new LuxuryVehicleFeeCalculator(),
                _ => throw new ArgumentException("Invalid vehicle type", nameof(vehicleType))
            };
        }
    }
}