using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BOL;

namespace BLL.Tests
{
    [TestClass]
    public class NameSearcherTest
    {
        [DataTestMethod]
        [DynamicData(nameof(GetData), DynamicDataSourceType.Method)]
        public void TestSearchMethod_Normal(IEnumerable<Customer> source, string searchString, IEnumerable<Customer> expected)
        {
            // Arrange
            NameSearcher searcher = new NameSearcher();
            // Act
            var actual = searcher.Search(source, searchString);
            // Assert
            Equals(actual, expected);
        }

        [DataTestMethod]
        public void TestSearchMethod_SearchStringIsNull()
        {
            // Arrange
            NameSearcher searcher = new NameSearcher();
            var source = new List<Customer>() { new Customer() { first_name = "Wayne", last_name = "Zhang" } };
            // Act
            var actual = searcher.Search(source, null);
            // Assert
            Equals(actual, source);
        }


        // Test Data
        public static IEnumerable<object[]> GetData()
        {
            var customer_1 = new Customer() { first_name = "Danny", last_name = "Yu" };
            var customer_2 = new Customer() { first_name = "Oliver", last_name = "Wang" };
            var customer_3 = new Customer() { first_name = "Wayne", last_name = "Zhang" };
            yield return new object[] { new List<Customer>() { customer_1, customer_2, customer_3 }, "danny", new List<Customer>() { customer_1 } };
            yield return new object[] { new List<Customer>() { customer_1, customer_2, customer_3 }, "yu", new List<Customer>() { customer_1 } };
            yield return new object[] { new List<Customer>() { customer_1, customer_2, customer_3 }, "DANNY", new List<Customer>() { customer_1 } };
            yield return new object[] { new List<Customer>() { customer_1, customer_2, customer_3 }, "ang", new List<Customer>() { customer_2, customer_3 } };
            yield return new object[] { new List<Customer>() { customer_1, customer_2, customer_3 }, "A", new List<Customer>() { customer_1, customer_2, customer_3 } };
            yield return new object[] { new List<Customer>() { customer_1, customer_2, customer_3 }, "W", new List<Customer>() { customer_2, customer_3 } };
            yield return new object[] { new List<Customer>() { customer_1, customer_2, customer_3 }, "y", new List<Customer>() { customer_1, customer_3 } };
        }
    }
}
;