using PagedList;
using PasswordManager.Models.Entities;
using System.Web.Mvc;

namespace PasswordManager.Models
{
    public class DirectorIndexViewModel
    {
        public IPagedList<Director> Directors { get; set; }

        public int TotalCount { get; set; }

        public string SearchString { get; set; }
    }
}