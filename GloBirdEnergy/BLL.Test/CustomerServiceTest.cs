using System;
using System.Collections.Generic;
using Autofac.Extras.Moq;
using BOL;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BLL.Test
{
    [TestClass]
    public class CustomerServiceTest
    {
        [TestMethod]
        public void TestGetAll_Valid()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<CustomerDB>()
                    .Setup(x => x.GetAll())
                    .Returns(GetSampleCustomers());
                var cls = mock.Create<CustomerService>();
                var expected = GetSampleCustomers();
                var actual = (List<Customer>)cls.GetAll();
                Assert.IsTrue(actual != null);
                Assert.AreEqual(expected.Count, actual.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.Equals(expected[i].username, actual[i].username);
                    Assert.Equals(expected[i].first_name, actual[i].first_name);
                    Assert.Equals(expected[i].last_name, actual[i].last_name);
                    Assert.Equals(expected[i].phone_number, actual[i].phone_number);
                    Assert.Equals(expected[i].date_of_birth, actual[i].date_of_birth);
                }
            }
        }

        [TestMethod]
        public void TestGetById_Valid()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var customer = new Customer
                {
                    id = 1,
                    username = "GiantCaiB",
                    first_name = "Danny",
                    last_name = "Yu",
                    phone_number = "0433768987",
                    date_of_birth = new DateTime(1995, 5, 18)
                };
                mock.Mock<CustomerDB>()
                   .Setup(x => x.GetById(1))
                   .Returns(customer);
                var cls = mock.Create<CustomerService>();
                var expected = customer;
                var actual = cls.GetById(1);
                Assert.AreEqual(actual.id, expected.id);
                Assert.AreEqual(actual.username, expected.username);
                Assert.AreEqual(actual.first_name, expected.first_name);
                Assert.AreEqual(actual.last_name, expected.last_name);
            }
        }

        private List<Customer> GetSampleCustomers()
        {
            var customerList = new List<Customer>()
            {
                new Customer
                {
                    id = 1,
                    username = "GiantCaiB",
                    first_name = "Danny",
                    last_name = "Yu",
                    phone_number = "0433768987",
                    date_of_birth = new DateTime(1995,5,18)
                },
                new Customer
                {
                    id = 2,
                    username = "Tttt",
                    first_name = "Elmo",
                    last_name = "Zhang",
                    phone_number = "0433768666",
                    date_of_birth = new DateTime(1993,8,15)
                },
                new Customer
                {
                    id = 3,
                    username = "Vetty123",
                    first_name = "Victoria",
                    last_name = "Chang",
                    phone_number = "0434068409",
                    date_of_birth = new DateTime(1993,2,1)
                }
            };
            return customerList;
        }
    }
}
