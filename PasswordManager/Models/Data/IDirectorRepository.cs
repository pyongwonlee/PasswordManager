using PagedList;
using PasswordManager.Models.Entities;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface IDirectorRepository : IDisposable
    {
        IEnumerable<Director> Directors { get; }
        IPagedList<Director> GetDirectorsInPage(int page, int pageSize, string searchTerm);

        Director Find(int id);

        int TotalCount { get; }

        bool Exists(string name);
        bool Exists(string name, int currentId);

        int Add(Director director);
        void Update(Director director);
        void Delete(int id);
    }
}
