using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualBasic.CompilerServices;
using TheTowerAPI.Models;
using TheTowerAPI.Services;
using TheTowerAPI.Services.DAL;

namespace TheTowerAPI.Handlers
{
    public class CustomAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly JwtTokenManager _tokenManager;
        
        public CustomAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, JwtTokenManager tokenManager) : base(options, logger, encoder, clock)
        {
            _tokenManager = tokenManager;

        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing authorization header");

            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var credentials = _tokenManager.ValidateJwtToken(token);

            if (credentials == null)
                return AuthenticateResult.Fail("corrupted token");

            User temp = new User
            {
                Nickname = credentials[0],
                Role = IntegerType.FromString(credentials[1])
            };
            
            
            Claim[] claims;

            switch (temp.Role)
            {
                case 1:
                    claims = new[]
                    {
                        new Claim(ClaimTypes.Name, temp.Nickname),
                        new Claim(ClaimTypes.Role, "User")
                    };
                    break;
                case 2:
                    claims = new[]
                    {
                        new Claim(ClaimTypes.Name, temp.Nickname),
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim(ClaimTypes.Role, "Moderator")
                    };
                    break;
                case 3:
                    claims = new[]
                    {
                        new Claim(ClaimTypes.Name, temp.Nickname),
                        new Claim(ClaimTypes.Role, "User"),
                        new Claim(ClaimTypes.Role, "Moderator"),
                        new Claim(ClaimTypes.Role, "Admin"),
                    };
                    break;
                default:
                {
                    return AuthenticateResult.Fail("something went terribly wrong");
                }
            }

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            
            return AuthenticateResult.Success(ticket);
        }
    }
}