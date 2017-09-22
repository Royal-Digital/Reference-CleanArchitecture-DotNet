using System;
using System.Net;
using Microsoft.Owin.Testing;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Api.Controllers.Comment;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.Entities;
using Todo.UseCase.Comment;

namespace Todo.Api.Tests.Controllers.Comment
{
    [TestFixture]
    public class DeleteCommentControllerTests
    {
        [Test]
        public void Execute_WhenValidCommentId_ShouldReturnSuccess()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();
            var requestUri = $"comment/delete/{id}/";

            using (var testServer = CreateTestServer(true))
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.DeleteAsync(requestUri).Result;
                //---------------Assert-------------------
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public void Execute_WhenInvalidCommentId_ShouldReturnUnprocessableEntity()
        {
            //---------------Arrange-------------------
            var id = Guid.Empty;
            var requestUri = $"comment/delete/{id}";

            using (var testServer = CreateTestServer(false))
            {
                var client = TestHttpClientFactory.CreateClient(testServer);
                //---------------Act-------------------
                var response = client.DeleteAsync(requestUri).Result;
                //---------------Assert-------------------
                Assert.AreEqual((HttpStatusCode)422, response.StatusCode);
            }
        }

        private TestServer CreateTestServer(bool canDelete)
        {
            var repository = Substitute.For<ICommentRepository>();
            repository.Delete(Arg.Any<TodoComment>()).Returns(canDelete);
            var useCase = new DeleteCommentUseCase(repository);
            var testServer = new TestServerBuilder<DeleteCommentController>()
                .WithInstanceRegistration<IDeleteCommentUseCase>(useCase)
                .Build();
            return testServer;
        }
    }
}
