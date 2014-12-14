using PasswordManager.Helpers;
using PasswordManager.Models.Data;
using System.Web.Mvc;
using System.Web.Routing;

namespace PasswordManager.Controllers
{
    public class HomeController : Controller
    {
        IPasswordHelperRepository repository;

        public HomeController(IPasswordHelperRepository repo)
        {
            this.repository = repo;
        }

        public ActionResult Default()
        {
            return View();
        }

        [Authorize]
        [Route("Home/Search/{searchTerm?}")]
        public ActionResult Index(string searchTerm)
        {
            var model = this.repository.GetPasswords("", searchTerm);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_List", model);
            }
            return View(model);
        }

        [Authorize]
        [Route("Home/Details/{id:int}")]
        public ActionResult Details(int id)
        {
            var model = this.repository.Find(id);

            return PartialView("_Details", model);
        }

        [Route("Home/About")]
        public ActionResult About()
        {
            ViewBag.Message = "Password Management";

            return View();
        }
    }
}