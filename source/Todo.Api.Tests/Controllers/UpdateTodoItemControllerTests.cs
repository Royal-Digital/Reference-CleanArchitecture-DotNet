using System;
using System.Net;
using System.Net.Http;
using Microsoft.Owin.Testing;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Api.Controllers;
using Todo.Domain.Model;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.UseCase;

namespace Todo.Api.Tests.Controllers
{
    [TestFixture]
    public class UpdateTodoItemControllerTests
    {
        [Test]
        public void Execute_WhenValidInputMessage_ShouldReturnSuccess()
        {
            //---------------Arrange-------------------
            var requestUri = "todo/update";
            var itemModel = CreateTodoItemModelWithId(Guid.NewGuid());

            using (var testServer = CreateTestServer())
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.PutAsJsonAsync(requestUri, itemModel).Result;
                //---------------Assert-------------------
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }            
        }

        [Test]
        public void Execute_WhenModelIdEmpty_ShouldReturnUnprocessableEntityCode()
        {
            //---------------Arrange-------------------
            var requestUri = "todo/update";
            var itemModel = CreateTodoItemModelWithId(Guid.Empty);

            using (var testServer = CreateTestServer())
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.PutAsJsonAsync(requestUri, itemModel).Result;
                //---------------Assert-------------------
                Assert.AreEqual((HttpStatusCode)422, response.StatusCode);
            }
        }

        private TodoItemModel CreateTodoItemModelWithId(Guid id)
        {
            var itemModel = new TodoItemModel
            {
                DueDate = DateTime.Today,
                IsCompleted = true,
                Id = id,
                ItemDescription = "Updated task"
            };
            return itemModel;
        }

        private TestServer CreateTestServer()
        {
            var useCase = CreateUpdateTodoItemUseCase();
            var testServer = new TestServerBuilder<UpdateTodoItemController>()
                .WithInstanceRegistration<IUpdateTodoItemUseCase>(useCase)
                .Build();
            return testServer;
        }

        private UpdateTodoItemUseCase CreateUpdateTodoItemUseCase()
        {
            var respository = Substitute.For<ITodoRepository>();
            var useCase = new UpdateTodoItemUseCase(respository);
            return useCase;
        }
    }
}
