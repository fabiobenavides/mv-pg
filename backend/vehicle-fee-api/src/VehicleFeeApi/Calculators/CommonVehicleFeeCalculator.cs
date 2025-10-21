using VehicleFeeApi.Interfaces;
using VehicleFeeApi.Models;

namespace VehicleFeeApi.Calculators
{
    public class CommonVehicleFeeCalculator : IVehicleFeeCalculator
    {
        public FeeResult CalculateFees(CalculateFeesRequest request)
        {
            var buyerFee = request.BasePrice * 0.05m; // 5% buyer fee
            var sellerFee = request.BasePrice * 0.03m; // 3% seller fee
            var associationFee = 50.00m; // fixed association fee
            var storageFee = request.BasePrice * 0.01m; // 1% storage fee

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