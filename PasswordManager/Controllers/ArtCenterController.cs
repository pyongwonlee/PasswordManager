using PasswordManager.Models.Data;
using System.Web.Mvc;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Enums;
using PasswordManager.Models;

namespace PasswordManager.Controllers
{
    public class ArtCenterController : Controller
    {
        private IArtCenterRepository repository;
        private IPreferenceRepository preference;

        public ArtCenterController(IArtCenterRepository repo, IPreferenceRepository pref)
        {
            repository = repo;
            preference = pref;
        }

        [Route("ArtCenters")]
        public ActionResult Index(string searchTerm)
        {
            int province = preference.ProvinceId; 
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "" : searchTerm.Trim();

            var model = new ArtCenterIndexViewModel
            {
                Centers = repository.GetCentersByProvince(province, searchTerm),
                TotalCount = repository.TotalCount,

                Province = new SelectList(repository.ProvinceNames, "Id", "Name", province),

                SearchString = searchTerm
            };

            if (Request.IsAjaxRequest())
            {
                return PartialView("_CenterList", model);
            }
            return View("Index", model);
        }

        [Route("ArtCenter/Refresh")]
        public ActionResult Refresh()
        {
            preference.ProvinceId = 0;
            return Index("");
        }

        [Route("ArtCenter/Filter")]
        public ActionResult Filter(int province, string searchTerm)
        {
            preference.ProvinceId = province;
            return Index(searchTerm);
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
                Trim(center);
                if (!repository.Exists(center.Name))
                {
                    repository.Add(center);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", "The art centre name already exists.");
                }
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
                Trim(center);
                if (!repository.Exists(center.Name, center.Id))
                {
                    repository.Update(center);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", "The art centre name already exists.");
                }
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
            return RedirectToAction("Index");
        }

        [NonAction]
        private void Trim(Center center)
        {
            center.Name = center.Name.Trim();
            if (center.WebAddress != null)
            {
                center.WebAddress = center.WebAddress.Trim();
            }
            if (center.Description != null)
            {
                center.Description = center.Description.Trim();
            }
            if (center.Note != null)
            {
                center.Note = center.Note.Trim();
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