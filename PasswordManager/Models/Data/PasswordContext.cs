using PasswordManager.Models.Entities;
using System.Data.Entity;

namespace PasswordManager.Models.Data
{
    public class PasswordContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Password> Passwords { get; set; }

        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Center> Centers { get; set; }

        public DbSet<Preference> Preferences { get; set; }
    }
}