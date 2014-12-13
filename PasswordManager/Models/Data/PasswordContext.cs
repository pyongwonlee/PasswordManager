using PasswordManager.Models.Entities;
using System.Data.Entity;

namespace PasswordManager.Models.Data
{
    public class PasswordContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Password> Passwords { get; set; }
    }
}