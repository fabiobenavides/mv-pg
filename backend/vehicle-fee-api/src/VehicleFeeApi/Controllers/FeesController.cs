using Microsoft.AspNetCore.Mvc;
using VehicleFeeApi.Interfaces;
using VehicleFeeApi.Models;
using VehicleFeeApi.Services;

namespace VehicleFeeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeesController : ControllerBase
    {
        private readonly IVehicleFeeCalculator _vehicleFeeService;

        public FeesController(IVehicleFeeCalculator vehicleFeeService)
        {
            _vehicleFeeService = vehicleFeeService;
        }

        [HttpPost("calculate-fees")]
        public ActionResult<FeeResult> CalculateFees([FromBody] CalculateFeesRequest request)
        {
            if (request == null || request.BasePrice <= 0)
            {
                return BadRequest("Invalid request.");
            }

            var result = _vehicleFeeService.CalculateFees(request);
            return Ok(result);
        }
    }
}