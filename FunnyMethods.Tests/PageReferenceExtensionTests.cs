using System;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Framework.Timers;
using EPiServer.Security;
using Moq;
using NUnit;
using NUnit.Framework;
using FunnyMethods.EPi7;

namespace FunnyMethods.Tests
{

    public class TestPage : PageData
    {
        
    }
    [TestFixture]
    public class PageReferenceExtensionTests
    {
        [Test]
        public void CreateRandomPage_Only_CreatesWhenModulus2IsZero()
        {

           Mock<IDateTime> dateMock = new Mock<IDateTime>();
            dateMock.SetupGet(i => i.Now).Returns(new DateTime(2000, 1, 2));
            
            Mock<IContentRepository> mock = new Mock<IContentRepository>();
            mock.Setup(i => i.GetDefault<TestPage>(It.IsAny<PageReference>())).Returns(new TestPage());
            var p = new PageReference(5);
            var t = p.CreateRandomPage<TestPage>(dateMock.Object, mock.Object);

            mock.Verify(i=>i.Save(It.IsAny<IContent>(),SaveAction.Publish, AccessLevel.NoAccess));
        }

        [Test]
        public void CreateRandomPage_DoesNot_CreatesWhenModulus2IsZero()
        {

            Mock<IDateTime> dateMock = new Mock<IDateTime>();
            dateMock.SetupGet(i => i.Now).Returns(new DateTime(2000, 1, 1));

            Mock<IContentRepository> mock = new Mock<IContentRepository>();
            mock.Setup(i => i.GetDefault<TestPage>(It.IsAny<PageReference>())).Returns(new TestPage());
            var p = new PageReference(5);
            var t = p.CreateRandomPage<TestPage>(dateMock.Object, mock.Object);

            mock.Verify(i => i.Save(It.IsAny<IContent>(), SaveAction.Publish, AccessLevel.NoAccess), Times.Never);
        }

        [Test]
        public void TestWhichShouldAlwaysFail()
        {
            Assert.Fail("failed");
        }
    }
}
