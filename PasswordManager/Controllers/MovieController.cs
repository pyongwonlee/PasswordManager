using PasswordManager.Models.Data;
using System.Web.Mvc;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Enums;
using PasswordManager.Models;
using PasswordManager.Helpers;

namespace PasswordManager.Controllers
{
    public class MovieController : Controller
    {
        private IMovieRepository repository;
        private IPreferenceRepository preference;
        private const int PAGE_SIZE = 50;

        public MovieController(IMovieRepository repo, IPreferenceRepository pref)
        {
            repository = repo;
            preference = pref;
        }

        [Route("Movies")]
        public ActionResult Index(int page=1, string sortKey="title", string searchTerm="")
        {
            int directorId = preference.DirectorId; 
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "" : searchTerm.Trim();

            MoiveSortResult sort = MovieSortManager.Sort(sortKey);

            var model = new MovieIndexViewModel
            {
                Movies = repository.GetMoviesByDirectorInPage(directorId, sort.Field, sort.IsAscending, page, PAGE_SIZE, searchTerm),
                TotalCount = repository.TotalCount,

                DirectorId = new SelectList(repository.DirectorNames, "Id", "Name", directorId),
                SelectedDirectorId = directorId,

                SortKey         = sortKey,
                TitleSort       = (sortKey == MovieSortNames.Title) ? MovieSortNames.TitleDesc : MovieSortNames.Title,
                DirectorSort    = (sortKey == MovieSortNames.Director) ? MovieSortNames.DirectorDesc : MovieSortNames.Director,
                YearSort        = (sortKey == MovieSortNames.Year) ? MovieSortNames.YearDesc : MovieSortNames.Year,
                TomatoSort      = (sortKey == MovieSortNames.Tomatometer) ? MovieSortNames.TomatometerDesc : MovieSortNames.Tomatometer,
                IMDBSort        = (sortKey == MovieSortNames.IMDBRating) ? MovieSortNames.IMDBRatingDesc : MovieSortNames.IMDBRating,

                IsTitleSortUp = (sort.Field == MovieSortField.Title && sort.IsAscending),
                IsTitleSortDown = (sort.Field == MovieSortField.Title && !sort.IsAscending),
                IsDirectorSortUp = (sort.Field == MovieSortField.Director && sort.IsAscending),
                IsDirectorSortDown = (sort.Field == MovieSortField.Director && !sort.IsAscending),
                IsYearSortUp = (sort.Field == MovieSortField.Year && sort.IsAscending),
                IsYearSortDown = (sort.Field == MovieSortField.Year && !sort.IsAscending),
                IsTomatoSortUp = (sort.Field == MovieSortField.Tomatometer && sort.IsAscending),
                IsTomatoSortDown = (sort.Field == MovieSortField.Tomatometer && !sort.IsAscending),
                IsIMDBSortUp = (sort.Field == MovieSortField.IMDBRating && sort.IsAscending),
                IsIMDBSortDown = (sort.Field == MovieSortField.IMDBRating && !sort.IsAscending),

                SearchString = searchTerm
            };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_MovieList", model);
            }
            return View("Index", model);
        }

        [Route("Movie/Refresh")]
        public ActionResult Refresh()
        {
            int page = 1;
            string sortKey = "title";
            string searchTerm = "";
            preference.DirectorId = 0;
            return Index(page, sortKey, searchTerm);
        }

        [Route("Movie/Filter")]
        public ActionResult Filter(int directorId, string searchTerm = "")
        {
            int page = 1;
            string sortKey = "title";
            preference.DirectorId = directorId;
            return Index(page, sortKey, searchTerm);
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
                Trim(movie);
                if (!repository.Exists(movie.Title, movie.DirectorId))
                {
                    repository.Add(movie);
                    return RedirectToAction("Index");
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
                Trim(movie);
                if (!repository.Exists(movie.Title, movie.DirectorId, movie.Id))
                {
                    repository.Update(movie);
                    return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }

        [NonAction]
        private void Trim(Movie movie)
        {
            movie.Title = movie.Title.Trim();
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