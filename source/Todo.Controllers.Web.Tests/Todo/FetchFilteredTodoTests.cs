using System.Collections.Generic;
using System.Net;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Fetch;
using Todo.Controllers.Web.Todo;
using Todo.Boundary;
using System.Net.Http;
using Todo.Boundary.Todo.Fetch.Filtered;
using Todo.Domain.Todo.Fetch;

namespace Todo.Controllers.Web.Tests.Todo
{
    [TestFixture]
    public class FetchFilteredTodoTests
    {
        [Test]
        public void Execute_WhenNoFilterArguments_ShouldReturnOk()
        {
            //---------------Arrange-------------------
            var requestUri = "todo/fetch";
            var args = new TodoFilterInput() { IncludedCompleted = false };

            var useCase = CreateFetchTodoCollectionUseCase();
            var testServer = new TestServerBuilder<FetchFilteredTodo>()
                .WithInstanceRegistration<IFetchFilteredTodoUseCase>(useCase)
                .Build();
            
            using (testServer)
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.PostAsJsonAsync(requestUri,args).Result;
                //---------------Assert-------------------
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }            
        }

        private IFetchFilteredTodoUseCase CreateFetchTodoCollectionUseCase()
        {
            var todoRepository = CreateTodoRepository();
            var useCase = new FetchFilteredTodoUseCase(todoRepository);
            return useCase;
        }

        private ITodoRepository CreateTodoRepository()
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.FetchAll().Returns(new List<TodoTo>());
            return repository;
        }
    }
}
