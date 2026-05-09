
using System.ComponentModel.DataAnnotations;

namespace CleanCommerce.Application.DTOs.Requests
{
    public class UpdateCartItemRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
