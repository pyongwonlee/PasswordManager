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
    public class ArtCenterRepositoryTest
    {
        Mock<DbSet<Center>> mockSet;

        [TestInitialize]
        public void Initialize()
        {
            mockSet = new Mock<DbSet<Center>>();

            var data = new List<Center> 
            { 
                new Center { Id = 2, Name = "BBB" }, 
                new Center { Id = 26, Name = "ZZZ" }, 
                new Center { Id = 1, Name = "AAA" }, 
                new Center { Id = 3, Name = "CCC" }, 
                new Center { Id = 5, Name = "EEE" }, 
                new Center { Id= 4, Name = "DDD" }, 
                new Center { Id = 25, Name = "YYY" }
            }.AsQueryable();

            mockSet.As<IDbSet<Center>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IDbSet<Center>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IDbSet<Center>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IDbSet<Center>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());            
        }

        [TestMethod]
        public void ArtCenterRepository_Property_TotalCount()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Centers).Returns(mockSet.Object);
            var centerRepository = new EFArtCenterRepository(mockPasswordContext.Object);

            // Act
            var count = centerRepository.TotalCount;

            // Assert
            mockPasswordContext.VerifyGet(x => x.Centers);
            Assert.AreEqual(7, count);
        }

        [TestMethod]
        public void ArtCenters_Property_OrderBy()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Centers).Returns(mockSet.Object);
            var centerRepository = new EFArtCenterRepository(mockPasswordContext.Object);

            // Act
            var firstCenter = centerRepository.Centers.FirstOrDefault();

            // Assert
            Assert.IsNotNull(firstCenter);
            Assert.AreEqual("AAA", firstCenter.Name);
        }
        
        [TestMethod]
        public void ArtCenterRepository_Method_Exists_Success()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Centers).Returns(mockSet.Object);
            var centerRepository = new EFArtCenterRepository(mockPasswordContext.Object);

            // Act
            var exists = centerRepository.Exists("EEE");

            // Assert
            mockPasswordContext.VerifyGet(x => x.Centers);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void ArtCenterRepository_Method_Exists_Fail()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Centers).Returns(mockSet.Object);
            var centerRepository = new EFArtCenterRepository(mockPasswordContext.Object);

            // Act
            var exists = centerRepository.Exists("MMM");

            // Assert
            mockPasswordContext.VerifyGet(x => x.Centers);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void ArtCenterRepository_Add_Calls_SaveChanges()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.SaveChanges());
            mockPasswordContext.Setup(x => x.Centers).Returns(mockSet.Object);
            var centerRepository = new EFArtCenterRepository(mockPasswordContext.Object);

            // Act
            centerRepository.Add(new Center());

            // Assert
            mockPasswordContext.VerifyGet(x => x.Centers);
            mockPasswordContext.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArtCenterRepository_Add_GetCentersByProvince_InvalidProvince()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Centers).Returns(mockSet.Object);
            var centerRepository = new EFArtCenterRepository(mockPasswordContext.Object);

            int provinceId = -1;

            // Act
            centerRepository.GetCentersByProvince(provinceId, "");

            // Assert
        }
    }
}
