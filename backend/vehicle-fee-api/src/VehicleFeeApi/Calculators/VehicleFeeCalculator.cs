using VehicleFeeApi.Interfaces;
using VehicleFeeApi.Models;

namespace VehicleFeeApi.Calculators
{
    public class VehicleFeeCalculator : IVehicleFeeCalculator
    {
        private const decimal MINIMUN_BASE_PRICE = 1m;
        private const decimal LIMIT_500_BASE_PRICE = 500m;
        private const decimal ZERO_FEE = 0m;
        private const decimal LIMIT_500_FEE = 5m;
        private const decimal LIMIT_1000_BASE_PRICE = 1000m;
        private const decimal LIMIT_1000_FEE = 10m;
        private const decimal LIMIT_3000_BASE_PRICE = 3000m;
        private const decimal LIMIT_3000_FEE = 15m;
        private const decimal MAXIMUN_FEE = 20m;
        private const decimal STORAGE_FEE = 100m;
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
            if (_basePrice < MINIMUN_BASE_PRICE) {
                return ZERO_FEE;
            }
            if (_basePrice <= LIMIT_500_BASE_PRICE) {
                return LIMIT_500_FEE;
            }
            if (_basePrice <= LIMIT_1000_BASE_PRICE) {
                return LIMIT_1000_FEE;
            }
            if (_basePrice <= LIMIT_3000_BASE_PRICE) {
                return LIMIT_3000_FEE;
            }
            return MAXIMUN_FEE;
        }

        private decimal CalculateStorageFee()
        {
            return STORAGE_FEE;
        }
    }
}