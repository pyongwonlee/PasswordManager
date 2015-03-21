using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PasswordManager.Models.Entities;

namespace PasswordManager.Models.Data
{
    public class EFCategoryRepository : ICategoryRepository
    {
        PasswordContext context;

        public EFCategoryRepository()
        {
            context = new PasswordContext();
        }

        public IEnumerable<Category> Categories
        {
            get 
            {
                return context.Categories
                    .Include(c => c.Companies)
                    .OrderBy(c => c.Name)
                    .ToList();
            }
        }

        public IEnumerable<Category> CategoryNames
        {
            get
            {
                var categories =
                    context.Categories
                        .OrderBy(c => c.Name)
                        .ToList();
                categories.Insert(0, new Category { CategoryId = 0, Name = "All" });
                return categories;    
            }
        }

        public int TotalCount
        {
            get
            {
                return context.Categories.Count();
            }
        }

        public Category Find(int id)
        {
            return context.Categories.Find(id);
        }

        public int Add(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();

            return category.CategoryId;
        }

        public void Update(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Category category = Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public void Dispose()
        {
            if(context!=null)
            {
                context.Dispose();
            }
        }

        public int GetCategoryId(string name)
        {
            int categoryId = 0;
            if (!string.IsNullOrEmpty(name) && name != "All")
            {
                var category = context.Categories
                     .Where(c => c.Name == name)
                     .FirstOrDefault();

                if(category != null)
                {
                    categoryId = category.CategoryId;
                }
            }

            return categoryId;
        }
    }
}