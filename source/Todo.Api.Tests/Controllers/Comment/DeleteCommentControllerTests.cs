using System;
using System.Net;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Boundry.Comment.Delete;
using Todo.Domain.Tests.Comment.Delete;
using Todo.Web.Controllers.Comment;

namespace Todo.Web.Controllers.Tests.Controllers.Comment
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
            var useCase = new DeleteCommentUseCaseTestDataBuilder()
                            .WithDeleteResult(canDelete)
                            .Build();
            var testServer = new TestServerBuilder<DeleteCommentController>()
                .WithInstanceRegistration<IDeleteCommentUseCase>(useCase)
                .Build();
            return testServer;
        }
    }
}
