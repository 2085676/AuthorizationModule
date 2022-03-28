using AuthorizationModule.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthorizationModule.Services
{
    public class TokenService : ITokenService
    {
        private readonly TokenAppSettings _appSettings;

        public TokenService(IOptions<TokenAppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateToken(string userEmail)
        {
            if (string.IsNullOrEmpty(userEmail))
                throw new Exception("Please enter correct email");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, userEmail),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(token);
        }
    }
}
