using PasswordManager.Models.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PasswordManager.Models
{
    public class ArtCenterIndexViewModel
    {
        public IEnumerable<Center> Centers { get; set; }

        public int TotalCount { get; set; }

        public SelectList Province { get; set; }
        
        public string SearchString { get; set; }
    }
}