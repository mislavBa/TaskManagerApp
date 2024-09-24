using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogController : ControllerBase
    {
        private readonly TaskManagerContext _context;

        public LogController(TaskManagerContext context)
        {
            _context = context;
        }

        [HttpGet("[action]{n}")]
        public ActionResult<IEnumerable<LogDto>> Get(int n = 10)
        {
            try
            {
                var logs = _context.Logs
                    .OrderByDescending(x => x.Timestamp)
                    .Take(n)
                    .ToList();

                var result = logs.Select(x => new LogDto
                {
                    Id = x.Id,
                    Timestamp = x.Timestamp,
                    Level = x.Level,
                    Message = x.Message,
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public int Count()
        {
            return _context.Logs.Count();
        }
    }
}
