using PagedList;
using PasswordManager.Models.Entities;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface ICityRepository : IRepository<City>
    {
        IEnumerable<Province> Provinces { get; }
        IEnumerable<Province> ProvinceNames { get; }

        IEnumerable<City> Cities { get; }
        IPagedList<City> GetCitiesInPage(string province, int page, int pageSize);
        
        bool Exists(string name);
        bool Exists(string name, int currentId);
    }
}
