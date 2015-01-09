using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CsvHelper;
using PasswordManager.Models.Data;
using System.Text;
using System.IO;
using PasswordManager.Models.Export;

namespace PasswordManager.Controllers
{
    public class ExportController : AutheticatedController
    {
        IPasswordHelperRepository passwordRepository;
        IMovieRepository movieRepository;
        IArtCenterRepository artcenterRepository;

        public ExportController(IPasswordHelperRepository repo, IMovieRepository repo1, IArtCenterRepository repo2)
        {
            passwordRepository = repo;
            movieRepository = repo1;
            artcenterRepository = repo2;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Route("Export/Passwords")]
        public ActionResult ExportPasswordsToCsv()
        {
            using(var sw = new StringWriter() )
            {
                var csvWriter = new CsvWriter(sw);
                csvWriter.WriteRecords(passwordRepository.PasswordListForExport);

                return this.File(new UTF8Encoding().GetBytes(sw.ToString()), 
                            "text/csv", 
                            string.Format("Password-{0}.csv", DateTime.Now.ToString("g").Replace("/", "-").Replace(":", "_").Replace(" ", "-")));
            }
        }

        [Route("Export/Movies")]
        public ActionResult ExportMoviesToCsv()
        {
            using (var sw = new StringWriter())
            {
                var csvWriter = new CsvWriter(sw);
                csvWriter.WriteRecords(movieRepository.MoviesForExport);

                return this.File(new UTF8Encoding().GetBytes(sw.ToString()),
                            "text/csv",
                            string.Format("Movies-{0}.csv", DateTime.Now.ToString("g").Replace("/", "-").Replace(":", "_").Replace(" ", "-")));
            }
        }

        [Route("Export/ArtCenters")]
        public ActionResult ExportArtCentersToCsv()
        {
            using (var sw = new StringWriter())
            {
                var csvWriter = new CsvWriter(sw);
                csvWriter.WriteRecords(artcenterRepository.ArtCentersForExport);

                return this.File(new UTF8Encoding().GetBytes(sw.ToString()),
                            "text/csv",
                            string.Format("ArtCentres-{0}.csv", DateTime.Now.ToString("g").Replace("/", "-").Replace(":", "_").Replace(" ", "-")));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                passwordRepository.Dispose();
                movieRepository.Dispose();
            }
            base.Dispose(disposing);
        }
	}
}