using PasswordManager.Models.Entities;
using System.Collections.Generic;
using System.Web.Mvc;
using PagedList;

namespace PasswordManager.Models
{
    public class CityIndexViewModel
    {
        public IPagedList<City> Cities { get; set; }

        public int TotalCount { get; set; }

        public SelectList Province { get; set; }
        public string SelectedProvince { get; set; }
    }
}