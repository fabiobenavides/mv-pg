using VehicleFeeApi.Interfaces;

namespace VehicleFeeApi.Calculators
{
    public class LuxuryVehicleFeeCalculator : ICalculateFee
    {
        private const decimal BuyerFeePercentage = 0.10m;
        private const decimal BuyerMinimalFee = 25m;
        private const decimal BuyerMamimunFee = 200m;
        private const decimal SellerFeePercentage = 0.04m;
        public decimal CalculateBuyerFee(decimal basePrice)
        {
            var initialFee = basePrice * BuyerFeePercentage;
            var finalFee = initialFee < BuyerMinimalFee ? BuyerMinimalFee : initialFee > BuyerMamimunFee ? BuyerMamimunFee : initialFee;
            return finalFee;
        }

        public decimal CalculateSellerFee(decimal basePrice)
        {
            var fee = basePrice * SellerFeePercentage;
            return fee;
        }
    }
}