using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheTowerAPI.Models;
using TheTowerAPI.Services.DAL;

namespace TheTowerAPI.Controllers
{
    [Route("api/record")]
    [Authorize]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly DbService _dbService;

        public RecordController(DbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost("{level}/{time}")]

        public IActionResult AddRecord(string level, long time)
        {
            Record r = new Record
            {
                Nickname = User.FindFirst(ClaimTypes.Name).Value,
                LevelName = level,
                Time = time
            };
            return Ok(_dbService.AddRecord(r));
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetRecords()
        {
            return Ok(_dbService.GetRecords());
        }
    }
}