using PasswordManager.Models.Data;
using System.Web.Mvc;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Enums;
using PasswordManager.Models;

namespace PasswordManager.Controllers
{
    public class ArtCenterController : Controller
    {
        IArtCenterRepository repository;

        public ArtCenterController(IArtCenterRepository repo)
        {
            repository = repo;
        }

        [Route("ArtCenters/{searchTerm?}")]
        public ActionResult Index(string province, string searchTerm)
        {
            province = string.IsNullOrEmpty(province) ? "All" : province;
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "" : searchTerm;

            var model = new ArtCenterIndexViewModel
            {
                Centers = repository.GetCentersByProvince(province, searchTerm),
                TotalCount = repository.TotalCount,

                Province = new SelectList(repository.ProvinceNames, "Abbreviation", "Name", province),
                SelectedProvince = province,

                SearchString = searchTerm
            };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CenterList", model);
            }
            return View(model);
        }


        [Route("ArtCenter/Create")]
        public ActionResult Create()
        {
            ViewBag.CityIdDropdown = new SelectList(repository.Cities, "Id", "Name");
            return View(new Center());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ArtCenter/Create")]
        public ActionResult Create(Center center)
        {
            if (ModelState.IsValid)
            {
                repository.Add(center);
                return RedirectToAction("Index", new { province = "All" });
            }

            ViewBag.CityIdDropdown = new SelectList(repository.Cities, "Id", "Name", center.CityId);
            return View(center);
        }

        [Route("ArtCenter/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            Center center = repository.Find(id);
            if (center == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityIdDropdown = new SelectList(repository.Cities, "Id", "Name", center.CityId);
            return View(center);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ArtCenter/Edit/{id:int}")]
        public ActionResult Edit(Center center)
        {
            if (ModelState.IsValid)
            {
                repository.Update(center);
                return RedirectToAction("Index", new { province = "All", page = 1 });
            }
            ViewBag.CityIdDropdown = new SelectList(repository.Cities, "Id", "Name", center.CityId); 
            return View(center);
        }

        [Route("ArtCenter/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            Center center = repository.Find(id);
            if (center == null)
            {
                return HttpNotFound();
            }
            return View(center);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("ArtCenter/Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index", new { province = "All", page = 1 });
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