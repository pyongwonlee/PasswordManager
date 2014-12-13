using PagedList;
using PasswordManager.Models.Entities;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface IDirectorRepository : IDisposable
    {
        IEnumerable<Director> Directors { get; }
        IPagedList<Director> GetDirectorsInPage(int page, int pageSize);

        Director Find(int id);

        int Add(Director director);
        void Update(Director director);
        void Delete(int id);
    }
}
