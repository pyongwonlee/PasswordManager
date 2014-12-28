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

        [Route("ArtCenter/{province}/{searchTerm?}")]
        public ActionResult Index(string province, string searchTerm)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "" : searchTerm;

            var model = new ArtCenterIndexViewModel
            {
                Centers = repository.GetCentersByProvince(province, searchTerm),
                TotalCount = repository.TotalCount,

                Province = new SelectList(repository.ProvinceNames, "Abbreviation", "Name", province),
                SelectedProvince = province,

                SearchString = searchTerm
            };
            return View(model);
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