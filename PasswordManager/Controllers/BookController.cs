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

        [Route("Book/Create")]
        public ActionResult Create()
        {
            return View(new Book() { Year = 2000 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Book/Create")]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                Trim(book);
                if (!repository.Exists(book.Author, book.Title))
                {
                    repository.Add(book);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Title", "The book already exists.");
                }
            }
            return View(book);
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

        [NonAction]
        private void Trim(Book book)
        {
            book.Author = book.Author.Trim();
            book.Title = book.Title.Trim();
            book.Publisher = book.Publisher.Trim();
            book.Location = book.Location.Trim();
            book.ISBN = book.ISBN.Trim();
        }
    }
}
