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

        [Route("Passwords/{page:int?}")]
        public ActionResult Index(int categoryId, int page = 1)
        {
            var accounts = repository.GetPasswordsByCategoryInPage(categoryId, page, PAGE_SIZE);

            ViewBag.categoryId = new SelectList(categoryRepository.CategoryNames, "categoryId", "Name", categoryId);
            ViewBag.SelectedCategoryId = categoryId;

            return View(accounts);
        }

        [Route("Password/Create")]
        public ActionResult Create()
        {
            ViewBag.CompanyIdDropdown = new SelectList(repository.Companies, "CompanyId", "Name");
            return View(new Password());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Password/Create")]
        public ActionResult Create(Password pw)
        {
            if (ModelState.IsValid)
            {
                Trim(pw);
                repository.Add(pw);
                return RedirectToAction("Index", new { categoryId = 0, page = 1 });
            }

            ViewBag.CompanyIdDropdown = new SelectList(repository.Companies, "CompanyId", "Name", pw.CompanyId);
            return View(pw);
        }

        [Route("Password/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            Password pw = repository.Find(id);
            if (pw == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyIdDropdown = new SelectList(repository.Companies, "CompanyId", "Name", pw.CompanyId);
            return View(pw);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Password/Edit/{id:int}")]
        public ActionResult Edit(Password pw)
        {
            if (ModelState.IsValid)
            {
                Trim(pw);
                repository.Update(pw);
                return RedirectToAction("Index", new { categoryId = 0, page = 1 });
            }
            ViewBag.CompanyIdDropdown = new SelectList(repository.Companies, "CompanyId", "Name", pw.CompanyId);
            return View(pw);
        }

        [Route("Password/Delete/{id:int}")]
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
        [Route("Password/Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index", new { categoryId = 0, page = 1 });
        }

        [NonAction]
        private void Trim(Password pw)
        {
            pw.UserName = pw.UserName.Trim();
            pw.PasswordCode = pw.PasswordCode.Trim();
            if (pw.Note1 != null)
            {
                pw.Note1 = pw.Note1.Trim();
            }
            if (pw.Note2 != null)
            {
                pw.Note2 = pw.Note2.Trim();
            }
            if (pw.Note3 != null)
            {
                pw.Note3 = pw.Note3.Trim();
            }
            if (pw.Note4 != null)
            {
                pw.Note4 = pw.Note4.Trim();
            }
            if (pw.Note5 != null)
            {
                pw.Note5 = pw.Note5.Trim();
            }
            if (pw.Comment != null)
            {
                pw.Comment = pw.Comment.Trim();
            }
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