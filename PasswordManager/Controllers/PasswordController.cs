using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Data;

namespace PasswordManager.Controllers
{
    public class PasswordController : AutheticatedController
    {
        IPasswordRepository repository;
        ICategoryRepository categoryRepository;
        int PAGE_SIZE = 10;

        public PasswordController(IPasswordRepository repo, ICategoryRepository cRepo)
        {
            repository = repo;
            categoryRepository = cRepo;
        }

        public ActionResult Index(int categoryId, int page = 1)
        {
            //int categoryId = categoryRepository.GetCategoryId(categoryName);

            var accounts = repository.GetPasswordsByCategoryInPage(categoryId, page, PAGE_SIZE);

            ViewBag.categoryId = new SelectList(categoryRepository.CategoryNames, "categoryId", "Name", categoryId);
            ViewBag.SelectedCategoryId = categoryId;

            return View(accounts);
        }

        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(repository.Companies, "CompanyId", "Name");
            return View(new Password());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Password pw)
        {
            if (ModelState.IsValid)
            {
                repository.Add(pw);
                return RedirectToAction("Index", new { categoryId = 0, page = 1 });
            }

            ViewBag.CompanyId = new SelectList(repository.Companies, "CompanyId", "Name", pw.CompanyId);
            return View(pw);
        }

        public ActionResult Edit(int id)
        {
            Password pw = repository.Find(id);
            if (pw == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(repository.Companies, "CompanyId", "Name", pw.CompanyId);
            return View(pw);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Password pw)
        {
            if (ModelState.IsValid)
            {
                repository.Update(pw);
                return RedirectToAction("Index", new { categoryId = 0, page = 1 });
            }
            ViewBag.CompanyId = new SelectList(repository.Companies, "CompanyId", "Name", pw.CompanyId);
            return View(pw);
        }

        public ActionResult Delete(int id)
        {
            Password pw = repository.Find(id);
            if (pw == null)
            {
                return HttpNotFound();
            }
            return View(pw);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index", new { categoryId = 0, page = 1 });
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