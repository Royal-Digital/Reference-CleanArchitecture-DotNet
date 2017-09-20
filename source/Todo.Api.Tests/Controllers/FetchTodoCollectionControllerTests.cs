using System.Collections.Generic;
using System.Net;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Api.Controllers;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.Entities;
using Todo.UseCase;

namespace Todo.Api.Tests.Controllers
{
    [TestFixture]
    public class FetchTodoCollectionControllerTests
    {
        [Test]
        public void Execute_WhenFetchAll_ShouldReturnOk()
        {
            //---------------Arrange-------------------
            var requestUri = "todo/fetch/all";
            var useCase = CreateFetchTodoCollectionUseCase();
            var testServer = new TestServerBuilder<FetchTodoItemController>()
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

        private static FetchTodoCollectionUseCase CreateFetchTodoCollectionUseCase()
        {
            var repository = CreateTodoRepository();
            var useCase = new FetchTodoCollectionUseCase(repository);
            return useCase;
        }

        private static ITodoRepository CreateTodoRepository()
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.FetchAll().Returns(new List<TodoItem>());
            return repository;
        }
    }
}
