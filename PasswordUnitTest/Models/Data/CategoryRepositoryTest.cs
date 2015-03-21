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
    public class CategoryRepositoryTest
    {
        Mock<DbSet<Category>> mockSet;

        [TestInitialize]
        public void Initialize()
        {
            mockSet = new Mock<DbSet<Category>>();

            var data = new List<Category> 
            { 
                new Category { CategoryId = 2, Name = "BBB", Companies = new List<Company>() }, 
                new Category { CategoryId = 26, Name = "ZZZ", Companies = new List<Company>() }, 
                new Category { CategoryId = 1, Name = "AAA", Companies = new List<Company>() }, 
                new Category { CategoryId = 3, Name = "CCC", Companies = new List<Company>() }, 
                new Category { CategoryId = 5, Name = "EEE", Companies = new List<Company>() }, 
                new Category { CategoryId = 4, Name = "DDD", Companies = new List<Company>() }, 
                new Category { CategoryId = 25, Name = "YYY", Companies = new List<Company>() }
            }.AsQueryable();

            mockSet.As<IDbSet<Category>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IDbSet<Category>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IDbSet<Category>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IDbSet<Category>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());            
        }

        [TestMethod]
        public void CategoryRepository_Property_TotalCount()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Categories).Returns(mockSet.Object);
            var categoryRepository = new EFCategoryRepository(mockPasswordContext.Object);

            // Act
            var count = categoryRepository.TotalCount;

            // Assert
            mockPasswordContext.VerifyGet(x => x.Categories);
            Assert.AreEqual(7, count);
        }

        [TestMethod]
        public void CategoryRepository_CategoryNames_Property_OrderBy()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Categories).Returns(mockSet.Object);
            var categoryRepository = new EFCategoryRepository(mockPasswordContext.Object);

            // Act
            var firstTwoCategories = categoryRepository.CategoryNames.Take(2).ToList();

            // Assert
            Assert.IsNotNull(firstTwoCategories[0]);
            Assert.AreEqual("All", firstTwoCategories[0].Name);

            Assert.IsNotNull(firstTwoCategories[1]);
            Assert.AreEqual("AAA", firstTwoCategories[1].Name);
        }
        
        [TestMethod]
        public void CategoryRepository_Method_Exists_Success()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Categories).Returns(mockSet.Object);
            var categoryRepository = new EFCategoryRepository(mockPasswordContext.Object);

            // Act
            var exists = categoryRepository.Exists("EEE");

            // Assert
            mockPasswordContext.VerifyGet(x => x.Categories);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void CategoryRepository_Method_Exists_Fail()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Categories).Returns(mockSet.Object);
            var categoryRepository = new EFCategoryRepository(mockPasswordContext.Object);

            // Act
            var exists = categoryRepository.Exists("MMM");

            // Assert
            mockPasswordContext.VerifyGet(x => x.Categories);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void CategoryRepository_Add_Calls_SaveChanges()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.SaveChanges());
            mockPasswordContext.Setup(x => x.Categories).Returns(mockSet.Object);
            var categoryRepository = new EFCategoryRepository(mockPasswordContext.Object);

            // Act
            categoryRepository.Add(new Category());

            // Assert
            mockPasswordContext.VerifyGet(x => x.Categories);
            mockPasswordContext.VerifyAll();
        }

        [TestMethod]
        public void CategoryRepository_GetCategoryId_ExistingName()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Categories).Returns(mockSet.Object);
            var categoryRepository = new EFCategoryRepository(mockPasswordContext.Object);

            var expectedId = 26;

            // Act
            var actualId = categoryRepository.GetCategoryId("ZZZ");

            // Assert
            Assert.AreEqual(expectedId, actualId);
        }

        [TestMethod]
        public void CategoryRepository_GetCategoryId_NonExistingName()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Categories).Returns(mockSet.Object);
            var categoryRepository = new EFCategoryRepository(mockPasswordContext.Object);

            var expectedId = 0;

            // Act
            var actualId = categoryRepository.GetCategoryId("ABC");

            // Assert
            Assert.AreEqual(expectedId, actualId);
        }
    }
}
