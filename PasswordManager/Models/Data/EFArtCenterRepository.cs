using PagedList;
using PasswordManager.Models.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using PasswordManager.Models.Enums;
using PasswordManager.Models.Export;

namespace PasswordManager.Models.Data
{
    public class EFArtCenterRepository : IArtCenterRepository
    {
        PasswordContext context;

        public EFArtCenterRepository()
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

        public IEnumerable<Center> Centers
        {
            get
            {
                return context.Centers
                        .OrderBy(c => c.Name)
                        .ToList();
            }
        }

        public IEnumerable<Center> GetCentersByProvince(string province, string searchTerm)
        {
            var centers = context.Centers
               .Include(c => c.City)
               .Include(c => c.City.Province)
               .Where(c => (string.IsNullOrEmpty(province) || province == "All" || province == c.City.Province.Abbreviation) &&
                           (string.IsNullOrEmpty(searchTerm) || 
                            c.Name.ToLower().Contains(searchTerm.ToLower()) || 
                            c.City.Name.ToLower().Contains(searchTerm.ToLower()) ||
                            c.City.Province.Abbreviation.ToLower().Contains(searchTerm.ToLower()) ||
                            c.City.Province.Name.ToLower().Contains(searchTerm.ToLower())))            
                .OrderBy(c => c.Name);

            return centers;
        }
                
        public int TotalCount
        {
            get 
            {
                return context.Centers.Count();
            }
        }

        public Center Find(int id)
        {
            return context.Centers.Find(id);
        }

        public bool Exists(string name)
        {
            return context.Centers
                .Where(d => d.Name == name).Count() > 0;
        }

        public bool Exists(string name, int currentId)
        {
            return context.Centers
                .Where(d => d.Name == name && d.Id != currentId).Count() > 0;
        }

        public int Add(Center center)
        {
            context.Centers.Add(center);
            context.SaveChanges();

            return center.Id;
        }

        public void Update(Center center)
        {
            context.Entry(center).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Center center = Find(id);
            context.Centers.Remove(center);
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