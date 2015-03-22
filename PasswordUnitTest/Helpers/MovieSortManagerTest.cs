using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordManager.Models.Enums;
using PasswordManager.Helpers;

namespace PasswordUnitTest.Helpers
{
    [TestClass]
    public class MovieSortManagerTest
    {
        [TestMethod]
        public void MovieSortManager_Sort_By_Title()
        {
            // Arrange
            MovieSortField sortField;
            bool sortAscending;
            string sortKey = MovieSortNames.Title;

            // Act
            MovieSortManager.Sort(sortKey, out sortField, out sortAscending);

            // Assert
            Assert.AreEqual(MovieSortField.Title, sortField);
            Assert.IsTrue(sortAscending);
        }

        [TestMethod]
        public void MovieSortManager_Sort_By_Title_Desc()
        {
            // Arrange
            MovieSortField sortField;
            bool sortAscending;
            string sortKey = MovieSortNames.TitleDesc;

            // Act
            MovieSortManager.Sort(sortKey, out sortField, out sortAscending);

            // Assert
            Assert.AreEqual(MovieSortField.Title, sortField);
            Assert.IsFalse(sortAscending);
        }

        [TestMethod]
        public void MovieSortManager_Sort_By_Director()
        {
            // Arrange
            MovieSortField sortField;
            bool sortAscending;
            string sortKey = MovieSortNames.Director;

            // Act
            MovieSortManager.Sort(sortKey, out sortField, out sortAscending);

            // Assert
            Assert.AreEqual(MovieSortField.Director, sortField);
            Assert.IsTrue(sortAscending);
        }

        [TestMethod]
        public void MovieSortManager_Sort_By_Year_Desc()
        {
            // Arrange
            MovieSortField sortField;
            bool sortAscending;
            string sortKey = MovieSortNames.YearDesc;

            // Act
            MovieSortManager.Sort(sortKey, out sortField, out sortAscending);

            // Assert
            Assert.AreEqual(MovieSortField.Year, sortField);
            Assert.IsFalse(sortAscending);
        }

        [TestMethod]
        public void MovieSortManager_Sort_By_Tomatometer()
        {
            // Arrange
            MovieSortField sortField;
            bool sortAscending;
            string sortKey = MovieSortNames.Tomatometer;

            // Act
            MovieSortManager.Sort(sortKey, out sortField, out sortAscending);

            // Assert
            Assert.AreEqual(MovieSortField.Tomatometer, sortField);
            Assert.IsTrue(sortAscending);
        }

        [TestMethod]
        public void MovieSortManager_Sort_By_IMDBRating_Desc()
        {
            // Arrange
            MovieSortField sortField;
            bool sortAscending;
            string sortKey = MovieSortNames.IMDBRatingDesc;

            // Act
            MovieSortManager.Sort(sortKey, out sortField, out sortAscending);

            // Assert
            Assert.AreEqual(MovieSortField.IMDBRating, sortField);
            Assert.IsFalse(sortAscending);
        }

        [TestMethod]
        public void MovieSortManager_Sort_By_Wrong_Key()
        {
            // Arrange
            MovieSortField sortField;
            bool sortAscending;
            string sortKey = "Bogus";

            // Act
            MovieSortManager.Sort(sortKey, out sortField, out sortAscending);

            // Assert
            Assert.AreEqual(MovieSortField.Title, sortField);
            Assert.IsTrue(sortAscending);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MovieSortManager_Sort_By_Null_Key()
        {
            // Arrange
            MovieSortField sortField;
            bool sortAscending;
            string sortKey = null;

            // Act
            MovieSortManager.Sort(sortKey, out sortField, out sortAscending);

            // Assert
            Assert.AreEqual(MovieSortField.Title, sortField);
            Assert.IsTrue(sortAscending);
        }
    }
}
