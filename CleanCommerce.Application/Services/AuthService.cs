using AutoMapper;
using CleanCommerce.Application.DTOs.Requests;
using CleanCommerce.Application.DTOs.Responses;
using CleanCommerce.Application.Interfaces.Repositories;
using CleanCommerce.Application.Interfaces.Security;
using CleanCommerce.Application.Interfaces.Services;
using CleanCommerce.Domain.Entities;

namespace CleanCommerce.Application.Services
{
    public class AuthService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        IMapper mapper
        ) : IAuthService
    {
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await userRepository.GetByUsernameOrEmailAsync(request.UsernameOrEmail);
            if (user is null)
                throw new Exception("Invalid credentials.");

            var isPasswordValid = passwordHasher.VerifyPassword(request.Password,user.PasswordHash);

            if(!isPasswordValid)
                throw new Exception("Invalid credentials.");

            var roles = user.UserRoles
                .Select(ur => ur.Role.Name)
                .ToList();

            var token = jwtTokenService.GenerateToken(user, roles);

            return new AuthResponse
            {
                UserId = user.Id,
                UserName = user.Username,
                Email = user.Email,
                Roles = roles,
                Token = token
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterUserRequest request)
        {
            var existingUserByUsername = await userRepository.GetByUsernameAsync(request.Username);
            if (existingUserByUsername is not null)
                throw new Exception("Username already exists.");

            var existingUserByEmail = await userRepository.GetByEmailAsync(request.Email);
            if (existingUserByEmail is not null)
                throw new Exception("Email already exists.");

            var customerRole = await roleRepository.GetByNameAsync("Customer");
            if (customerRole is null)
                throw new Exception("Default role 'Customer' was not found.");

            var user = mapper.Map<User>(request);

            user.PasswordHash = passwordHasher.HashPassword(request.Password);
            user.CreatedAt = DateTime.Now;

            var createdUser = await userRepository.AddAsync(user);

            await userRoleRepository.TaskAsync(new UserRole
            {
                UserId = createdUser.Id,
                RoleId = customerRole.Id
            });

            var roles = await roleRepository.GetRoleNamesByUserIdAsync(createdUser.Id);

            var token = jwtTokenService.GenerateToken(createdUser, roles);

            return new AuthResponse
            { 
                UserId = createdUser.Id,
                UserName = createdUser.Username,
                Email = createdUser.Email,
                Roles = roles,
                Token = token
            };
        }
    }
}
