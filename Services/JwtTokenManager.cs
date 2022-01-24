using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TheTowerAPI.Services
{
    public class JwtTokenManager
    {
        private readonly string _secretKey = "my extremely secret secret key, no one can hack it";
        
        public JwtTokenManager(){}
        
        public string GenerateJwtToken(string nickname, int role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, nickname), new Claim(ClaimTypes.Role, role.ToString())}),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
        public string[] ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                string[] credentials = new[]
                {
                    jwtToken.Claims.First(x => x.Type == "unique_name").Value,
                    jwtToken.Claims.First(x => x.Type == "role").Value
                };

                // return account id from JWT token if validation successful
                return credentials;
            }
            catch(Exception ex)
            {
                throw ex;
                return null;
            }
        }
    }
}