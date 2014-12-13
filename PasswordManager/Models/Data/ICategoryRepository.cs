using PasswordManager.Models.Entities;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface ICategoryRepository : IDisposable
    {
        IEnumerable<Category> Categories { get; }
        IEnumerable<Category> CategoryNames { get; }

        Category Find(int id);

        int Add(Category category);
        void Update(Category category);
        void Delete(int id);

        int GetCategoryId(string name);
    }
}
