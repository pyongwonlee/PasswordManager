using PagedList;
using PasswordManager.Models.Entities;
using PasswordManager.Models.Enums;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface IMovieRepository : IDisposable
    {
        IEnumerable<Director> Directors { get; }
        IEnumerable<Director> DirectorNames { get; }

        IEnumerable<Movie> Movies { get; }
        IPagedList<Movie> GetMoviesByDirectorInPage(int directorId, MovieSortField sortField, bool sortAscending, int page, int pageSize);

        int TotalCount { get; }

        Movie Find(int id);

        bool Exists(string title, int directorId);
        bool Exists(string title, int directorId, int currentId);

        int Add(Movie movie);
        void Update(Movie movie);
        void Delete(int id);
    }
}
