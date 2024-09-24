using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Models;
using WebAPI.Security;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TaskManagerContext _context;

        public UserController(IConfiguration configuration, TaskManagerContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpGet("[action]")]
        public ActionResult GetToken()
        {
            try
            {
                var secureKey = _configuration["JWT:SecureKey"];
                var serializedToken = JwtTokenProvider.CreateToken(secureKey, 10);

                return Ok(serializedToken);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult<UserDto> Register(UserDto user)
        {
            try
            {
                var trimmedUser = user.Username.Trim();
                if(_context.Users.Any(x =>
                x.Username.Equals(trimmedUser)))
                {
                    return BadRequest($"User {trimmedUser} already exists!");
                }

                var b64salt = PasswordHashProvider.GetSalt();
                var b64hash = PasswordHashProvider.GetHash(user.Password, b64salt);

                var newUser = new User
                {
                    Id = user.Id,
                    Username = user.Username,
                    PwdHash = b64hash,
                    PwdSalt = b64salt,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                user.Id = newUser.Id;

                return Ok(user);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("[action]")]
        public ActionResult Login(UserLoginDto userDto)
        {
            try
            {
                var existingUser = _context.Users.FirstOrDefault(x =>
                x.Username.Trim() == userDto.Username);
                if (existingUser == null)
                {
                    return BadRequest("Incorrect username");
                }

                var b64hash = PasswordHashProvider.GetHash(userDto.Password, existingUser.PwdSalt);
                if (b64hash != existingUser.PwdHash)
                {
                    return BadRequest("Incorrect password");
                }

                var secureKey = _configuration["JWT:SecureKey"];
                var serializedToken = JwtTokenProvider.CreateToken(secureKey, 120, userDto.Username);

                return Ok(serializedToken);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
