using VehicleFeeApi.Interfaces;
using VehicleFeeApi.Models;

namespace VehicleFeeApi.Calculators
{
    public class VehicleFeeCalculator : IVehicleFeeCalculator
    {
        private readonly ICalculateFee _feeCalculator;
        private readonly decimal _basePrice;

        public VehicleFeeCalculator(ICalculateFee buyerFeeCalculator, decimal basePrice)
        {
            _feeCalculator = buyerFeeCalculator;
            _basePrice = basePrice;
        }

        public FeeResult CalculateFees()
        {
            return new FeeResult
            {
                BuyerFee = _feeCalculator.CalculateBuyerFee(_basePrice),
                SellerFee = _feeCalculator.CalculateSellerFee(_basePrice),
                AssociationFee = CalculateAssociationFee(),
                StorageFee = CalculateStorageFee()
            };
        }

        private decimal CalculateAssociationFee()
        {
            if (_basePrice < 1) {
                return 0m;
            }
            if (_basePrice <= 500m) {
                return 5m;
            }
            if (_basePrice <= 1000m) {
                return 10m;
            }
            if (_basePrice <= 3000m) {
                return 15m;
            }
            return 20m;
        }

        private decimal CalculateStorageFee()
        {
            return 100m;
        }
    }
}