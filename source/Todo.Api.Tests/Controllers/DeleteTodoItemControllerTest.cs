using System;
using System.Net;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Api.Controllers;
using Todo.Domain.UseCase;
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
            var useCase = new DeleteTodoItemUseCase();
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

        //[Test]
        //public void Execute_WhenInvalidItemId_ShouldReturnUnprocessableEntityCode()
        //{
        //    //---------------Arrange-------------------
        //    var requestUri = "todo/create";
        //    var inputMessage = CreateTodoItemMessage(null, "2017-01-01");
        //    var useCase = new CreateTodoUseCaseTestDataBuilder().Build();
        //    var testServer = new TestServerBuilder<CreateTodoItemController>()
        //        .WithInstanceRegistration<ICreateTodoItemUseCase>(useCase)
        //        .Build();

        //    using (testServer)
        //    {
        //        var client = TestHttpClientFactory.CreateClient(testServer);
        //        //---------------Act-------------------
        //        var response = client.PostAsJsonAsync(requestUri, inputMessage).Result;
        //        //---------------Assert-------------------
        //        Assert.AreEqual((HttpStatusCode)422, response.StatusCode);
        //    }
        //}
    }
}
