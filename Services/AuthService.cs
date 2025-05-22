using ExecutionPca.Api.Dtos;
using ExecutionPca.Api.Models;
using ExecutionPca.Api.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExecutionPca.Api.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);

            if (user == null)
            {
                return new LoginResponseDto
                {
                    IsAuthenticated = false,
                    Message = "El usuario no existe."
                };
            }

            var decryptedPassword = ReversePassword(user.EncryptedPassword);
            var passwordMatches = decryptedPassword == request.Password;

            if (!passwordMatches)
            {
                return new LoginResponseDto
                {
                    IsAuthenticated = false,
                    Message = "La contraseña es incorrecta."
                };
            }

            var token = GenerateJwtToken(user.Username);

            return new LoginResponseDto
            {
                IsAuthenticated = true,
                Username = user.Username,
                Token = token,
                Message = "Autenticación exitosa"
            };
        }

        private string GenerateJwtToken(string username)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, username)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string ReversePassword(string encrypted)
        {
            if (string.IsNullOrEmpty(encrypted) || encrypted.Length < 5)
                return string.Empty;

            var trimmed = encrypted.Substring(2, encrypted.Length - 4);
            return new string(trimmed.Reverse().ToArray());
        }
    }
}
