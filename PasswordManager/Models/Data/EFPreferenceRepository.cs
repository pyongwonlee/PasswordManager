using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PasswordManager.Models.Entities;
using PagedList;

namespace PasswordManager.Models.Data
{
    public class EFPreferenceRepository : IPreferenceRepository
    {
        private const string CategoryIdKey = "CategoryId";
        private const string DirectorIdKey = "DirectorId";
        private const string ProvinceIdKey = "ProvinceId";

        private PasswordContext context;

        public EFPreferenceRepository()
        {
            context = new PasswordContext();
        }

        public int CategoryId
        {
            get
            {
                var value = GetValue(CategoryIdKey);
                if (value > 0 && context.Categories.Find(value) == null) 
                {
                    value = 0;
                }
                return value;
            }
            set
            {
                SetValue(CategoryIdKey, value);
            }
        }

        public int DirectorId
        {
            get
            {
                var value = GetValue(DirectorIdKey);
                if (value > 0 && context.Directors.Find(value) == null) 
                {
                    value = 0;
                }
                return value;
            }
            set
            {
                SetValue(DirectorIdKey, value);
            }
        }

        public int ProvinceId
        {
            get
            {
                var value = GetValue(ProvinceIdKey);
                if (value > 0 && context.Provinces.Find(value) == null) 
                {
                    value = 0;
                }
                return value;
            }
            set
            {
                SetValue(ProvinceIdKey, value);
            }
        }

        private int GetValue(string key)
        {
            return context.Preferences
                        .Where(p => p.Key == key)
                        .Select(p => p.Value)
                        .SingleOrDefault();
        }

        private void SetValue(string key, int value)
        {
            var preference = new Preference
            {
                Key = key,
                Value = value
            };
            context.Entry(preference).State = EntityState.Modified;
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