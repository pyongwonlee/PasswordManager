using PagedList;
using PasswordManager.Models.Entities;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface IMovieRepository : IDisposable
    {
        IEnumerable<Director> Directors { get; }
        IEnumerable<Director> DirectorNames { get; }

        IEnumerable<Movie> Movies { get; }
        IPagedList<Movie> GetMoviesByDirectorInPage(int directorId, int page, int pageSize);

        Movie Find(int id);

        int Add(Movie movie);
        void Update(Movie movie);
        void Delete(int id);
    }
}
