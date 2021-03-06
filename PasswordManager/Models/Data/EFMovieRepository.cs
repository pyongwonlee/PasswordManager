﻿using PagedList;
using PasswordManager.Models.Entities;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using PasswordManager.Models.Enums;
using PasswordManager.Models.Export;

namespace PasswordManager.Models.Data
{
    public class EFMovieRepository : IMovieRepository
    {
        private const int OTHER_DIRECTOR_ID = 90000;

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
                        .Where(d => d.Movies.Count() > 1)
                        .OrderBy(d => d.Name)
                        .ToList();
                directors.Insert(0, new Director { Id = 0, Name = "-- All --" });
                directors.Add(new Director { Id = OTHER_DIRECTOR_ID, Name = "-- Others --" });
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

        public IPagedList<Movie> GetMoviesByDirectorInPage(int directorId, MovieSortField sortField, bool sortAscending, int page, int pageSize, string searchTerm)
        {
            var movies = context.Movies
               .Include(m => m.Director)
               .Where(m => (directorId == 0 || m.DirectorId == directorId || (directorId == OTHER_DIRECTOR_ID && m.Director.Movies.Count() <= 1)) &&
                           (string.IsNullOrEmpty(searchTerm) || m.Title.ToLower().Contains(searchTerm.ToLower()) || m.Director.Name.ToLower().Contains(searchTerm.ToLower())));
            
            switch (sortField)
            {
                case MovieSortField.Title:
                    movies = sortAscending ? movies.OrderBy(m => m.Title.Replace("The ", "").Replace("the ", "").Replace("A ", "").Replace("An ", "")) : movies.OrderByDescending(m => m.Title.Replace("The ", "").Replace("the ", "").Replace("A ", "").Replace("An ", ""));
                    break;
                case MovieSortField.Director:
                    movies = sortAscending ? movies.OrderBy(m => m.Director.Name) : movies.OrderByDescending(m => m.Director.Name);
                    break;
                case MovieSortField.Year:
                    movies = sortAscending ? movies.OrderBy(m => m.Year) : movies.OrderByDescending(m => m.Year);
                    break;
                case MovieSortField.Tomatometer:
                    movies = sortAscending ? movies.OrderBy(m => m.Tomatometer) : movies.OrderByDescending(m => m.Tomatometer);
                    break;
                case MovieSortField.IMDBRating:
                    movies = sortAscending ? movies.OrderBy(m => m.IMDBRating) : movies.OrderByDescending(m => m.IMDBRating);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Title);
                    break;
            }

            return movies.ToPagedList(page, pageSize);
        }

        public IEnumerable<ExportMovieModel> MoviesForExport
        {
            get
            {
                var movies = context.Movies
                        .OrderBy(m => m.Title)
                        .ToList();

                return movies
                        .Select(m => new ExportMovieModel
                        {
                            Title = m.Title,
                            Director = m.Director.Name,
                            Year = m.Year,
                            Tomatometer = m.Tomatometer.HasValue ? string.Format("{0}", m.Tomatometer)  : "",
                            IMDBRating = m.IMDBRating.HasValue ? string.Format("{0:N1}", m.IMDBRating) : ""
                        })
                        .ToList();
            }
        }

        public int TotalCount
        {
            get 
            {
                return context.Movies.Count();
            }
        }

        public Movie Find(int id)
        {
            return context.Movies.Find(id);
        }

        public bool Exists(string title, int directorId)
        {
            return context.Movies
                .Where(m => m.Title == title && m.DirectorId == directorId).Any();
        }

        public bool Exists(string title, int directorId, int currentId)
        {
            return context.Movies
                .Where(m => m.Title == title && m.DirectorId == directorId && m.Id != currentId).Any();
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