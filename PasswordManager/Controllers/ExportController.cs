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
        IPasswordHelperRepository repository;

        public ExportController(IPasswordHelperRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ExportToCsv()
        {
            using(var sw = new StringWriter() )
            {
                var csvWriter = new CsvWriter(sw);
                //csvWriter.WriteRecords<ExportAccountModel>(repository.PasswordsForExport);
                csvWriter.WriteRecords(repository.PasswordListForExport);

                return this.File(new UTF8Encoding().GetBytes(sw.ToString()), 
                            "text/csv", 
                            string.Format("Password-{0}.csv", DateTime.Now.ToString("g").Replace("/", "-").Replace(":", "_").Replace(" ", "-")));
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