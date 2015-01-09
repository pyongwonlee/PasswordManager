using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Data;
using PasswordManager.Models;

namespace PasswordManager.Controllers
{
    public class CityController : Controller
    {
        ICityRepository repository;
        int PAGE_SIZE = 20;

        public CityController(ICityRepository repo)
        {
            repository = repo;
        }

        [Route("Cities/{page:int}")]
        public ActionResult Index(string province, int page = 1)
        {
            var cities = repository.GetCitiesInPage(province, page, PAGE_SIZE);

            var model = new CityIndexViewModel
            {
                Cities = cities,
                TotalCount = repository.TotalCount,
                Province = new SelectList(repository.ProvinceNames, "Abbreviation", "Name", province),
                SelectedProvince = province
            };
            return View(model);
        }

        [Route("City/Create")]
        public ActionResult Create()
        {
            ViewBag.ProvinceIdDropdown = new SelectList(repository.Provinces, "Id", "Name");
            return View(new City());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("City/Create")]
        public ActionResult Create(City city)
        {
            if (ModelState.IsValid)
            {
                Trim(city);
                repository.Add(city);
                return RedirectToAction("Index", new { province = "All", page = 1 });
            }

            ViewBag.ProvinceIdDropdown = new SelectList(repository.Provinces, "Id", "Name", city.ProvinceId);
            return View(city);
        }

        [Route("City/Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            City city = repository.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProvinceIdDropdown = new SelectList(repository.Provinces, "Id", "Name", city.ProvinceId);
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("City/Edit/{id:int}")]
        public ActionResult Edit(City city)
        {
            if (ModelState.IsValid)
            {
                Trim(city);
                repository.Update(city);
                return RedirectToAction("Index", new { province = "All", page = 1 });
            }
            ViewBag.ProvinceIdDropdown = new SelectList(repository.Provinces, "Id", "Name", city.ProvinceId);
            return View(city);
        }

        [Route("City/Delete/{id:int}")]
        public ActionResult Delete(int id)
        {
            City city = repository.Find(id);
            if (city == null)
            {
                return HttpNotFound();
            }
            return View(city);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("City/Delete/{id:int}")]
        public ActionResult DeleteConfirmed(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index", new { province = "All", page = 1 });
        }

        [NonAction]
        private void Trim(City city)
        {
            city.Name = city.Name.Trim();
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