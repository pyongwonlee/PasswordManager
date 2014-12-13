using PasswordManager.Models.Data;
using System.Web.Mvc;
using PasswordManager.Models.Entities;

namespace PasswordManager.Controllers
{
    public class MovieController : Controller
    {
        IMovieRepository repository;
        int PAGE_SIZE = 3;

        public MovieController(IMovieRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index(int directorId, int page = 1)
        {
            var movies = repository.GetMoviesByDirectorInPage(directorId, page, PAGE_SIZE);

            ViewBag.DirectorId = new SelectList(repository.DirectorNames, "Id", "Name", directorId);
            ViewBag.SelectedDirectorId = directorId;

            return View(movies);
        }

        public ActionResult Create()
        {
            ViewBag.DirectorDropdown = new SelectList(repository.Directors, "Id", "Name");
            return View(new Movie() { Year = 2000 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                repository.Add(movie);
                return RedirectToAction("Index", new { directorId = 0, page = 1 });
            }
            ViewBag.DirectorDropdown = new SelectList(repository.Directors, "Id", "Name", movie.DirectorId);
            return View(movie);
        }

        public ActionResult Edit(int id)
        {
            Movie movie = repository.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.DirectorDropdown = new SelectList(repository.Directors, "Id", "Name", movie.DirectorId);
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                repository.Update(movie);
                return RedirectToAction("Index", new { directorId = 0, page = 1 });
            }
            ViewBag.DirectorDropdown = new SelectList(repository.Directors, "Id", "Name", movie.DirectorId);
            return View(movie);
        }

        public ActionResult Delete(int id)
        {
            Movie movie = repository.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index", new { directorId = 0, page = 1 });
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