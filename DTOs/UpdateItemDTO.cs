using System.ComponentModel.DataAnnotations;

namespace Scith.DTOs
{
    public record UpdateItemDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(0, 2000)]
        public double Price { get; init; }
    }
}