using System;
using System.Collections.Generic;
using System.Net;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.TestUtils.Builders;
using TddBuddy.CleanArchitecture.TestUtils.Factories;
using Todo.Api.Controllers.Todo;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;
using Todo.Entities;
using Todo.UseCase.Todo;

namespace Todo.Api.Tests.Controllers.Todo
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

        private FetchTodoCollectionUseCase CreateFetchTodoCollectionUseCase()
        {
            var todoRepository = CreateTodoRepository();
            var commentsRepository = Substitute.For<ICommentRepository>();
            commentsRepository.FindForItem(Arg.Any<Guid>()).Returns(new List<TodoComment>());
            var useCase = new FetchTodoCollectionUseCase(todoRepository, commentsRepository);
            return useCase;
        }

        private ITodoRepository CreateTodoRepository()
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.FetchAll().Returns(new List<TodoItem>());
            return repository;
        }
    }
}
