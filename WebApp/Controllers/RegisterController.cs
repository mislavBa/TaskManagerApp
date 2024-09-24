using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebAPI.Security;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly TaskManagerContext _context;

        public RegisterController(TaskManagerContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserVM userVm)
        {
            try
            {
                var trimmed = userVm.Username.Trim();
                if(_context.Users.Any(x => x.Username.Equals(trimmed)))
                {
                    return BadRequest("Username already exists");
                }

                var b64salt = PasswordHashProvider.GetSalt();
                var b64hash = PasswordHashProvider.GetHash(userVm.Password, b64salt);

                var user = new User
                {
                    Id = userVm.Id,
                    Username = userVm.Username,
                    PwdHash = b64hash,
                    PwdSalt = b64salt,
                    FirstName = userVm.FirstName,
                    LastName = userVm.LastName,
                    Email = userVm.Email,
                    Phone = userVm.Phone,
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                userVm.Id = user.Id;
                return View("RegisterSuccess");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}
