using PasswordManager.Models.Data;
using System.Web.Mvc;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Enums;

namespace PasswordManager.Controllers
{
    public class MovieController : Controller
    {
        IMovieRepository repository;
        int PAGE_SIZE = 50;

        public MovieController(IMovieRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index(int directorId=0, string sortKey="title", int page = 1)
        {             
            MovieSortField sortField= MovieSortField.Title;
            bool sortAscending = true;

            switch(sortKey)
            {
                case "title":
                    sortField= MovieSortField.Title; sortAscending = true;
                    break;
                case "title_desc":
                    sortField = MovieSortField.Title; sortAscending = false;
                    break;
                case "director":
                    sortField = MovieSortField.Director; sortAscending = true;
                    break;
                case "director_desc":
                    sortField = MovieSortField.Director; sortAscending = false;
                    break;
                case "year":
                    sortField = MovieSortField.Year; sortAscending = true;
                    break;
                case "year_desc":
                    sortField = MovieSortField.Year; sortAscending = false;
                    break;
                case "tomato":
                    sortField = MovieSortField.Tomatometer; sortAscending = true;
                    break;
                case "tomato_desc":
                    sortField = MovieSortField.Tomatometer; sortAscending = false;
                    break;
                case "imdb":
                    sortField = MovieSortField.IMDBRating; sortAscending = true;
                    break;
                case "imdb_desc":
                    sortField = MovieSortField.IMDBRating; sortAscending = false;
                    break;
                default:
                    sortField = MovieSortField.Title; sortAscending = true;
                    break;
            }
            var movies = repository.GetMoviesByDirectorInPage(directorId, sortField, sortAscending, page, PAGE_SIZE);

            ViewBag.DirectorId = new SelectList(repository.DirectorNames, "Id", "Name", directorId);
            ViewBag.SelectedDirectorId = directorId;

            ViewBag.TitleSort = sortKey == "title_desc" ? "title" : "title_desc";
            ViewBag.DirectorSort = sortKey == "director_desc" ? "director" : "director_desc";
            ViewBag.YearSort = sortKey == "year_desc" ? "year" : "year_desc";
            ViewBag.TomatoSort = sortKey == "tomato_desc" ? "tomato" : "tomato_desc";
            ViewBag.IMDBSort = sortKey == "imdb_desc" ? "imdb" : "imdb_desc";

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