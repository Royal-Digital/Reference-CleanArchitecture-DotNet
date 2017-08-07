using System.Net;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Api.Controllers;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.UseCase;

namespace Todo.Api.Tests.Controllers
{
    [TestFixture]
    public class FetchTodoCollectionControllerTests
    {
        [Test]
        public void Execute_WhenFetchAll_ShouldReturnOk()
        {
            //---------------Set up test pack-------------------
            var requestUri = "todo/fetch/all";
            var repository = Substitute.For<ITodoRepository>();
            var useCase = new FetchTodoCollectionUseCase(repository);
            var testServer = new TestServerBuilder<FetchTodoItemController>()
                .WithInstanceRegistration<IFetchTodoCollectionUseCase>(useCase)
                .Build();
            
            using (testServer)
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Execute Test ----------------------
                var response = client.GetAsync(requestUri).Result;
                //---------------Test Result -----------------------
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }            
        }
    }
}
