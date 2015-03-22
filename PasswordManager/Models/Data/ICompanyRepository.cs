using PasswordManager.Models.Entities;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface ICompanyRepository : IRepository<Company>
    {
        IEnumerable<Category> Categories { get; }

        IEnumerable<Company> Companies { get; }
        IEnumerable<Company> GetCompaniesByCategory(int categoryId);

        bool Exists(string name);
    }
}
