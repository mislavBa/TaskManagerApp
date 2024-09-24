using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly TaskManagerContext _context;

        public UserController(TaskManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Details()
        {
            var username = HttpContext.User.Identity.Name;

            var userDb = _context.Users.First(x => x.Username == username);
            var userVm = new UserVM
            {
                Id = userDb.Id,
                Username = userDb.Username,
                FirstName = userDb.FirstName,
                LastName = userDb.LastName,
                Email = userDb.Email,
                Phone = userDb.Phone,
            };

            return View(userVm);
        }

        public IActionResult DetailsPartial()
        {
            var username = HttpContext.User.Identity.Name;

            var userDb = _context.Users.First(x => x.Username == username);
            var userVm = new UserVM
            {
                Id = userDb.Id,
                Username = userDb.Username,
                FirstName = userDb.FirstName,
                LastName = userDb.LastName,
                Email = userDb.Email,
                Phone = userDb.Phone,
            };

            return PartialView("_PartialDetails", userVm);
        }

        public JsonResult GetProfileData(int id)
        {
            var userDb = _context.Users.First(x => x.Id == id);
            return Json(new
            {
                userDb.FirstName,
                userDb.LastName,
                userDb.Email,
                userDb.Phone,
            });
        }

        public ActionResult SetData(int id, [FromBody]UserVM userVm)
        {
            var userDb = _context.Users.First(x => x.Id == id);
            userDb.FirstName = userVm.FirstName;
            userDb.LastName = userVm.LastName;
            userDb.Email = userVm.Email;
            userDb.Phone = userVm.Phone;

            _context.SaveChanges();

            return Ok();
        }
        
        public IActionResult ProfileEdit(int id)
        {
            var userDb = _context.Users.First(x => x.Id == id);
            var userVm = new UserVM
            {
                Id = userDb.Id,
                Username = userDb.Username,
                FirstName = userDb.FirstName,
                LastName = userDb.LastName,
                Email = userDb.Email,
                Phone = userDb.Phone,
            };

            return View(userVm);
        }

        [HttpPost]
        public IActionResult ProfileEdit(int id, UserVM userVm)
        {
            var userDb = _context.Users.First(x => x.Id == id);
            userDb.FirstName = userVm.FirstName;
            userDb.LastName = userVm.LastName;
            userDb.Email = userVm.Email;
            userDb.Phone = userVm.Phone;

            _context.SaveChanges();

            return RedirectToAction("Details");
        }
    }
}
