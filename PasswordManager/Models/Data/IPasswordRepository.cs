using PasswordManager.Models.Entities;
using System;
using System.Collections.Generic;
using PagedList;

namespace PasswordManager.Models.Data
{
    public interface IPasswordRepository : IDisposable
    {
        IEnumerable<Company> Companies { get; }

        IEnumerable<Password> Passwords { get; }
        IEnumerable<Password> GetPasswordsByCategory(int categoryId);
        IPagedList<Password> GetPasswordsByCategoryInPage(int categoryId, int page, int pageSize);

        Password Find(int id);

        int Add(Password password);
        void Update(Password password);
        void Delete(int id);
    }
}
