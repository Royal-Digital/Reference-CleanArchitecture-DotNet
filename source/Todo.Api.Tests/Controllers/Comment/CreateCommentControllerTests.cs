using System;
using System.Net;
using System.Net.Http;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Api.Controllers;
using Todo.Api.Controllers.Comment;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;
using Todo.TestUtils;

namespace Todo.Api.Tests.Controllers.Comment
{
    [TestFixture]
    public class CreateCommentControllerTests
    {
        [Test]
        public void Execute_WhenValidTodoItemId_ShouldReturnSuccess()
        {
            //---------------Arrange-------------------
            var requestUri = "comment/create";
            var inputMessage = new CreateCommentInput {TodoItemId = Guid.NewGuid(), Comment = "a comment"};

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
        public void Execute_WhenInvalidTodoItemId_ShouldReturnUnprocessableEntity()
        {
            //---------------Arrange-------------------
            var requestUri = "comment/create";
            var inputMessage = new CreateCommentInput { TodoItemId = Guid.Empty, Comment = "a comment" };

            using (var testServer = CreateTestServer())
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.PostAsJsonAsync(requestUri, inputMessage).Result;
                //---------------Assert-------------------
                Assert.AreEqual((HttpStatusCode)422, response.StatusCode);
            }
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Execute_WhenNullOrWhitespaceComment_ShouldReturnUnprocessableEntity(string comment)
        {
            //---------------Arrange-------------------
            var requestUri = "comment/create";
            var inputMessage = new CreateCommentInput { TodoItemId = Guid.NewGuid(), Comment = comment };

            using (var testServer = CreateTestServer())
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.PostAsJsonAsync(requestUri, inputMessage).Result;
                //---------------Assert-------------------
                Assert.AreEqual((HttpStatusCode)422, response.StatusCode);
            }
        }

        private TestServer CreateTestServer()
        {
            var useCase = new CreateCommentUseCaseTestDataBuilder().Build();
            var testServer = new TestServerBuilder<CreateCommentController>()
                .WithInstanceRegistration<ICreateCommentUseCase>(useCase)
                .Build();
            return testServer;
        }
    }
}
