using System;
using System.Web.Mvc;
using Autofac.Extras.Moq;
using BLL;
using BOL;
using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Controllers;

namespace UI.Tests.Controllers
{
    [TestClass]
    public class CallNotesControllerTest
    {

        [TestMethod]
        public void Index()
        {
            // Arrange
            CallNotesController controller = new CallNotesController();

            // Act
            ViewResult result = controller.Index(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Details()
        {
            // Arrange
            CallNotesController controller = new CallNotesController();

            // Act
            ViewResult result = controller.Details(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Create()
        {
            // Arrange
            CallNotesController controller = new CallNotesController();

            // Act
            ViewResult result = controller.Create(1,1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Edit()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<CallNoteDB>()
                   .Setup(x => x.GetById(1))
                   .Returns(GetSampleCallNote());
                var cls = mock.Create<CallNoteService>();
                var db = mock.Create<CallNoteDB>();
                cls.SetDB(db);
                CallNotesController controller = new CallNotesController(cls);
                ViewResult result = controller.Edit(1) as ViewResult;
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void Delete()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<CallNoteDB>()
                   .Setup(x => x.GetById(1))
                   .Returns(GetSampleCallNote());
                var cls = mock.Create<CallNoteService>();
                var db = mock.Create<CallNoteDB>();
                cls.SetDB(db);
                CallNotesController controller = new CallNotesController(cls);
                ViewResult result = controller.Delete(1) as ViewResult;
                Assert.IsNotNull(result);
            }
        }

        private CallNote GetSampleCallNote()
        {
            return new CallNote()
            {
                id = 7777,
                parent_id = 1,
                text_content = "CallNotesControllerTest"
            };
        }
    }
}
