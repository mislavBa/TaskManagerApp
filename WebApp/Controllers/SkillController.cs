using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class SkillController : Controller
    {
        private readonly TaskManagerContext _context;

        public SkillController(TaskManagerContext context)
        {
            _context = context;
        }

        // GET: SkillController
        public ActionResult Index()
        {
            ViewData["Skills"] = _context.Skills;
            return View();
        }

        // GET: SkillController/Details/5
        public ActionResult Details(int id)
        {
            var skill = _context.Skills.FirstOrDefault(x => x.Id == id);

            var skillVM = new SkillVM
            {
                Id = skill.Id,
                Name = skill.Name,
                Description = skill.Description,
            };
            return View(skillVM);
        }

        // GET: SkillController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkillController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SkillVM skill)
        {
            var trimmed = skill.Name.Trim();
            if(_context.Skills.Any(x => x.Name.Equals(trimmed)))
            {
                return View("Duplicate");
            }

            try
            {
                var newSkill = new Skill
                {
                    Name = skill.Name,
                    Description = skill.Description,
                };

                _context.Skills.Add(newSkill);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SkillController/Edit/5
        public ActionResult Edit(int id)
        {
            var skill = _context.Skills.FirstOrDefault(x => x.Id == id);

            var skillVM = new SkillVM
            {
                Id = skill.Id,
                Name = skill.Name,
                Description = skill.Description,
            };
            return View(skillVM);
        }

        // POST: SkillController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SkillVM skill)
        {
            try
            {
                var dbSkill = _context.Skills.FirstOrDefault(x=> x.Id == id);   
                dbSkill.Name = skill.Name;
                dbSkill.Description = skill.Description;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SkillController/Delete/5
        public ActionResult Delete(int id)
        {
            var skill = _context.Skills.FirstOrDefault(x => x.Id == id);

            var skillVM = new SkillVM
            {
                Id = skill.Id,
                Name = skill.Name,
                Description = skill.Description,
            };
            return View(skillVM);
        }

        // POST: SkillController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SkillVM skill)
        {
            try
            {
                var dbSkill = _context.Skills.FirstOrDefault(x => x.Id == id);
                _context.Skills.Remove(dbSkill);
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
