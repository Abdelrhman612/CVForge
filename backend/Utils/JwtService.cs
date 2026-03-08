using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.DataBase.Models;
using backend.InterFaces;
using Microsoft.IdentityModel.Tokens;

namespace backend.Utils
{
    public class JwtService : IJwtService
    {
        private readonly Jwt _jwt;
        public JwtService(Jwt jwt)
        {
            _jwt = jwt;

        }
        public string GenerateToken(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwt.Key);
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(double.Parse(_jwt.LifeTime.ToString())),
                    Issuer = _jwt.Issuer,
                    Audience = _jwt.Audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);

            }
            catch (Exception ex)
            {
                // Handle exceptions related to token generation
                throw new InvalidOperationException("An error occurred while generating the JWT token.", ex);
            }




        }
    }
}