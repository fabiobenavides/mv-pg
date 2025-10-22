using Microsoft.AspNetCore.Mvc;
using VehicleFeeApi.Models;
using VehicleFeeApi.Services;
using VehicleFeeApi.DTOs;

namespace VehicleFeeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeesController : ControllerBase
    {
        private readonly VehicleFeeService _vehicleFeeService;

        public FeesController(VehicleFeeService vehicleFeeService)
        {
            _vehicleFeeService = vehicleFeeService;
        }

        [HttpPost("calculate-fees")]
        public ActionResult<FeeResultDto> CalculateFees([FromBody] CalculateFeesRequest request)
        {
            if (request == null || request.BasePrice <= 0)
            {
                return BadRequest("Invalid request.");
            }

            var result = _vehicleFeeService.CalculateFees(request);

            // From model to DTO mapping
            var resultDto = new FeeResultDto
            {
                BuyerFee = result.BuyerFee,
                SellerFee = result.SellerFee,
                AssociationFee = result.AssociationFee,
                StorageFee = result.StorageFee
            };

            return Ok(resultDto);
        }
    }
}