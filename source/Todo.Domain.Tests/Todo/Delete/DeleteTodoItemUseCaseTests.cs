using System;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Boundary.Todo.Delete;
using Todo.Domain.Todo.Delete;

namespace Todo.Domain.Tests.Todo.Delete
{
    [TestFixture]
    public class DeleteTodoItemUseCaseTests
    {
        
        [Test]
        public void Ctor_WhenNullTodoRepository_ShouldThrowArgumentNullException()
        {
            //---------------Arrange-------------------
            var expected = "repository";
            //---------------Act-------------------
            var result = Assert.Throws<ArgumentNullException>(() => { new DeleteTodoUseCase(null); });
            //---------------Assert-------------------
            Assert.AreEqual(expected, result.ParamName);
        }

        [Test]
        public void Execute_WhenIdExist_ShouldReturnEmptyResponse()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();
            var presenter = new ResultFreePropertyPresenter<ErrorOutputMessage>();
            var testContext = new DeleteTodoItemUseCaseTestDataBuilder().WithDeleteResult(true).Build();
            var usecase = testContext.UseCase;

            var message = new DeleteTodoInput {Id = id};
            //---------------Act-------------------
            usecase.Execute(message, presenter);
            //---------------Assert-------------------
            Assert.IsFalse(presenter.IsErrorResponse());
            testContext.Repository.Received(1).Save();
        }

        [Test]
        public void Execute_WhenIdDoesNotExist_ShouldReturnErrorMessage()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();
            var expected = $"Could not locate item with id [{id}]";
            var presenter = new ResultFreePropertyPresenter<ErrorOutputMessage>();
            var testContext = new DeleteTodoItemUseCaseTestDataBuilder().WithDeleteResult(false).Build();
            var usecase = testContext.UseCase;
            var message = new DeleteTodoInput { Id = id };
            //---------------Act-------------------
            usecase.Execute(message, presenter);
            //---------------Assert-------------------
            Assert.AreEqual(expected, presenter.ErrorContent.Errors[0]);
        }
    }
}
