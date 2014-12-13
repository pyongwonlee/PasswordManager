using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Data;

namespace PasswordManager.Controllers
{
    public class CategoryController : AutheticatedController
    {
        ICategoryRepository repository;

        public CategoryController(ICategoryRepository repo)
	    {
            this.repository = repo;   
	    }

        public ActionResult Index()
        {
            var categories = repository.Categories;
            return View(categories);
        }

        public ActionResult Create()
        {
            Category category = new Category();
            return View(category);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                repository.Add(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int id)
        {
            Category category = repository.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                repository.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }
                
        public ActionResult Delete(int id)
        {
            Category category = repository.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
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
