using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PasswordManager.Models.Entities;

namespace PasswordManager.Models.Data
{
    public class EFCompanyRepository : ICompanyRepository
    {
        PasswordContext context;

        public EFCompanyRepository()
            : this(new PasswordContext())
        {
        }

        public EFCompanyRepository(PasswordContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Category> Categories
        {
            get
            {
                return context.Categories
                    .OrderBy(c => c.Name)
                    .ToList();
            }
        }

        public IEnumerable<Company> Companies
        {
            get 
            {
                return context.Companies
                    .Include(c => c.Category)
                    .OrderBy(c => c.Name)
                    .ToList();
            }
        }

        public IEnumerable<Company> GetCompaniesByCategory(int categoryId)
        {
            if (categoryId < 0)
            {
                throw new ArgumentException("Invalid category id");
            }

            return context.Companies
                .Where(c => c.CategoryId == categoryId)
                .Include(c => c.Category)
                .OrderBy(c => c.Name)
                .ToList();
        }

        public int TotalCount
        {
            get
            {
                return context.Companies.Count();
            }
        }

        public Company Find(int id)
        {
            return context.Companies.Find(id);
        }

        public bool Exists(string name)
        {
            return context.Companies
                .Where(d => d.Name == name).Count() > 0;
        }

        public int Add(Company company)
        {
            context.Companies.Add(company);
            context.SaveChanges();

            return company.CompanyId;
        }

        public void Update(Company company)
        {
            context.Entry(company).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Company company = Find(id);
            context.Companies.Remove(company);
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