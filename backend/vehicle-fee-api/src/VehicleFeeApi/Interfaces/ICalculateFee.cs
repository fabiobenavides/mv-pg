namespace VehicleFeeApi.Interfaces
{
    public interface ICalculateFee
    {
        decimal CalculateBuyerFee(decimal basePrice);
        decimal CalculateSellerFee(decimal basePrice);
    }
}