using PasswordManager.Models.Entities;
using PasswordManager.Models.Export;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface IArtCenterRepository : IRepository<Center>
    {
        IEnumerable<Province> Provinces { get; }
        IEnumerable<Province> ProvinceNames { get; }

        IEnumerable<City> Cities { get; }

        IEnumerable<Center> Centers { get; }
        IEnumerable<Center> GetCentersByProvince(int provinceId, string searchTerm);

        IEnumerable<ExportArtCenterModel> ArtCentersForExport { get; }
                
        bool Exists(string name);
        bool Exists(string name, int currentId);
    }
}
