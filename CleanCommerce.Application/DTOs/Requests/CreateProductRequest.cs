
using System.ComponentModel.DataAnnotations;

namespace CleanCommerce.Application.DTOs.Requests
{
    public class CreateProductRequest
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [MaxLength(1000)]
        public string? Description { get; set; }
        [Range(0.01,999999999)]
        public decimal Price { get; set; }
        [Range(0,int.MaxValue)]
        public int StockQuantity { get; set; }
        [Range(0, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}
