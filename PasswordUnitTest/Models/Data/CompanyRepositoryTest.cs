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
    public class ConmpanyRepositoryTest
    {

        Mock<DbSet<Company>> mockSet;

        [TestInitialize]
        public void Initialize()
        {
            mockSet = new Mock<DbSet<Company>>();

            var category1 = new Category { CategoryId = 1, Name = "A" };
            var category2 = new Category { CategoryId = 2, Name = "B" };

            var data = new List<Company> 
            { 
                new Company { CompanyId = 2, Name = "BBB", Category = category1, CategoryId = category1.CategoryId }, 
                new Company { CompanyId = 26, Name = "ZZZ", Category = category1, CategoryId = category1.CategoryId }, 
                new Company { CompanyId = 1, Name = "AAA", Category = category1, CategoryId = category1.CategoryId }, 
                new Company { CompanyId = 3, Name = "CCC", Category = category1, CategoryId = category1.CategoryId }, 
                new Company { CompanyId = 5, Name = "EEE", Category = category2, CategoryId = category2.CategoryId }, 
                new Company { CompanyId = 4, Name = "DDD", Category = category2, CategoryId = category2.CategoryId }, 
                new Company { CompanyId = 25, Name = "YYY", Category = category2, CategoryId = category2.CategoryId }
            }.AsQueryable();

            mockSet.As<IDbSet<Company>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IDbSet<Company>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IDbSet<Company>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IDbSet<Company>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());            
        }

        [TestMethod]
        public void CompanyRepository_Property_TotalCount()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Companies).Returns(mockSet.Object);
            var companyRepository = new EFCompanyRepository(mockPasswordContext.Object);

            // Act
            var count = companyRepository.TotalCount;

            // Assert
            mockPasswordContext.VerifyGet(x => x.Companies);
            Assert.AreEqual(7, count);
        }
        
        [TestMethod]
        public void CompanyRepository_Method_Exists_Success()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Companies).Returns(mockSet.Object);
            var companyRepository = new EFCompanyRepository(mockPasswordContext.Object);

            // Act
            var exists = companyRepository.Exists("EEE");

            // Assert
            mockPasswordContext.VerifyGet(x => x.Companies);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void CompanyRepository_Method_Exists_Fail()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Companies).Returns(mockSet.Object);
            var companyRepository = new EFCompanyRepository(mockPasswordContext.Object);

            // Act
            var exists = companyRepository.Exists("MMM");

            // Assert
            mockPasswordContext.VerifyGet(x => x.Companies);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void CompanyRepository_Add_Calls_SaveChanges()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.SaveChanges());
            mockPasswordContext.Setup(x => x.Companies).Returns(mockSet.Object);
            var companyRepository = new EFCompanyRepository(mockPasswordContext.Object);

            // Act
            companyRepository.Add(new Company());

            // Assert
            mockPasswordContext.VerifyGet(x => x.Companies);
            mockPasswordContext.VerifyAll();
        }

        [TestMethod]
        public void CompanyRepository_GetCompaniesByCategory_ValidCategory()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Companies).Returns(mockSet.Object);
            var companyRepository = new EFCompanyRepository(mockPasswordContext.Object);

            int categoryId = 1;

            // Act
            var companies = companyRepository.GetCompaniesByCategory(categoryId);

            // Assert
            Assert.AreEqual(4, companies.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CompanyRepository_GetCompaniesByCategory_InvalidCategory()
        {
            // Arrange
            var mockPasswordContext = new Mock<PasswordContext>();
            mockPasswordContext.Setup(x => x.Companies).Returns(mockSet.Object);
            var companyRepository = new EFCompanyRepository(mockPasswordContext.Object);

            int categoryId = -1;

            // Act
            companyRepository.GetCompaniesByCategory(categoryId);

            // Assert
        }
    }
}
