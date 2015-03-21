using PasswordManager.Models.Entities;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> Categories { get; }
        IEnumerable<Category> CategoryNames { get; }
        
        int GetCategoryId(string name);

        bool Exists(string name);
    }
}
