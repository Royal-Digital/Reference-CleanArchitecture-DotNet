using System.Web.Http;
using NUnit.Framework;

namespace Todo.Api.Tests
{
    [TestFixture]
    public class IocTests
    {
        [Test]
        public void Configure_ShouldNotThrowException()
        {
            //---------------Set up test pack-------------------
            var configuration = new HttpConfiguration();
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.DoesNotThrow(() => IocConfig.Configure(configuration));
        }
    }
}
