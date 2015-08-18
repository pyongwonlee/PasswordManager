using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PasswordManager.Models;
using PasswordManager.Models.Data;
using PasswordManager.Models.Entities;

namespace PasswordManager.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repository;
        private const int PAGE_SIZE = 50;

        public BookController(IBookRepository repo)
        {
            repository = repo;
        }

        [Route("Books")]
        public ActionResult Index(int page = 1, string searchTerm = "")
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "" : searchTerm.Trim();

            var model = new BookIndexViewModel
            {
                Books = repository.GetBooksInPage(searchTerm, page, PAGE_SIZE),
                TotalCount = repository.TotalCount,
                SearchString = searchTerm
            };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_BookList", model);
            }
            return View("Index", model);
        }

        [Route("Book/Refresh")]
        public ActionResult Refresh()
        {
            int page = 1;
            string searchTerm = "";
            return Index(page, searchTerm);
        }

        [Route("Book/Search")]
        public ActionResult Search(string searchTerm = "")
        {
            int page = 1;
            return Index(page, searchTerm);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
