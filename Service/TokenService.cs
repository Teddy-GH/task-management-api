using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using task_management_system.Interfaces;
using task_management_system.Models;

namespace task_management_system.Service
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;

        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:SigningKey"]));
            _userManager = userManager;
        }

        public async Task<string> CreateToken(User user)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName ?? string.Empty),
        };

          // role added to token
           claims.Add(new Claim(ClaimTypes.Role, (user.Role != null ? user.Role.ToString() : "User")));
          


            var signingCreds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = signingCreds,
                Issuer = _config["Token:Issuer"],
                Audience = _config["Token:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDesc);

            return tokenHandler.WriteToken(token);
        }
    }

}
