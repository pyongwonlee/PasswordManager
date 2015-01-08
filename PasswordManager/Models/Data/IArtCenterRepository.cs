using PasswordManager.Models.Entities;
using PasswordManager.Models.Export;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface IArtCenterRepository : IDisposable
    {
        IEnumerable<Province> Provinces { get; }
        IEnumerable<Province> ProvinceNames { get; }

        IEnumerable<City> Cities { get; }

        IEnumerable<Center> Centers { get; }
        IEnumerable<Center> GetCentersByProvince(int provinceId, string searchTerm);

        IEnumerable<ExportArtCenterModel> ArtCentersForExport { get; }

        int TotalCount { get; }

        Center Find(int id);

        bool Exists(string name);
        bool Exists(string name, int currentId);

        int Add(Center center);
        void Update(Center center);
        void Delete(int id);
    }
}
