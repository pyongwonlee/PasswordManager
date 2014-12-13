﻿using PagedList;
using PasswordManager.Models.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace PasswordManager.Models.Data
{
    public class EFMovieRepository : IMovieRepository
    {
        PasswordContext context;

        public EFMovieRepository()
        {
            context = new PasswordContext();
        }

        public IEnumerable<Director> Directors
        {
            get
            {
                return context.Directors
                        .OrderBy(c => c.Name)
                        .ToList();
            }
        }

        public IEnumerable<Director> DirectorNames
        {
            get
            {
                var directors =
                    context.Directors
                        .OrderBy(c => c.Name)
                        .ToList();
                directors.Insert(0, new Director { Id = 0, Name = "All" });
                return directors;
            }
        }

        public IEnumerable<Movie> Movies
        {
            get
            {
                return context.Movies
                        .OrderBy(m => m.Title)
                        .ToList();
            }
        }

        public IPagedList<Movie> GetMoviesByDirectorInPage(int directorId, int page, int pageSize)
        {
            return context.Movies
               .Include(m => m.Director)
               .Where(m => directorId == 0 || m.DirectorId == directorId)
               .OrderBy(m => m.Title)
               .ToPagedList(page, pageSize);
        }

        public Movie Find(int id)
        {
            return context.Movies.Find(id);
        }

        public int Add(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();

            return movie.Id;
        }

        public void Update(Movie movie)
        {
            context.Entry(movie).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Movie movie = Find(id);
            context.Movies.Remove(movie);
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