﻿using System;
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
    public class DirectorRepositoryTest
    {

        Mock<DbSet<Director>> mockSet;

        [TestInitialize]
        public void Initialize()
        {
            mockSet = new Mock<DbSet<Director>>(); 

            var data = new List<Director> 
            { 
                new Director { Id = 2, Name = "BBB" }, 
                new Director { Id = 26, Name = "ZZZ" }, 
                new Director { Id = 1, Name = "AAA" }, 
                new Director { Id = 3, Name = "CCC" }, 
                new Director { Id = 5, Name = "EEE" }, 
                new Director { Id= 4, Name = "DDD" }, 
                new Director { Id = 25, Name = "YYY" }
            }.AsQueryable();

            mockSet.As<IQueryable<Director>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Director>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Director>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Director>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            
        }

        [TestMethod]
        public void Directors_Property_Count()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();

            mockPasswordContext.Setup(x => x.Directors).Returns(mockSet.Object);

            var directorRepository = new EFDirectorRepository(mockPasswordContext.Object);

            // Act
            var count = directorRepository.Directors.Count();

            // Assert
            Assert.AreEqual(7, count);
        }

        [TestMethod]
        public void Directors_Property_OrderBy()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();

            mockPasswordContext.Setup(x => x.Directors).Returns(mockSet.Object);

            var directorRepository = new EFDirectorRepository(mockPasswordContext.Object);

            // Act
            var firstDirector = directorRepository.Directors.FirstOrDefault();

            // Assert
            Assert.IsNotNull(firstDirector);
            Assert.AreEqual("AAA", firstDirector.Name);
        }
        
        [TestMethod]
        public void Find_Existing_Director()
        {
            //// Arrange
            //var mockPasswordContext = new Mock<PasswordContext>();

            //mockPasswordContext.Setup(x => x.Directors).Returns(mockSet.Object);

            //var directorRepository = new EFDirectorRepository(mockPasswordContext.Object);

            //// Act
            //var directorEEE = directorRepository.Find(5);

            //// Assert
            //Assert.IsNotNull(directorEEE);
            //Assert.AreEqual("EEE", directorEEE.Name);

        }

        [TestMethod]
        public void Add_Calls_SaveChanges()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();

            mockPasswordContext.Setup(x => x.SaveChanges());
            mockPasswordContext.Setup(x => x.Directors).Returns(mockSet.Object);

            var directorRepository = new EFDirectorRepository(mockPasswordContext.Object);

            // Act
            directorRepository.Add(new Director());

            // Assert
            mockPasswordContext.VerifyAll();
        }
    }
}