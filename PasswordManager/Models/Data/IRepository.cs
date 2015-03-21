using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManager.Models.Data
{
    public interface IRepository<T> : IDisposable
    {
        int TotalCount { get; }
        T Find(int id);

        int Add(T item);
        void Update(T item);
        void Delete(int id);
    }
}
