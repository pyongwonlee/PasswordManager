using System;
using System.Collections.Generic;
using PasswordManager.Models.Entities;
using PagedList;

namespace PasswordManager.Models.Data
{
    public interface IBookRepository : IDisposable
    {
        IEnumerable<Book> Books { get; }
        IPagedList<Book> GetBooksInPage(string searchTerm, int page, int pageSize);

        Book Find(int id);

        int Add(Book book);
        void Update(Book book);
        void Delete(int id);
    }
}
