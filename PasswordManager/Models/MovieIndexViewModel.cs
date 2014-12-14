using PagedList;
using PasswordManager.Models.Entities;
using System.Web.Mvc;

namespace PasswordManager.Models
{
    public class MovieIndexViewModel
    {
        public IPagedList<Movie> Movies { get; set; }

        public int TotalCount { get; set; }

        public SelectList DirectorId { get; set; }
        public int SelectedDirectorId { get; set; }

        public string TitleSort { get; set; }
        public string DirectorSort { get; set; }
        public string YearSort { get; set; }
        public string TomatoSort { get; set; }
        public string IMDBSort { get; set; }
    }
}