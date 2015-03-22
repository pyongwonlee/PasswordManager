using PasswordManager.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PasswordManager.Helpers
{
    public static class MovieSortNames
    {
        public const string Title = "title";
        public const string TitleDesc = "title_desc";
        public const string Director = "director";
        public const string DirectorDesc = "director_desc";
        public const string Year = "year";
        public const string YearDesc = "year_desc";
        public const string Tomatometer = "tomato";
        public const string TomatometerDesc = "tomato_desc";
        public const string IMDBRating = "imdb";
        public const string IMDBRatingDesc = "imdb_desc";
    }

    public class MoiveSortResult
    {
        public MovieSortField Field { get; set; }
        public bool IsAscending { get; set; }
    }

    public static class MovieSortManager
    {
        public static MoiveSortResult Sort(string sortKey)
        {
            MovieSortField sortField = MovieSortField.Title;
            bool sortAscending = true;

            if (string.IsNullOrWhiteSpace(sortKey))
            {
                throw new ArgumentException("Invalide sort key");
            }

            switch (sortKey)
            {
                case MovieSortNames.Title:
                    sortField = MovieSortField.Title; sortAscending = true;
                    break;
                case MovieSortNames.TitleDesc:
                    sortField = MovieSortField.Title; sortAscending = false;
                    break;
                case MovieSortNames.Director:
                    sortField = MovieSortField.Director; sortAscending = true;
                    break;
                case MovieSortNames.DirectorDesc:
                    sortField = MovieSortField.Director; sortAscending = false;
                    break;
                case MovieSortNames.Year:
                    sortField = MovieSortField.Year; sortAscending = true;
                    break;
                case MovieSortNames.YearDesc:
                    sortField = MovieSortField.Year; sortAscending = false;
                    break;
                case MovieSortNames.Tomatometer:
                    sortField = MovieSortField.Tomatometer; sortAscending = true;
                    break;
                case MovieSortNames.TomatometerDesc:
                    sortField = MovieSortField.Tomatometer; sortAscending = false;
                    break;
                case MovieSortNames.IMDBRating:
                    sortField = MovieSortField.IMDBRating; sortAscending = true;
                    break;
                case MovieSortNames.IMDBRatingDesc:
                    sortField = MovieSortField.IMDBRating; sortAscending = false;
                    break;
                default:
                    sortField = MovieSortField.Title; sortAscending = true;
                    break;
            }

            return new MoiveSortResult
            {
                Field = sortField,
                IsAscending = sortAscending
            };
        }
    }
}