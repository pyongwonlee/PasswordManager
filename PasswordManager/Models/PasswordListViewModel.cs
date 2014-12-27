using PasswordManager.Models.Entities;
using System.Collections.Generic;

namespace PasswordManager.Models
{
    public class PasswordListViewModel
    {
        public IEnumerable<Password> Passwords { get; set; }
        public string SearchString { get; set; }
    }
}