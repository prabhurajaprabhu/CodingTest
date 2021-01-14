using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using App.repositroy;
using App.services;

namespace App.Test.servicesTest
{
    [TestClass]
    public class CustomerServiceTest
    {
        [TestMethod]
        public void ResturnAsExpected()
        {
            var mockUserService = new Mock<ICompanyRepository>();

            var r = mockUserService.Setup(x => x.AddCustomer(It.Is<string>(i => i == "Test"), It.Is<string>(i => i == "Test1"), It.Is<string>(i => i == "test@gmail.com"),
              It.Is<DateTime>(i => i == DateTime.Now), It.Is<int>(i => i == 1))).Returns(false);

            var result = mockUserService.Object.AddCustomer("Test", "Test1", "test@gmail.com", DateTime.Now, 1);

            Assert.IsNotNull(result);

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void ResturnAsNotExpected()
        {
            var mockUserService = new Mock<ICompanyRepository>();

            var r = mockUserService.Setup(x => x.AddCustomer(It.Is<string>(i => i == "Test"), It.Is<string>(i => i == "Test1"), It.Is<string>(i => i == "test@gmail.com"),
              It.Is<DateTime>(i => i == new DateTime(1950, 01, 01)), It.Is<int>(i => i == 1))).Returns(true);

            var result = mockUserService.Object.AddCustomer("Test", "Test1", "test@gmail.com", new DateTime(1950, 01, 01), 1);

            Assert.IsNotNull(result);

            Assert.AreEqual(true, result);
        }
    }
}
