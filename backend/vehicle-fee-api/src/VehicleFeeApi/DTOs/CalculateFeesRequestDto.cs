using System.ComponentModel.DataAnnotations;
using VehicleFeeApi.Enums;

namespace VehicleFeeApi.DTOs
{
    public class CalculateFeesRequestDto
    {
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Base price must be greater than zero.")]
        public decimal BasePrice { get; set; }

        [Required]
        [EnumDataType(typeof(VehicleType), ErrorMessage = "Invalid vehicle type.")]
        public VehicleType VehicleType { get; set; }
    }
}