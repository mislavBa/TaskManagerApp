using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly TaskManagerContext _context;

        public TaskController(TaskManagerContext context)
        {
            _context = context;
        }

        // GET: TaskController
        public ActionResult Index(string searchQuery, int page = 1, int pageSize = 10)
        {
            var tasks = _context.Tasks.AsQueryable();       

            if (!string.IsNullOrEmpty(searchQuery))
            {
                tasks = tasks.Where(t => t.Name.Contains(searchQuery) ||
                               t.Description.Contains(searchQuery));
                ViewData["SearchQuery"] = searchQuery;
            }
            int total = tasks.Count();

            tasks = tasks.OrderBy(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            ViewData["Tasks"] = tasks.ToList();
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling((double)total / pageSize);
                

            return View();
        }


        // GET: TaskController/Details/5
        public ActionResult Details(int id)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);

            var taskVM = new TaskVM
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                ManagerId = task.ManagerId,
            };
            return View(taskVM);
        }

        // GET: TaskController/Create
        public ActionResult Create()
        {
            var managers = _context.Managers;

            var taskVm = new TaskVM
            {
                Managers = managers,
            };
            return View(taskVm);
        }

        // POST: TaskController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskVM task)
        {            
            try
            {
                var trimmed = task.Name.Trim();
                if(_context.Tasks.Any(x => x.Name == trimmed))
                {
                    return View("Duplicate");
                }
                var newTask = new Models.Task
                {
                    Name = task.Name,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    CreatedAt = task.CreatedAt,
                    ManagerId = task.ManagerId,
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

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskController/Edit/5
        public ActionResult Edit(int id)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);

            var taskVM = new TaskVM
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                ManagerId = task.ManagerId,
            };
            return View(taskVM);
        }

        // POST: TaskController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TaskVM task)
        {
            try
            {
                var dbTask = _context.Tasks.FirstOrDefault(x => x.Id==id);
                dbTask.Name = task.Name;
                dbTask.Description = task.Description;
                dbTask.DueDate = task.DueDate;
                dbTask.CreatedAt = task.CreatedAt;
                dbTask.ManagerId = task.ManagerId;

                _context.SaveChanges();

                var newLog = new Log
                {
                    Timestamp = DateTime.Now,
                    Message = $"Updated task with Id {id}",
                    Level = 1
                };
                _context.Logs.Add(newLog);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: TaskController/Delete/5        
        public ActionResult Remove(int id)
        {
            var task = _context.Tasks.FirstOrDefault(x => x.Id == id);

            var taskVM = new TaskVM
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt,
                ManagerId = task.ManagerId,
            };
            return View(taskVM);
        }

        // POST: TaskController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int id, TaskVM task)
        {
            try
            {
                var dbTask = _context.Tasks.FirstOrDefault(x => x.Id == id);
                _context.Tasks.Remove(dbTask);
                _context.SaveChanges();

                var newLog = new Log
                {
                    Timestamp = DateTime.Now,
                    Message = $"Deleted task with Id  {id}",
                    Level = 2
                };
                _context.Logs.Add(newLog);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
