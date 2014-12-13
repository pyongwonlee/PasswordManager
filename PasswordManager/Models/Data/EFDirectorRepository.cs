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

        public IPagedList<Director> GetDirectorsInPage(int page, int pageSize)
        {
            return context.Directors
                .Include(d => d.Movies)
                .OrderBy(d => d.Name)
                .ToPagedList(page, pageSize);
        }

        public Director Find(int id)
        {
            return context.Directors.Find(id);
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