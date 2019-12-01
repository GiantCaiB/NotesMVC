using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLL.Tests
{
    [TestClass]
    public class CustomerValidatorTest
    {
        [DataTestMethod]
        [DynamicData(nameof(GetData_Birthday), DynamicDataSourceType.Method)]
        public void TestCheckAgeIsValid(DateTime dateOfBirth, bool expected)
        {
            // Arrange
            CustomerValidator validator = new CustomerValidator();
            // Act
            var actual = validator.CheckAgeIsValid(dateOfBirth);
            // Assert
            Assert.AreEqual(actual, expected);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetData_PhoneNumber), DynamicDataSourceType.Method)]
        public void TestCheckIsAustralianNumber(string phoneNumber, bool expected)
        {
            // Arrange
            CustomerValidator validator = new CustomerValidator();
            // Act
            var actual = validator.CheckIsAustralianNumber(phoneNumber);
            // Assert
            Assert.AreEqual(actual, expected);
        }

        // Test Data
        public static IEnumerable<object[]> GetData_Birthday()
        {
            yield return new object[] { DateTime.Today, false };
            yield return new object[] { DateTime.Today.AddDays(-1), true };
            yield return new object[] { DateTime.Today.AddYears(-110), true };
            yield return new object[] { DateTime.Today.AddYears(-110).AddDays(-1), false };
        }
        public static IEnumerable<object[]> GetData_PhoneNumber()
        {
            yield return new object[] { "", false };
            yield return new object[] { "Hello there", false };
            yield return new object[] { "0433777888", true };
            yield return new object[] { "0399998888", true };
            yield return new object[] { "1399998888", false };
            yield return new object[] { "03999988881", false };
            yield return new object[] { "039999888", false };
        }
    }
}
