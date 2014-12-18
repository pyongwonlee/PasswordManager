using PasswordManager.Models.Data;
using System.Web.Mvc;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Enums;
using PasswordManager.Models;

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

        [Route("Movies/{page:int?}")]
        public ActionResult Index(int directorId, string sortKey="title", int page = 1, string searchTerm = "")
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

            var model = new MovieIndexViewModel
            {
                Movies = repository.GetMoviesByDirectorInPage(directorId, sortField, sortAscending, page, PAGE_SIZE, searchTerm),
                TotalCount = repository.TotalCount,

                DirectorId = new SelectList(repository.DirectorNames, "Id", "Name", directorId),
                SelectedDirectorId = directorId,

                TitleSort       = (sortKey == "title_desc") ? "title" : "title_desc",
                DirectorSort    = (sortKey == "director_desc") ? "director" : "director_desc",
                YearSort        = (sortKey == "year_desc") ? "year" : "year_desc",
                TomatoSort      = (sortKey == "tomato_desc") ? "tomato" : "tomato_desc",
                IMDBSort        = (sortKey == "imdb_desc") ? "imdb" : "imdb_desc",

                IsTitleSortUp   = (sortField == MovieSortField.Title && sortAscending),
                IsTitleSortDown = (sortField == MovieSortField.Title && !sortAscending),
                IsDirectorSortUp    = (sortField == MovieSortField.Director && sortAscending),
                IsDirectorSortDown  = (sortField == MovieSortField.Director && !sortAscending),
                IsYearSortUp    = (sortField == MovieSortField.Year && sortAscending),
                IsYearSortDown  = (sortField == MovieSortField.Year && !sortAscending),
                IsTomatoSortUp  = (sortField == MovieSortField.Tomatometer && sortAscending),
                IsTomatoSortDown = (sortField == MovieSortField.Tomatometer && !sortAscending),
                IsIMDBSortUp    = (sortField == MovieSortField.IMDBRating && sortAscending),
                IsIMDBSortDown = (sortField == MovieSortField.IMDBRating && !sortAscending),

                SearchString = searchTerm
            };
            return View(model);
        }

        [Route("Movie/Create")]
        public ActionResult Create()
        {
            ViewBag.DirectorDropdown = new SelectList(repository.Directors, "Id", "Name");
            return View(new Movie() { Year = 2000 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Movie/Create")]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (!repository.Exists(movie.Title, movie.DirectorId))
                {
                    repository.Add(movie);
                    return RedirectToAction("Index", new { directorId = 0, page = 1 });
                }
                else
                {
                    ModelState.AddModelError("Title", "The movie already exists.");
                }
            }
            ViewBag.DirectorDropdown = new SelectList(repository.Directors, "Id", "Name", movie.DirectorId);
            return View(movie);
        }

        [Route("Movie/Edit/{id:int}")]
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
        [Route("Movie/Edit/{id:int}")]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (!repository.Exists(movie.Title, movie.DirectorId, movie.Id))
                {
                    repository.Update(movie);
                    return RedirectToAction("Index", new { directorId = 0, page = 1 });
                }
                else
                {
                    ModelState.AddModelError("Title", "The movie already exists.");
                }
            }
            ViewBag.DirectorDropdown = new SelectList(repository.Directors, "Id", "Name", movie.DirectorId);
            return View(movie);
        }

        [Route("Movie/Delete/{id:int}")]
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
        [Route("Movie/Delete/{id:int}")]
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