
namespace CleanCommerce.Application.DTOs.Responses
{
    public class CartReposnse
    {
        public int UserId { get; set; }
        public List<CartItemResponse> Items { get; set; } = [];
        public decimal TotalAmount { get; set; }
    }
}
