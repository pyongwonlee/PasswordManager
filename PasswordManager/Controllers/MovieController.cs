﻿using PasswordManager.Models.Data;
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

            MovieSortField sortField;
            bool sortAscending;

            MovieSortManager.Sort(sortKey, out sortField, out sortAscending);

            var model = new MovieIndexViewModel
            {
                Movies = repository.GetMoviesByDirectorInPage(directorId, sortField, sortAscending, page, PAGE_SIZE, searchTerm),
                TotalCount = repository.TotalCount,

                DirectorId = new SelectList(repository.DirectorNames, "Id", "Name", directorId),
                SelectedDirectorId = directorId,

                TitleSort       = (sortKey == MovieSortNames.Title) ? MovieSortNames.TitleDesc : MovieSortNames.Title,
                DirectorSort    = (sortKey == MovieSortNames.Director) ? MovieSortNames.DirectorDesc : MovieSortNames.Director,
                YearSort        = (sortKey == MovieSortNames.Year) ? MovieSortNames.YearDesc : MovieSortNames.Year,
                TomatoSort      = (sortKey == MovieSortNames.Tomatometer) ? MovieSortNames.TomatometerDesc : MovieSortNames.Tomatometer,
                IMDBSort        = (sortKey == MovieSortNames.IMDBRating) ? MovieSortNames.IMDBRatingDesc : MovieSortNames.IMDBRating,

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