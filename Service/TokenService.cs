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

        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:SigningKey"]));
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.Email, user.Email),
               new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
           };
          
            var signingCreds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = signingCreds,
                Issuer  = _config["Token:Issuer"],
                Audience = _config["Token:Audience"]

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDesc);


            return tokenHandler.WriteToken(token);

        }
    }
}
