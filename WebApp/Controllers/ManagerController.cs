using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ManagerController : Controller
    {

        private readonly TaskManagerContext _context;

        public ManagerController(TaskManagerContext context)
        {
            _context = context;
        }


        // GET: ManagerController
        public ActionResult Index()
        {
            ViewData["Managers"] = _context.Managers;
            return View();
        }

        // GET: ManagerController/Details/5
        public ActionResult Details(int id)
        {
            var manager = _context.Managers.FirstOrDefault(m => m.Id == id);

            var managerVM = new ManagerVM
            {
                Id = manager.Id,
                FirstName = manager.FirstName,
                LastName = manager.LastName,
            };

            return View(managerVM);
        }

        // GET: ManagerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ManagerVM manager)
        {
            var trimmed = manager.FirstName.Trim();
            if(_context.Managers.Any(x => x.FirstName == trimmed))
            {
                return View("Duplicate");
            }

            try
            {
                var newManager = new Manager
                {
                    FirstName = manager.FirstName,
                    LastName = manager.LastName,
                };

                _context.Managers.Add(newManager);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            var manager = _context.Managers.FirstOrDefault(m => m.Id == id);

            var managerVM = new ManagerVM
            {
                Id = manager.Id,
                FirstName = manager.FirstName,
                LastName = manager.LastName,
            };
            return View(managerVM);
        }

        // POST: ManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ManagerVM manager)
        {
            try
            {
                var dbManager = _context.Managers.FirstOrDefault(x=> x.Id == id);   
                dbManager.FirstName = manager.FirstName;
                dbManager.LastName = manager.LastName;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: ManagerController/Delete/5
        public ActionResult Delete(int id)
        {
            var manager = _context.Managers.FirstOrDefault(m => m.Id == id);

            var managerVM = new ManagerVM
            {
                Id = manager.Id,
                FirstName = manager.FirstName,
                LastName = manager.LastName,
            };
            return View(managerVM);
        }

        // POST: ManagerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ManagerVM manager)
        {
            try
            {
                var dbManager = _context.Managers.FirstOrDefault(x => x.Id == id);

                _context.Managers.Remove(dbManager);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
