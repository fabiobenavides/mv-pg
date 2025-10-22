using Microsoft.AspNetCore.Mvc;
using VehicleFeeApi.Models;
using VehicleFeeApi.Services;
using VehicleFeeApi.DTOs;
using VehicleFeeApi.Enums;
using Microsoft.AspNetCore.Http;
using System;

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
        [ProducesResponseType(typeof(FeeResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public ActionResult<FeeResultDto> CalculateFees([FromBody] CalculateFeesRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Validation Error",
                    Detail = "Request body cannot be null",
                    Status = StatusCodes.Status400BadRequest
                });
            }

            if (requestDto.BasePrice <= 0)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Validation Error",
                    Detail = "Base price must be greater than zero",
                    Status = StatusCodes.Status400BadRequest
                });
            }

            try
            {
                // Map DTO to domain model
                var request = new CalculateFeesRequest
                {
                    BasePrice = requestDto.BasePrice,
                    VehicleType = requestDto.VehicleType
                };

                var result = _vehicleFeeService.CalculateFees(request);

                // Map domain model to DTO
                var resultDto = new FeeResultDto
                {
                    BuyerFee = result.BuyerFee,
                    SellerFee = result.SellerFee,
                    AssociationFee = result.AssociationFee,
                    StorageFee = result.StorageFee
                };

                return Ok(resultDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Invalid Vehicle Type",
                    Detail = $"Valid vehicle types are: {string.Join(", ", Enum.GetNames<VehicleType>())}",
                    Status = StatusCodes.Status400BadRequest
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while processing your request",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
    }
}