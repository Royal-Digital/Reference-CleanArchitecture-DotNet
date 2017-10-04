using System;
using System.Net;
using System.Net.Http;
using Microsoft.Owin.Testing;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Update;
using Todo.Controllers.Web.Todo;
using Todo.Domain.Todo;
using Todo.Domain.Todo.Update;

namespace Todo.Controllers.Web.Tests.Todo
{
    [TestFixture]
    public class UpdateTodoItemTests
    {
        [Test]
        public void Execute_WhenValidInputMessage_ShouldReturnSuccess()
        {
            //---------------Arrange-------------------
            var requestUri = "todo/update";
            var itemModel = CreateTodoItemWithId(Guid.NewGuid());

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
            var itemModel = CreateTodoItemWithId(Guid.Empty);

            using (var testServer = CreateTestServer())
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.PutAsJsonAsync(requestUri, itemModel).Result;
                //---------------Assert-------------------
                Assert.AreEqual((HttpStatusCode)422, response.StatusCode);
            }
        }

        private TodoItem CreateTodoItemWithId(Guid id)
        {
            var itemModel = new TodoItem
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
            var testServer = new TestServerBuilder<UpdateTodoItem>()
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
