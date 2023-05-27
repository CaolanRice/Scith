using System.ComponentModel.DataAnnotations;

namespace Scith.API.DTOs
{
    public record UpdateItemDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(0, 2500)]
        public double Price { get; init; }
    }
}