using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.DTOs.Responses;

namespace CleanCommerce.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterUserRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
