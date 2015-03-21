using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PasswordManager.Models.Data;
using PasswordManager.Models.Entities;
using PasswordManager.Controllers;
using System.Web.Mvc;

namespace PasswordUnitTest.Controllers
{
    [TestClass]
    public class ArtCenterControllerTest
    {
        Mock<IArtCenterRepository> mockArtCenterRepository;
        Mock<IPreferenceRepository> preferenceRepository;

        [TestInitialize]
        public void Initialize()
        {
            mockArtCenterRepository = new Mock<IArtCenterRepository>();
            preferenceRepository = new Mock<IPreferenceRepository>();

            var cities = new List<City>
            { 
                new City { Id = 2, Name = "BBB" }, 
                new City { Id = 26, Name = "ZZZ" }, 
                new City { Id = 1, Name = "AAA" }, 
                new City { Id = 3, Name = "CCC" }, 
                new City { Id = 5, Name = "EEE" }, 
                new City { Id= 4, Name = "DDD" }, 
                new City { Id = 25, Name = "YYY" }
            };

            mockArtCenterRepository.Setup(m => m.Cities).Returns(cities);
        }

        [TestMethod]
        public void ArtCenterController_Create_Get()
        {
            // Arrange
            var artCenterController = new ArtCenterController(mockArtCenterRepository.Object, preferenceRepository.Object);

            // Act
            var result = artCenterController.Create() as ViewResult;
            Assert.IsNotNull(result);

            var dropdownObject = result.ViewBag.CityIdDropdown as SelectList;
            Assert.IsNotNull(dropdownObject);

            int expected = 7;
            int actual = 0;

            foreach(var item in dropdownObject.Items)
            {
                actual++;
            }
                         
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
