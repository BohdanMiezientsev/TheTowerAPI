using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheTowerAPI.DTOs;
using TheTowerAPI.Models;
using TheTowerAPI.Services;
using TheTowerAPI.Services.DAL;

namespace TheTowerAPI.Controllers
{
    [Route("api/user")]
    [Authorize]
    [ApiController] 
    public class UserController : ControllerBase
    {
        private readonly DbService _dbService;
        private readonly SHA256Hasher _hasher;
        private readonly JwtTokenManager _tokenManager;

        public UserController(DbService dbService, SHA256Hasher hasher, JwtTokenManager tokenManager)
        {
            _dbService = dbService;
            _hasher = hasher;
            _tokenManager = tokenManager;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_dbService.GetUsers());
        }
        
        [HttpGet("{nickname}")]
        public IActionResult GetUser(string nickname)
        {
            return Ok(_dbService.GetUser(nickname));
        }

        [HttpPost("email/{email}")]
        public IActionResult ChangeEmail(string email)
        {
            _dbService.ChangeUserEmail(User.FindFirst(ClaimTypes.Name).Value, email);
            return Ok("Email changed");
        }
        
        [HttpPost("email/{nickname}/{email}")]
        [Authorize(Roles = "Moderator")]
        public IActionResult ChangeUserEmail(string nickname, string email)
        {
            _dbService.ChangeUserEmail(nickname, email);
            return Ok("Email changed");
        }

        [HttpDelete("{nickname}")]
        [Authorize(Roles = "Moderator")]
        public IActionResult DeleteUser(string nickname)
        {
            _dbService.DeleteUser(nickname);
            return Ok();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult LoginUser(UserLoginRequest request)
        {
            int role = _dbService.VerifyUser(new User {Nickname = request.Nickname, Password = _hasher.Hash(request.Password)});
            if (role > 0)
                return Ok(new LoginResponse{Token = _tokenManager.GenerateJwtToken(request.Nickname, role)});
            return BadRequest("wrong credentials");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult RegisterUser(UserRegisterRequest request)
        {
            var user = new User{Nickname = request.Nickname, Email = request.Email, Password = _hasher.Hash(request.Password)};
            var verification = _dbService.VerifyUser(user);
            if (verification > 0 || verification == -1)
            {
                return BadRequest("such user already exists");
            }
            
            return Ok(_dbService.CreateUser(user));
        }
    }
}