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
            string sortKey = MovieSortNames.Title;

            // Act
            MoiveSortResult sort = MovieSortManager.Sort(sortKey);

            // Assert
            Assert.AreEqual(MovieSortField.Title, sort.Field);
            Assert.IsTrue(sort.IsAscending);
        }

        [TestMethod]
        public void MovieSortManager_Sort_By_Title_Desc()
        {
            // Arrange
            string sortKey = MovieSortNames.TitleDesc;

            // Act
            MoiveSortResult sort = MovieSortManager.Sort(sortKey);

            // Assert
            Assert.AreEqual(MovieSortField.Title, sort.Field);
            Assert.IsFalse(sort.IsAscending);
        }

        [TestMethod]
        public void MovieSortManager_Sort_By_Director()
        {
            // Arrange
            string sortKey = MovieSortNames.Director;

            // Act
            MoiveSortResult sort = MovieSortManager.Sort(sortKey);

            // Assert
            Assert.AreEqual(MovieSortField.Director, sort.Field);
            Assert.IsTrue(sort.IsAscending);
        }

        [TestMethod]
        public void MovieSortManager_Sort_By_Year_Desc()
        {
            // Arrange
            string sortKey = MovieSortNames.YearDesc;

            // Act
            MoiveSortResult sort = MovieSortManager.Sort(sortKey);

            // Assert
            Assert.AreEqual(MovieSortField.Year, sort.Field);
            Assert.IsFalse(sort.IsAscending);
        }

        [TestMethod]
        public void MovieSortManager_Sort_By_Tomatometer()
        {
            // Arrange
            string sortKey = MovieSortNames.Tomatometer;

            // Act
            MoiveSortResult sort = MovieSortManager.Sort(sortKey);

            // Assert
            Assert.AreEqual(MovieSortField.Tomatometer, sort.Field);
            Assert.IsTrue(sort.IsAscending);
        }

        [TestMethod]
        public void MovieSortManager_Sort_By_IMDBRating_Desc()
        {
            // Arrange
            string sortKey = MovieSortNames.IMDBRatingDesc;

            // Act
            MoiveSortResult sort = MovieSortManager.Sort(sortKey);

            // Assert
            Assert.AreEqual(MovieSortField.IMDBRating, sort.Field);
            Assert.IsFalse(sort.IsAscending);
        }

        [TestMethod]
        public void MovieSortManager_Sort_By_Wrong_Key()
        {
            // Arrange
            string sortKey = "Bogus";

            // Act
            MoiveSortResult sort = MovieSortManager.Sort(sortKey);

            // Assert
            Assert.AreEqual(MovieSortField.Title, sort.Field);
            Assert.IsTrue(sort.IsAscending);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MovieSortManager_Sort_By_Null_Key()
        {
            // Arrange
            string sortKey = null;

            // Act
            MoiveSortResult sort = MovieSortManager.Sort(sortKey);

            // Assert
            Assert.AreEqual(MovieSortField.Title, sort.Field);
            Assert.IsTrue(sort.IsAscending);
        }
    }
}
