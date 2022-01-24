using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheTowerAPI.Models;
using TheTowerAPI.Services.DAL;

namespace TheTowerAPI.Controllers
{
    [Route("api/level")]
    [Authorize(Roles = "Admin")]
    [ApiController] 
    public class LevelController : ControllerBase
    {
        private readonly DbService _dbService;

        public LevelController(DbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("add/{name}")]
        public IActionResult AddLevel(string name)
        {
            _dbService.AddLevel(name);
            return Ok();
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetLevelsWithRecords()
        {
            return Ok(_dbService.GetLevelsWithRecords());
        }
    }
}