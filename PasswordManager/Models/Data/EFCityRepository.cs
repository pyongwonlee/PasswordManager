using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PasswordManager.Models.Entities;
using PagedList;

namespace PasswordManager.Models.Data
{
    public class EFCityRepository : ICityRepository
    {
        PasswordContext context;

        public EFCityRepository()
        {
            context = new PasswordContext();
        }

        public IEnumerable<Province> Provinces
        {
            get
            {
                return context.Provinces
                        .OrderBy(p => p.Name)
                        .ToList();
            }
        }

        public IEnumerable<Province> ProvinceNames
        {
            get
            {
                var provinces =
                    context.Provinces
                        .OrderBy(p => p.Name)
                        .ToList();
                provinces.Insert(0, new Province { Id = 0, Name = "-- All --", Abbreviation = "All" });
                return provinces;
            }
        }

        public IEnumerable<City> Cities
        {
            get 
            {
                return context.Cities
                        .OrderBy(c => c.Name)
                        .ToList();
            }
        }

        public IPagedList<City> GetCitiesInPage(string province, int page, int pageSize)
        {
            return context.Cities
                .Include(c => c.Province)
                .Include(c => c.Centers)
                .Where(c => (string.IsNullOrWhiteSpace(province) || province == "All" || province == c.Province.Abbreviation))
                .OrderBy(c => c.Name)
                .ToPagedList(page, pageSize);
        }

        public City Find(int id)
        {
            return context.Cities.Find(id);
        }

        public int TotalCount
        {
            get
            {
                return context.Cities.Count();
            }
        }


        public bool Exists(string name)
        {
            return context.Cities
                .Where(c => c.Name == name).Count() > 0;
        }

        public bool Exists(string name, int currentId)
        {
            return context.Cities
                .Where(c => c.Name == name && c.Id != currentId).Count() > 0;
        }

        public int Add(City city)
        {
            context.Cities.Add(city);
            context.SaveChanges();

            return city.Id;
        }

        public void Update(City city)
        {
            context.Entry(city).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            City city = Find(id);
            context.Cities.Remove(city);
            context.SaveChanges();
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}