using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasswordManager.Models.Export
{
    public class ExportMovieModel
    {
        public string Title { get; set; }

        public string Director { get; set; }

        public int Year { get; set; }

        public string Tomatometer { get; set; }

        public string IMDBRating { get; set; }        
    }
}