using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PasswordManager.Models.Data;
using PasswordManager.Models.Entities;
using System.Data.Entity;

namespace PasswordUnitTest.Models.Data
{
    [TestClass]
    public class CityRepositoryTest
    {

        Mock<DbSet<City>> mockSet;

        [TestInitialize]
        public void Initialize()
        {
            mockSet = new Mock<DbSet<City>>();

            var data = new List<City> 
            { 
                new City { Id = 2, Name = "BBB" }, 
                new City { Id = 26, Name = "ZZZ" }, 
                new City { Id = 1, Name = "AAA" }, 
                new City { Id = 3, Name = "CCC" }, 
                new City { Id = 5, Name = "EEE" }, 
                new City { Id= 4, Name = "DDD" }, 
                new City { Id = 25, Name = "YYY" }
            }.AsQueryable();

            mockSet.As<IDbSet<City>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IDbSet<City>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IDbSet<City>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IDbSet<City>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());            
        }

        [TestMethod]
        public void CityRepository_Property_TotalCount()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Cities).Returns(mockSet.Object);
            var cityRepository = new EFCityRepository(mockPasswordContext.Object);

            // Act
            var count = cityRepository.TotalCount;

            // Assert
            mockPasswordContext.VerifyGet(x => x.Cities);
            Assert.AreEqual(7, count);
        }

        [TestMethod]
        public void CityRepository_Cities_Property_OrderBy()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Cities).Returns(mockSet.Object);
            var cityRepository = new EFCityRepository(mockPasswordContext.Object);

            // Act
            var firstCity = cityRepository.Cities.FirstOrDefault();

            // Assert
            Assert.IsNotNull(firstCity);
            Assert.AreEqual("AAA", firstCity.Name);
        }
        
        [TestMethod]
        public void CityRepository_Method_Exists_Success()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Cities).Returns(mockSet.Object);
            var cityRepository = new EFCityRepository(mockPasswordContext.Object);

            // Act
            var exists = cityRepository.Exists("EEE");

            // Assert
            mockPasswordContext.VerifyGet(x => x.Cities);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void CityRepository_Method_Exists_Fail()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Cities).Returns(mockSet.Object);
            var cityRepository = new EFCityRepository(mockPasswordContext.Object);

            // Act
            var exists = cityRepository.Exists("MMM");

            // Assert
            mockPasswordContext.VerifyGet(x => x.Cities);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void CityRepository_Add_Calls_SaveChanges()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.SaveChanges());
            mockPasswordContext.Setup(x => x.Cities).Returns(mockSet.Object);
            var cityRepository = new EFCityRepository(mockPasswordContext.Object);

            // Act
            cityRepository.Add(new City());

            // Assert
            mockPasswordContext.VerifyGet(x => x.Cities);
            mockPasswordContext.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CityRepository_GetCitiesInPage_InvalidPageNumber()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Cities).Returns(mockSet.Object);
            var cityRepository = new EFCityRepository(mockPasswordContext.Object);

            int page = -1;
            int pageSize = 5;

            // Act
            cityRepository.GetCitiesInPage("", page, pageSize);

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CityRepository_GetCitiesInPage_InvalidPageSize()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Cities).Returns(mockSet.Object);
            var cityRepository = new EFCityRepository(mockPasswordContext.Object);

            int page = 1;
            int pageSize = -1;

            // Act
            cityRepository.GetCitiesInPage("", page, pageSize);

            // Assert
        }
    }
}
