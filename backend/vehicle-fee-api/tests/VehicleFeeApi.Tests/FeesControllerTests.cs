using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using VehicleFeeApi.Controllers;
using VehicleFeeApi.Interfaces;
using VehicleFeeApi.Models;
using Xunit;

namespace VehicleFeeApi.Tests
{
    public class FeesControllerTests
    {
        private readonly FeesController _controller;
        private readonly Mock<IVehicleFeeCalculator> _mockCalculator;

        public FeesControllerTests()
        {
            _mockCalculator = new Mock<IVehicleFeeCalculator>();
            _controller = new FeesController(_mockCalculator.Object);
        }

        [Fact]
        public void CalculateFees_ReturnsCorrectFees_ForCommonVehicle()
        {
            // Arrangeen
            var request = new CalculateFeesRequest { BasePrice = 10000, VehicleType = "Common" };
            var expectedResult = new FeeResult
            {
                BuyerFee = 100,
                SellerFee = 150,
                AssociationFee = 50,
                StorageFee = 20
            };
            _mockCalculator.Setup(c => c.CalculateFees(request)).Returns(expectedResult);

            // Act
            var result = _controller.CalculateFees(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var feeResult = Assert.IsType<FeeResult>(okResult.Value);
            Assert.Equal(expectedResult.BuyerFee, feeResult.BuyerFee);
            Assert.Equal(expectedResult.SellerFee, feeResult.SellerFee);
            Assert.Equal(expectedResult.AssociationFee, feeResult.AssociationFee);
            Assert.Equal(expectedResult.StorageFee, feeResult.StorageFee);
        }

        [Fact]
        public void CalculateFees_ReturnsCorrectFees_ForLuxuryVehicle()
        {
            // Arrange
            var request = new CalculateFeesRequest { BasePrice = 20000, VehicleType = "Luxury" };
            var expectedResult = new FeeResult
            {
                BuyerFee = 200,
                SellerFee = 300,
                AssociationFee = 100,
                StorageFee = 40
            };
            _mockCalculator.Setup(c => c.CalculateFees(request)).Returns(expectedResult);

            // Act
            var result = _controller.CalculateFees(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var feeResult = Assert.IsType<FeeResult>(okResult.Value);
            Assert.Equal(expectedResult.BuyerFee, feeResult.BuyerFee);
            Assert.Equal(expectedResult.SellerFee, feeResult.SellerFee);
            Assert.Equal(expectedResult.AssociationFee, feeResult.AssociationFee);
            Assert.Equal(expectedResult.StorageFee, feeResult.StorageFee);
        }

        [Fact]
        public void CalculateFees_ReturnsBadRequest_ForInvalidVehicleType()
        {
            // Arrange
            var request = new CalculateFeesRequest { BasePrice = 10000, VehicleType = "InvalidType" };

            // Act
            var result = _controller.CalculateFees(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid vehicle type.", badRequestResult.Value);
        }
    }
}