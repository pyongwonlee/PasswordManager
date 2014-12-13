using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasswordManager.Models.Export
{
    public class ExportAccountModel
    {
        public string Company { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string Comment { get; set; }

        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public string Note3 { get; set; }
        public string Note4 { get; set; }
        public string Note5 { get; set; }
    }
}