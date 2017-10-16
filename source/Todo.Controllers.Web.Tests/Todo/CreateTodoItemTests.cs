using System;
using System.Net;
using System.Net.Http;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Boundary.Todo.Create;
using Todo.Controllers.Web.Todo;
using Todo.Domain.Tests.Todo.Create;

namespace Todo.Controllers.Web.Tests.Todo
{
    [TestFixture]
    public class CreateTodoItemTests
    {
        [Test]
        public void Execute_WhenValidInputMessage_ShouldReturnSuccess()
        {
            //---------------Arrange-------------------
            var requestUri = "todo/create";
            var inputMessage = CreateTodoItemMessage("A new thing to do","2017-01-01");

            using (var testServer = CreateTestServer())
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.PostAsJsonAsync(requestUri, inputMessage).Result;
                //---------------Assert-------------------
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }            
        }

        [Test]
        public void Execute_WhenInputMessageContainsInvalidData_ShouldReturnUnprocessableEntityCode()
        {
            //---------------Arrange-------------------
            var requestUri = "todo/create";
            var inputMessage = CreateTodoItemMessage(null, "2017-01-01");

            using (var testServer = CreateTestServer())
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.PostAsJsonAsync(requestUri, inputMessage).Result;
                //---------------Assert-------------------
                Assert.AreEqual((HttpStatusCode)422, response.StatusCode);
            }
        }

        private static TestServer CreateTestServer()
        {
            var testContext = new CreateTodoUseCaseTestDataBuilder().Build();
            var testServer = new TestServerBuilder<CreateTodoItem>()
                .WithInstanceRegistration<ICreateTodoUseCase>(testContext.UseCase)
                .Build();
            return testServer;
        }

        private CreateTodoInput CreateTodoItemMessage(string itemText, string itemDueDate)
        {
            var inputMessage = new CreateTodoInput
            {
                ItemDescription = itemText,
                DueDate = DateTime.Parse(itemDueDate)
            };
            return inputMessage;
        }
    }
}
