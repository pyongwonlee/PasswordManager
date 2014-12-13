using PasswordManager.Models.Entities;
using System;
using System.Collections.Generic;

namespace PasswordManager.Models.Data
{
    public interface IMovieRepository : IDisposable
    {
        IEnumerable<Director> Directors { get; }

        IEnumerable<Movie> Movies { get; }
        IEnumerable<Movie> GetMoviesByDirector(int directorId);

        Movie Find(int id);

        int Add(Movie category);
        void Update(Movie category);
        void Delete(int id);
    }
}
