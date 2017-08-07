using System;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Api.Controllers;
using Todo.Domain.Messages;
using Todo.Domain.UseCase;
using Todo.TestUtils;

namespace Todo.Api.Tests.Controllers
{
    [TestFixture]
    public class CreateTodoItemControllerTests
    {
        [Test]
        public void Execute_WhenValidInputMessage_ShouldReturnSuccess()
        {
            //---------------Set up test pack-------------------
            var requestUri = "todo/create";
            var inputMessage = CreateTodoItemMessage("A new thing to do","2017-01-01");
            var useCase = new CreateTodoUseCaseTestDataBuilder().Build();
            var testServer = new TestServerBuilder<CreateTodoItemController>()
                .WithInstanceRegistration<ICreateTodoItemUseCase>(useCase)
                .Build();
            
            using (testServer)
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Execute Test ----------------------
                var response = client.PostAsJsonAsync(requestUri, inputMessage).Result;
                //---------------Test Result -----------------------
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }            
        }

        [Test]
        public void Execute_WhenInputMessageContainsInvalidData_ShouldReturnUnprocessableEntityCode()
        {
            //---------------Set up test pack-------------------
            var requestUri = "todo/create";
            var inputMessage = CreateTodoItemMessage(null, "2017-01-01");
            var useCase = new CreateTodoUseCaseTestDataBuilder().Build();
            var testServer = new TestServerBuilder<CreateTodoItemController>()
                .WithInstanceRegistration<ICreateTodoItemUseCase>(useCase)
                .Build();

            using (testServer)
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Execute Test ----------------------
                var response = client.PostAsJsonAsync(requestUri, inputMessage).Result;
                //---------------Test Result -----------------------
                Assert.AreEqual((HttpStatusCode)422, response.StatusCode);
            }
        }

        private CreateTodoItemInputMessage CreateTodoItemMessage(string itemText, string itemDueDate)
        {
            var inputMessage = new CreateTodoItemInputMessage
            {
                ItemDescription = itemText,
                CompletionDate = DateTime.Parse(itemDueDate)
            };
            return inputMessage;
        }
    }
}
