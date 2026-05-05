using System.ComponentModel.DataAnnotations;

namespace CleanCommerce.Application.DTOs.Requests
{
    public class CreateCategoryRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}
