using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Controllers;
using System.Web.Mvc;
using BOL;
using Autofac.Extras.Moq;
using DAL;
using BLL;

namespace UI.Tests.Controllers
{
    /// <summary>
    /// Summary description for CustomersControllerTest
    /// </summary>
    [TestClass]
    public class CustomersControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            CustomersController controller = new CustomersController();

            // Act
            ViewResult result = controller.Index("") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            CustomersController controller = new CustomersController();

            // Act
            ViewResult result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_GET()
        {
            // Arrange
            CustomersController controller = new CustomersController();

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create_POST()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<CustomerDB>()
                   .Setup(x => x.GetById(GetSampleCustomer()));
                var cls = mock.Create<CustomerService>();
                var db = mock.Create<CustomerDB>();
                cls.SetDB(db);
                CustomersController controller = new CustomersController(cls);
                ViewResult result = controller.Create(GetSampleCustomer()) as ViewResult;
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void Edit()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<CustomerDB>()
                   .Setup(x => x.GetById(1))
                   .Returns(GetSampleCustomer());
                var cls = mock.Create<CustomerService>();
                var db = mock.Create<CustomerDB>();
                cls.SetDB(db);
                CustomersController controller = new CustomersController(cls);
                ViewResult result = controller.Edit(1) as ViewResult;
                Assert.IsNotNull(result);
            }
        }


        private Customer GetSampleCustomer()
        {
            return new Customer()
            {
                id = 666,
                username = "TestUser666",
                first_name = "Elmo",
                last_name = "Zhang",
                phone_number = "0433768666",
                date_of_birth = new DateTime(1993, 8, 15)
            };
        }
    }
}
