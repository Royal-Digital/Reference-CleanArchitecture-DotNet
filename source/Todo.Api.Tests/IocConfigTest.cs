using System.Web.Http;
using NUnit.Framework;
using Todo.Api;

namespace Todo.Controllers.Web.Tests
{
    [TestFixture]
    public class IocTests
    {
        [Test]
        public void Configure_ShouldNotThrowException()
        {
            //---------------Arrange-------------------
            var configuration = new HttpConfiguration();
            //---------------Act-------------------
            //---------------Assert-------------------
            Assert.DoesNotThrow(() => IocConfig.Configure(configuration));
        }
    }
}
