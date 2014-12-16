using PasswordManager.Models.Data;
using System.Web.Mvc;
using PasswordManager.Models.Entities;

namespace PasswordManager.Controllers
{
    public class DirectorController : Controller
    {
        IDirectorRepository repository;
        int PAGE_SIZE = 20;

        public DirectorController(IDirectorRepository repo)
        {
            repository = repo;
        }

        [Route("Directors/{page:int?}")]
        public ActionResult Index(int page = 1)
        {
            var directors = repository
                .GetDirectorsInPage(page, PAGE_SIZE);

            return View(directors);
        }

        [Route("Director/Create")]
        public ActionResult Create()
        {
            return View(new Director());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Director/Create")]
        public ActionResult Create(Director director)
        {
            if (ModelState.IsValid)
            {
                if (!repository.Exists(director.Name))
                {
                    repository.Add(director);
                    return RedirectToAction("Index", new { page = 1 });
                }
                else
                {
                    ModelState.AddModelError("Name", "The director name already exists.");
                }
            }
            return View(director);
        }

        [Route("Director/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            Director director = repository.Find(id);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Director/Edit/{id:int}")]
        public ActionResult Edit(Director director)
        {
            if (ModelState.IsValid)
            {
                if (!repository.Exists(director.Name, director.Id))
                {
                    repository.Update(director);
                    return RedirectToAction("Index", new { page = 1 });
                }
                else
                {
                    ModelState.AddModelError("Name", "The director name already exists.");
                }
            }
            return View(director);
        }

        [Route("Director/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            Director director = repository.Find(id);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Director/Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index", new { page = 1 });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}