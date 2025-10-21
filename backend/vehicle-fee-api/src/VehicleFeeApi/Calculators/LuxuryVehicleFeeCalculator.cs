using VehicleFeeApi.Interfaces;
using VehicleFeeApi.Models;

namespace VehicleFeeApi.Calculators
{
    public class LuxuryVehicleFeeCalculator : IVehicleFeeCalculator
    {
        public FeeResult CalculateFees(CalculateFeesRequest request)
        {
            var buyerFee = request.BasePrice * 0.1m; // 10% buyer fee
            var sellerFee = request.BasePrice * 0.05m; // 5% seller fee
            var associationFee = 200; // fixed association fee
            var storageFee = 50; // fixed storage fee

            return new FeeResult
            {
                BuyerFee = buyerFee,
                SellerFee = sellerFee,
                AssociationFee = associationFee,
                StorageFee = storageFee
            };
        }
    }
}