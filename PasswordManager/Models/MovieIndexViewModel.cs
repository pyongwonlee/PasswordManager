using PagedList;
using PasswordManager.Models.Entities;
using System.Web.Mvc;

namespace PasswordManager.Models
{
    public class BookIndexViewModel
    {
        public IPagedList<Book> Books { get; set; }

        public int TotalCount { get; set; }
        public string SearchString { get; set; }
    }
}