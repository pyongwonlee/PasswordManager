using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PasswordManager.Models.Entities;
using PagedList;

namespace PasswordManager.Models.Data
{
    public class EFPasswordtRepository : IPasswordRepository
    {
        PasswordContext context;

        public EFPasswordtRepository()
        {
            context = new PasswordContext();
        }

        public IEnumerable<Company> Companies
        {
            get 
            {
                return context.Companies
                    .OrderBy(c => c.Name)
                    .ToList();
            }
        }


        public IEnumerable<Password> Passwords
        {
            get
            {
                return context.Passwords
                    .Include(pw => pw.Company)
                    .OrderBy(c => c.Company.Name)
                    .ToList();
            }
        }

        public IEnumerable<Password> GetPasswordsByCategory(int categoryId)
        {
            return context.Passwords
                .Include(pw => pw.Company)
                .Where(pw => categoryId==0 || pw.Company.CategoryId == categoryId)
                .OrderBy(c => c.Company.Name)
                .ToList();
        }

        public IPagedList<Password> GetPasswordsByCategoryInPage(int categoryId, int page, int pageSize)
        {
            return context.Passwords
                .Include(pw => pw.Company)
                .Where(pw => categoryId == 0 || pw.Company.CategoryId == categoryId)
                .OrderBy(c => c.Company.Name)
                .ToPagedList(page, pageSize);
        }

        public Password Find(int id)
        {
            return context.Passwords.Find(id);
        }

        public int Add(Password password)
        {
            context.Passwords.Add(password);
            context.SaveChanges();

            return password.PasswordId;
        }

        public void Update(Password password)
        {
            context.Entry(password).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Password password = Find(id);
            context.Passwords.Remove(password);
            context.SaveChanges();
        }

        public void Dispose()
        {
            if(context!=null)
            {
                context.Dispose();
            }
        }
    }
}