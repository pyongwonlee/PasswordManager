using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Data;
using PagedList;

namespace PasswordManager.Controllers
{
    public class CompanyController : AutheticatedController
    {
        ICompanyRepository repository;

        public CompanyController(ICompanyRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index(int page)
        {
            var companies = repository.Companies.ToPagedList(page, 10);
            return View(companies);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "Name");
            return View(new Company());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                repository.Add(company);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryIdDropdown = new SelectList(repository.Categories, "CategoryId", "Name", company.CategoryId);
            return View(company);
        }

        public ActionResult Edit(int id)
        {
            Company company = repository.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryIdDropdown = new SelectList(repository.Categories, "CategoryId", "Name", company.CategoryId);
            return View(company);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                repository.Update(company);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(repository.Categories, "CategoryId", "Name", company.CategoryId);
            return View(company);
        }

        public ActionResult Delete(int id)
        {
            Company company = repository.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index");
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
