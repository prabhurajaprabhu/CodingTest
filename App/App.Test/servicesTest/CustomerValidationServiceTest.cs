using System;
using App.services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace App.Test.servicesTest
{
    [TestClass]
    public class CustomerValidationServiceTest
    {
        [TestMethod]
        public void ReturnValidAsExpected()
        {
            var customerValidationService = new CustomerValidationService();

            var actual = customerValidationService.IsValid("Test", "Test1", "test@gmail.com");

            Assert.AreEqual(true, actual);

        }

        [TestMethod]
        public void ReturnValidAsNotExpected()
        {
            var customerValidationService = new CustomerValidationService();

            var actual = customerValidationService.IsValid("", "", "test@gmail.com");

            Assert.AreEqual(false, actual);

        }

        [TestMethod]
        public void ReturnValidAsNotExpectedWhwnEmailAddressisInValid()
        {
            var customerValidationService = new CustomerValidationService();

            var actual = customerValidationService.IsValid("Test", "Test1", "testgmailcom");

            Assert.AreEqual(false, actual);

        }

        [TestMethod]
        public void ReturnValidAgeAsExpected()
        {
            var customerValidationService = new CustomerValidationService();

            var actual = customerValidationService.IsValidAge(new DateTime(1950, 01, 01));

            Assert.AreEqual(true, actual);

        }


        [TestMethod]
        public void ReturnValidAgeAsNotExpected()
        {
            var customerValidationService = new CustomerValidationService();

            var actual = customerValidationService.IsValidAge(DateTime.Now);

            Assert.AreEqual(false, actual);

        }
    }
}
