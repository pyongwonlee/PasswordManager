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
        public string SortKey { get; set; }

        public string TitleSort { get; set; }
        public string DirectorSort { get; set; }
        public string YearSort { get; set; }
        public string TomatoSort { get; set; }
        public string IMDBSort { get; set; }

        public bool IsTitleSortUp { get; set; }
        public bool IsTitleSortDown { get; set; }
        public bool IsDirectorSortUp { get; set; }
        public bool IsDirectorSortDown { get; set; }
        public bool IsYearSortUp { get; set; }
        public bool IsYearSortDown { get; set; }
        public bool IsTomatoSortUp { get; set; }
        public bool IsTomatoSortDown { get; set; }
        public bool IsIMDBSortUp { get; set; }
        public bool IsIMDBSortDown { get; set; }

        public string SearchString { get; set; }
    }
}