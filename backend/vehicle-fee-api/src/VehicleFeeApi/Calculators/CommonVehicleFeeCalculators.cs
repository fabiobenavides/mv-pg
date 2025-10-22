using VehicleFeeApi.Interfaces;

namespace VehicleFeeApi.Calculators
{
    public class CommonVehicleFeeCalculator : ICalculateFee
    {
        public decimal CalculateBuyerFee(decimal basePrice)
        {
            var initialFee = basePrice * 0.10m;
            var finalFee = initialFee < 10 ? 10 : initialFee > 50 ? 50 : initialFee;
            return finalFee;
            
        }

        public decimal CalculateSellerFee(decimal basePrice)
        {
            var fee = basePrice * 0.02m;
            return fee;
        }
    }
}