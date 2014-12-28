using PagedList;
using PasswordManager.Models.Entities;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface ICityRepository : IDisposable
    {
        IEnumerable<Province> Provinces { get; }
        IEnumerable<Province> ProvinceNames { get; }

        IEnumerable<City> Cities { get; }
        IPagedList<City> GetCitiesInPage(string province, int page, int pageSize);

        City Find(int id);

        int TotalCount { get; }

        bool Exists(string name);
        bool Exists(string name, int currentId);

        int Add(City city);
        void Update(City city);
        void Delete(int id);
    }
}
