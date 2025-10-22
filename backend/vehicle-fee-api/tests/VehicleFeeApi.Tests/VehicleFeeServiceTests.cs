using Moq;
using VehicleFeeApi.Enums;
using VehicleFeeApi.Interfaces;
using VehicleFeeApi.Models;
using VehicleFeeApi.Services;

namespace VehicleFeeApi.Tests
{
  public class VehicleFeeServiceTests
    {
        private Mock<IVehicleFeeCalculatorFactory> _mockFactory;
        private VehicleFeeService _service;
        private Mock<ICalculateFee> _mockCalculator;

        [SetUp]
        public void Setup()
        {
            _mockFactory = new Mock<IVehicleFeeCalculatorFactory>();
            _mockCalculator = new Mock<ICalculateFee>();
            _service = new VehicleFeeService(_mockFactory.Object);
        }

        [Test]
        public void CalculateFees_WithCommonVehicle_ReturnsExpectedFees()
        {
            // Arrange
             var request = new CalculateFeesRequest
             {
                BasePrice = 10000m,
                VehicleType = VehicleType.Common
             };

            var expectedResult = new FeeResult
            {
                BuyerFee = 50m,
                SellerFee = 200m,
                AssociationFee = 20m,
                StorageFee = 100m
            };

            _mockCalculator.Setup(c => c.CalculateBuyerFee(request.BasePrice))
                .Returns(expectedResult.BuyerFee);
           _mockCalculator.Setup(c => c.CalculateSellerFee(request.BasePrice))
                .Returns(expectedResult.SellerFee);

          _mockFactory.Setup(f => f.CreateCalculator(request.VehicleType))
                .Returns(_mockCalculator.Object);

           // Act
             var result = _service.CalculateFees(request);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.BuyerFee, Is.EqualTo(expectedResult.BuyerFee));
                Assert.That(result.SellerFee, Is.EqualTo(expectedResult.SellerFee));
                Assert.That(result.AssociationFee, Is.EqualTo(expectedResult.AssociationFee));
                Assert.That(result.StorageFee, Is.EqualTo(expectedResult.StorageFee));
            });
            _mockFactory.Verify(f => f.CreateCalculator(request.VehicleType), Times.Once);
        }

        [Test]
        public void CalculateFees_WithLuxuryVehicle_ReturnsExpectedFees()
        {
            // Arrange
            var request = new CalculateFeesRequest
            {
                BasePrice = 50000m,
                VehicleType = VehicleType.Luxury
            };

            var expectedResult = new FeeResult
            {
                BuyerFee = 3000m,
                SellerFee = 2000m,
                AssociationFee = 20m,
                StorageFee = 100m
            };

            _mockCalculator.Setup(c => c.CalculateBuyerFee(request.BasePrice))
                .Returns(expectedResult.BuyerFee);
            _mockCalculator.Setup(c => c.CalculateSellerFee(request.BasePrice))
                .Returns(expectedResult.SellerFee);

            _mockFactory.Setup(f => f.CreateCalculator(request.VehicleType))
                .Returns(_mockCalculator.Object);

            // Act
            var result = _service.CalculateFees(request);

            // Assert
            Assert.Multiple(() =>
{
                Assert.That(result.BuyerFee, Is.EqualTo(expectedResult.BuyerFee));
                Assert.That(result.SellerFee, Is.EqualTo(expectedResult.SellerFee));
                Assert.That(result.AssociationFee, Is.EqualTo(expectedResult.AssociationFee));
                Assert.That(result.StorageFee, Is.EqualTo(expectedResult.StorageFee));
            });
            _mockFactory.Verify(f => f.CreateCalculator(request.VehicleType), Times.Once);
        }

        [TestCase(-1)]
        [TestCase(99)]
        public void CalculateFees_WithInvalidEnumValue_ThrowsArgumentException(int invalidEnumValue)
  {
            // Arrange
            var request = new CalculateFeesRequest
            {
                BasePrice = 10000m,
                VehicleType = (VehicleType)invalidEnumValue // invalid enum value
            };

            _mockFactory.Setup(f => f.CreateCalculator(request.VehicleType))
                .Throws<ArgumentException>();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _service.CalculateFees(request));
            Assert.That(ex.Message, Does.Contain("Invalid vehicle type"));
        }

        [Test]
        public void CalculateFees_WithNullRequest_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.CalculateFees(null));
        }

        [Test]
        public void CalculateFees_WithZeroBasePrice_ThrowsArgumentException()
        {
            // Arrange
            var request = new CalculateFeesRequest
            {
                BasePrice = 0m,
                VehicleType = VehicleType.Common
            };

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _service.CalculateFees(request));
            Assert.That(ex.Message, Does.Contain("Base price must be greater than zero"));
        }

        [Test]
        public void CalculateFees_WithNegativeBasePrice_ThrowsArgumentException()
        {
            // Arrange
            var request = new CalculateFeesRequest
            {
                BasePrice = -100m,
                VehicleType = VehicleType.Common
            };

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _service.CalculateFees(request));
            Assert.That(ex.Message, Does.Contain("Base price must be greater than zero"));
        }
    }
}