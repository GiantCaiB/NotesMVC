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
    public class CallNoteServiceTest
    {
        [TestMethod]
        public void TestGetByCustomerId()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<CallNoteDB>()
                   .Setup(x => x.GetByCustomerId(1))
                   .Returns(It.IsAny<IEnumerable<CallNote>>);
                var cls = mock.Create<CallNoteService>();
                var actual = cls.GetByCustomerId(1);
                var expected = GetSampleCallNotes();
                Assert.IsTrue(actual != null);
            }
        }

        private List<CallNote> GetSampleCallNotes()
        {
            return new List<CallNote>()
            {
                new CallNote
                {
                    id = 555,
                    customer_id = 1,
                    parent_id = 1,
                    text_content = "CallNoteServiceTest"
                },
                new CallNote
                {
                    id = 556,
                    customer_id = 1,
                    parent_id = 555,
                    text_content = "CallNoteServiceTest"
                },
                new CallNote
                {
                    id = 557,
                    customer_id = 1,
                    parent_id = 556,
                    text_content = "CallNoteServiceTest"
                }
            };
        }
    }
}
