using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PasswordManager.Models.Entities;
using PagedList;

namespace PasswordManager.Models.Data
{
    public class EFDirectorRepository : IDirectorRepository
    {
        PasswordContext context;

        public EFDirectorRepository()
        {
            context = new PasswordContext();
        }

        public IEnumerable<Director> Directors
        {
            get 
            {
                return context.Directors
                        .OrderBy(c => c.Name)
                        .ToList();
            }
        }

        public IPagedList<Director> GetDirectorsInPage(int page, int pageSize, string searchTerm)
        {
            return context.Directors
                .Include(d => d.Movies)
                .Where(d => (string.IsNullOrEmpty(searchTerm) || d.Name.ToLower().Contains(searchTerm.ToLower())))
                .OrderBy(d => d.Name)
                .ToPagedList(page, pageSize);
        }

        public Director Find(int id)
        {
            return context.Directors.Find(id);
        }

        public int TotalCount
        {
            get
            {
                return context.Directors.Count();
            }
        }


        public bool Exists(string name)
        {
            return context.Directors
                .Where(d => d.Name == name).Count() > 0;
        }

        public bool Exists(string name, int currentId)
        {
            return context.Directors
                .Where(d => d.Name == name && d.Id != currentId).Count() > 0;
        }

        public int Add(Director director)
        {
            context.Directors.Add(director);
            context.SaveChanges();

            return director.Id;
        }

        public void Update(Director director)
        {
            context.Entry(director).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Director director = Find(id);
            context.Directors.Remove(director);
            context.SaveChanges();
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}