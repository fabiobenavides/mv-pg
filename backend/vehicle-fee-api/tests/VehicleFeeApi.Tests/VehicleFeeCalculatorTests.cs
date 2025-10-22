using VehicleFeeApi.Calculators;
using VehicleFeeApi.Interfaces;

namespace VehicleFeeApil.Tests
{
    public class VehicleFeeCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]

        public void Calculate_Test()
        {
            var commonFeeCalculator = new CommonVehicleFeeCalculator();
            var basePrice = 398.00m;
            var feeCalculator = new VehicleFeeCalculator(commonFeeCalculator, basePrice);
            
            var result = feeCalculator.CalculateFees();

            Assert.AreEqual(result.BuyerFee, 39.80m);
            Assert.AreEqual(result.SellerFee, 7.96m);
            Assert.AreEqual(result.AssociationFee, 5.00m);
            Assert.AreEqual(result.StorageFee, 100.00m);

            var resultTotal = basePrice 
                + result.BuyerFee 
                + result.SellerFee 
                + result.AssociationFee 
                + result.StorageFee;
            Assert.AreEqual(550.76, resultTotal);


        }
    }
}
