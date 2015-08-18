using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using PasswordManager.Models.Entities;
using PagedList;

namespace PasswordManager.Models.Data
{
    public class EFBookRepository : IBookRepository
    {
        PasswordContext context;

        public EFBookRepository()
        {
            context = new PasswordContext();
        }

        public IEnumerable<Book> Books
        {
            get
            {
                return context.Books
                    .OrderBy(b => b.Author)
                    .ToList();
            }
        }

        public IPagedList<Book> GetBooksInPage(string searchTerm, int page, int pageSize)
        {
            return context.Books
                .Where
                (b =>  string.IsNullOrEmpty(searchTerm) 
                    || b.Author.ToLower().Contains(searchTerm.ToLower())
                    || b.Title.ToLower().Contains(searchTerm.ToLower())
                    || b.Year.ToString().Contains(searchTerm)
                )
                .OrderBy(b => b.Author)
                .ToPagedList(page, pageSize);
        }

        public Book Find(int id)
        {
            return context.Books.Find(id);
        }

        public bool Exists(string author, string title)
        {
            return context.Books
                .Where(m => m.Author == author && m.Title == title).Any();
        }

        public int TotalCount
        {
            get
            {
                return context.Books.Count();
            }
        }

        public int Add(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();

            return book.BookId;
        }

        public void Update(Book book)
        {
            context.Entry(book).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Book book = Find(id);
            context.Books.Remove(book);
            context.SaveChanges();
        }

        public void Dispose()
        {
            if(context!=null)
            {
                context.Dispose();
            }
        }
    }
}