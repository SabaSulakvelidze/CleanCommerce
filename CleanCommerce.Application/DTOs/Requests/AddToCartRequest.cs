
using System.ComponentModel.DataAnnotations;

namespace CleanCommerce.Application.DTOs.Requests
{
    public class AddToCartRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
