using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using WebAPI.DTOs;
using WebAPI.Models;
using Task = WebAPI.Models.Task;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskManagerContext _context;

        public TaskController(TaskManagerContext context)
        {
            _context = context;
        }

        // GET: api/<TaskController>
        [HttpGet("[action]")]
        public ActionResult<IEnumerable<TaskDto>> Get()
        {
            try
            {
                var result = _context.Tasks;
                var map = result.Select(x => new TaskDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt,
                    DueDate = x.DueDate,
                    ManagerId = x.ManagerId,
                });
                return Ok(map);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<TaskController>/5
        [HttpGet("[action]{id}")]
        public ActionResult<TaskDto> Get(int id)
        {
            try
            {
                var result = _context.Tasks.FirstOrDefault(x => x.Id == id);

                var task = new TaskDto
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    CreatedAt = result.CreatedAt,
                    DueDate = result.DueDate,
                    ManagerId = result.ManagerId,
                };
                return Ok(task);
            }
            catch (Exception ex)
            {
                var newLog = new Log
                {
                    Timestamp = DateTime.Now,
                    Message = $"Couldn't find task with Id {id}",
                    Level = 3
                };

                _context.Logs.Add(newLog);
                _context.SaveChanges();
                return StatusCode(404, ex.Message);
            }
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<TaskDto>> Search(string name, int page = 1, int count = 10)
        {
            try
            {
                var results = _context.Tasks.Where(x => x.Name.Contains(name));
                
                var paged = results.Skip((page - 1) * count).Take(count).ToList();

                var tasks = paged.Select(t => new TaskDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    CreatedAt = t.CreatedAt,
                    DueDate = t.DueDate,
                    ManagerId = t.ManagerId,
                }).ToList();

                if (!tasks.Any()) return NotFound();

                return Ok(tasks);
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }

        // POST api/<TaskController>
        [HttpPost("[action]")]
        public ActionResult<TaskDto> Post([FromBody] TaskDto value)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newTask = new Task
                {
                    Id = value.Id,
                    Name = value.Name,
                    Description = value.Description,
                    CreatedAt = value.CreatedAt,
                    DueDate = value.DueDate,
                    ManagerId = value.ManagerId,
                };


                _context.Tasks.Add(newTask);
                _context.SaveChanges();

                var newLog = new Log
                {
                    Timestamp = DateTime.Now,
                    Message = $"Created task with Id {newTask.Id}",
                    Level = 1
                };

                _context.Logs.Add(newLog);
                _context.SaveChanges();

                value.Id = newTask.Id;

                return Ok(newTask);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        // PUT api/<TaskController>/5
        [HttpPut("[action]{id}")]
        public ActionResult<TaskDto> Put(int id, [FromBody] TaskDto value)
        {
            try
            {
                var result = _context.Tasks.FirstOrDefault(x => x.Id == id);

                result.Name = value.Name;
                result.Description = value.Description;
                result.CreatedAt = value.CreatedAt;
                result.DueDate = value.DueDate;
                result.ManagerId = value.ManagerId;

                _context.SaveChanges();

                var newLog = new Log
                {
                    Timestamp = DateTime.Now,
                    Message = $"Updated task with Id {result.Id}",
                    Level = 1
                };

                _context.Logs.Add(newLog);
                _context.SaveChanges();

                value.Id = result.Id;
                return Ok(result);
            }
            catch (Exception)
            {
                var newLog = new Log
                {
                    Timestamp = DateTime.Now,
                    Message = $"Couldn't update task with Id {id}",
                    Level = 3
                };

                _context.Logs.Add(newLog);
                _context.SaveChanges();
                return BadRequest();
            }
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("[action]{id}")]
        public ActionResult<TaskDto> Delete(int id)
        {
            try
            {
                var result = _context.Tasks.FirstOrDefault(x => x.Id == id);

                var resultDto = new TaskDto
                {
                    Id = result.Id,
                    Name = result.Name,
                    Description = result.Description,
                    CreatedAt = result.CreatedAt,
                    DueDate = result.DueDate,
                    ManagerId = result.ManagerId,
                };

                _context.Tasks.Remove(result);
                _context.SaveChanges();

                var newLog = new Log
                {
                    Timestamp = DateTime.Now,
                    Message = $"Deleted task with Id {resultDto.Id}",
                    Level = 2
                };

                _context.Logs.Add(newLog);
                _context.SaveChanges();


                return Ok(resultDto);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
