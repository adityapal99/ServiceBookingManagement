using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AuthorizationMicroservice.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AuthorizationMicroservice.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public readonly IConfiguration _configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthTokenPayload GetAuthToken(AppUser user)
        {
            return new AuthTokenPayload
            {
                AccessToken = this.GenerateJwtToken(user.Id)
            };

        }

        private string GenerateJwtToken(int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
