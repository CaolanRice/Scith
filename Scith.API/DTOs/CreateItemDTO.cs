using System.ComponentModel.DataAnnotations;

namespace Scith.API.DTOs
{
    public record CreateItemDTO
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(0, 2000)]
        public double Price { get; init; }
    }
}