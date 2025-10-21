namespace VehicleFeeApi.Models
{
    public class FeeResult
    {
        public decimal BuyerFee { get; set; }
        public decimal SellerFee { get; set; }
        public decimal AssociationFee { get; set; }
        public decimal StorageFee { get; set; }
    }
}