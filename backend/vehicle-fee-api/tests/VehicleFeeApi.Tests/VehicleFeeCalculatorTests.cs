using VehicleFeeApi.Calculators;

namespace VehicleFeeApi.Tests
{
    public class VehicleFeeCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        private static object[] CommonTestData =
        {
            new object[] { 398m, 39.8m, 7.96m, 5m, 100m, 550.76m },
            new object[] { 501m, 50m, 10.02m, 10m, 100m, 671.02m },
            new object[] { 57m, 10m, 1.14m, 5m, 100m, 173.14m },
            new object[] { 1100m, 50m, 22m, 15m, 100m, 1287m },
            new object[] { 0m, 10m, 0m, 0m, 100m, 110m },
        };

        [TestCaseSource(nameof(CommonTestData))]
        public void Common_Should_Calculte_Correctly(decimal basePrice, decimal expectedBuyerFee, decimal expectedSellerFee,
            decimal expectedAssociationFee, decimal expectedStorageFee, decimal expectedTotal)
        {
            var commonFeeCalculator = new CommonVehicleFeeCalculator();
            var feeCalculator = new VehicleFeeCalculator(commonFeeCalculator, basePrice);
            
            var result = feeCalculator.CalculateFees();

            var resultTotal = basePrice
                + result.BuyerFee
                + result.SellerFee
                + result.AssociationFee
                + result.StorageFee;

            Assert.Multiple(() =>
            {
                Assert.That(result.BuyerFee, Is.EqualTo(expectedBuyerFee));
                Assert.That(result.SellerFee, Is.EqualTo(expectedSellerFee));
                Assert.That(result.AssociationFee, Is.EqualTo(expectedAssociationFee));
                Assert.That(result.StorageFee, Is.EqualTo(expectedStorageFee));
                Assert.That(resultTotal, Is.EqualTo(expectedTotal));
            });

        }

        private static object[] LuxuryTestData =
        {
            new object[] { 1800m, 180m, 72m, 15m, 100m, 2167m },
            new object[] { 1000000m, 200m, 40000m, 20m, 100m, 1040320m },
            new object[] { 0m, 25m, 0m, 0m, 100m, 125m },
        };

        [TestCaseSource(nameof(LuxuryTestData))]
        public void Luxury_Should_Calculte_Correctly(decimal basePrice, decimal expectedBuyerFee, decimal expectedSellerFee,
            decimal expectedAssociationFee, decimal expectedStorageFee, decimal expectedTotal)
        {
            var luxuryFeeCalculator = new LuxuryVehicleFeeCalculator();
            var feeCalculator = new VehicleFeeCalculator(luxuryFeeCalculator, basePrice);

            var result = feeCalculator.CalculateFees();

            var resultTotal = basePrice
                + result.BuyerFee
                + result.SellerFee
                + result.AssociationFee
                + result.StorageFee;

            Assert.Multiple(() =>
            {
                Assert.That(result.BuyerFee, Is.EqualTo(expectedBuyerFee));
                Assert.That(result.SellerFee, Is.EqualTo(expectedSellerFee));
                Assert.That(result.AssociationFee, Is.EqualTo(expectedAssociationFee));
                Assert.That(result.StorageFee, Is.EqualTo(expectedStorageFee));
                Assert.That(resultTotal, Is.EqualTo(expectedTotal));
            });

        }
    }
}
