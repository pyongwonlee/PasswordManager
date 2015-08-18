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

        int TotalCount { get; }
        Book Find(int id);
        bool Exists(string author, string title);

        int Add(Book book);
        void Update(Book book);
        void Delete(int id);
    }
}
