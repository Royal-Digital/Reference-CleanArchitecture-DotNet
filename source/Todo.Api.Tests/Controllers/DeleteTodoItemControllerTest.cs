using System;
using System.Net;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Api.Controllers;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.TestUtils;
using Todo.UseCase;

namespace Todo.Api.Tests.Controllers
{
    [TestFixture]
    public class DeleteTodoItemControllerTest
    {
        [Test]
        public void Execute_WhenValidItemId_ShouldReturnSuccess()
        {
            //---------------Arrange-------------------
            var deleteId = Guid.NewGuid();
            var requestUri = $"todo/delete/{deleteId}";
            var repository = CreateTodoRepository(true);
            var useCase = new DeleteTodoItemUseCase(repository);
            var testServer = new TestServerBuilder<DeleteTodoItemController>()
                .WithInstanceRegistration<IDeleteTodoItemUseCase>(useCase)
                .Build();
            
            using (testServer)
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.DeleteAsync(requestUri).Result;
                //---------------Assert-------------------
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }            
        }

        [Test]
        public void Execute_WhenInvalidItemId_ShouldReturnUnprocessableEntityCode()
        {
            //---------------Arrange-------------------
            var deleteId = Guid.NewGuid();
            var requestUri = $"todo/delete/{deleteId}";
            var repository = CreateTodoRepository(false);
            var useCase = new DeleteTodoItemUseCase(repository);
            var testServer = new TestServerBuilder<DeleteTodoItemController>()
                .WithInstanceRegistration<IDeleteTodoItemUseCase>(useCase)
                .Build();

            using (testServer)
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.DeleteAsync(requestUri).Result;
                //---------------Assert-------------------
                Assert.AreEqual((HttpStatusCode)422, response.StatusCode);
            }
        }

        private ITodoRepository CreateTodoRepository(bool isDeleted)
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.DeleteItem(Arg.Any<Guid>()).Returns(isDeleted);
            return repository;
        }
    }
}
