using System.Collections.Generic;
using System.Net;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Fetch;
using Todo.Controllers.Web.Todo;
using Todo.Domain.Todo.Fetch;

namespace Todo.Controllers.Web.Tests.Todo
{
    [TestFixture]
    public class FetchTodoItemsTests
    {
        [Test]
        public void Execute_WhenFetchAll_ShouldReturnOk()
        {
            //---------------Arrange-------------------
            var requestUri = "todo/fetch/all";
            var useCase = CreateFetchTodoCollectionUseCase();
            var testServer = new TestServerBuilder<FetchTodoItem>()
                .WithInstanceRegistration<IFetchTodoCollectionUseCase>(useCase)
                .Build();
            
            using (testServer)
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.GetAsync(requestUri).Result;
                //---------------Assert-------------------
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }            
        }

        private IFetchTodoCollectionUseCase CreateFetchTodoCollectionUseCase()
        {
            var todoRepository = CreateTodoRepository();
            var useCase = new FetchTodoCollectionUseCase(todoRepository);
            return useCase;
        }

        private ITodoRepository CreateTodoRepository()
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.FetchAll().Returns(new List<TodoItemTo>());
            return repository;
        }
    }
}
