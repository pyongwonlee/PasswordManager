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

        [Route("Categories")]
        public ActionResult Index()
        {
            var categories = repository.Categories;
            return View(categories);
        }

        [Route("Category/Create")]
        public ActionResult Create()
        {
            Category category = new Category();
            return View(category);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Category/Create")]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                Trim(category);
                repository.Add(category);
                return RedirectToAction("Index");
            }

            return View(category);
        }

        [Route("Category/Edit/{id:int}")]
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
        [Route("Category/Edit/{id:int}")]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                Trim(category);
                repository.Update(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [Route("Category/Delete/{id:int}")]
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
        [Route("Category/Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index");
        }

        [NonAction]
        private void Trim(Category category)
        {
            category.Name = category.Name.Trim();
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
