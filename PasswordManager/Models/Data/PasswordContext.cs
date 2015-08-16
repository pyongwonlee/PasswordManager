using PasswordManager.Models.Entities;
using System.Data.Entity;

namespace PasswordManager.Models.Data
{
    public class PasswordContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Password> Passwords { get; set; }

        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Center> Centers { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Preference> Preferences { get; set; }
    }
}