using PasswordManager.Models.Entities;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface ICompanyRepository : IDisposable
    {
        IEnumerable<Category> Categories { get; }

        IEnumerable<Company> Companies { get; }
        IEnumerable<Company> GetCompaniesByCategory(int categoryId);

        Company Find(int id);

        int Add(Company category);
        void Update(Company category);
        void Delete(int id);
    }
}
