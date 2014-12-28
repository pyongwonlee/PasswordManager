using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Data;
namespace PasswordManager.Controllers
{
    public class CityController : Controller
    {
        IArtCenterRepository repository;
        int PAGE_SIZE = 20;

        public CityController(IArtCenterRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View();
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