using PagedList;
using PasswordManager.Models.Entities;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface IDirectorRepository : IRepository<Director>
    {
        IEnumerable<Director> Directors { get; }
        IPagedList<Director> GetDirectorsInPage(int page, int pageSize, string searchTerm);
        
        bool Exists(string name);
        bool Exists(string name, int currentId);
    }
}
