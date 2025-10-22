namespace VehicleFeeApi.DTOs
{
    public class FeeResultDto
    {
        public decimal BuyerFee { get; set; }
        public decimal SellerFee { get; set; }
        public decimal AssociationFee { get; set; }
        public decimal StorageFee { get; set; }
        public decimal TotalFees => BuyerFee + SellerFee + AssociationFee + StorageFee;
    }
}