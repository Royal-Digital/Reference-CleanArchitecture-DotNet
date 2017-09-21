using System;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.Repository;
using Todo.Domain.UseCaseMessages;
using Todo.UseCase.Todo;

namespace Todo.UseCase.Tests.Todo
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
            var result = Assert.Throws<ArgumentNullException>(() => { new DeleteTodoItemUseCase(null); });
            //---------------Assert-------------------
            Assert.AreEqual(expected, result.ParamName);
        }

        [Test]
        public void Execute_WhenIdExist_ShouldReturnSuccessMessage()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();
            var expected = "Deleted item";
            var presenter = new PropertyPresenter<DeleteTodoItemOutput, ErrorOutputMessage>();
            var repository = CreateTodoRepository(true);
            var usecase = new DeleteTodoItemUseCase(repository);
            var message = new DeleteTodoItemInput {Id = id};
            //---------------Act-------------------
            usecase.Execute(message, presenter);
            //---------------Assert-------------------
            Assert.AreEqual(id, presenter.SuccessContent.Id);
            Assert.AreEqual(expected, presenter.SuccessContent.Message);
        }

        [Test]
        public void Execute_WhenIdDoesNotExist_ShouldReturnErrorMessage()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();
            var expected = $"Could not locate item with id [{id}]";
            var presenter = new PropertyPresenter<DeleteTodoItemOutput, ErrorOutputMessage>();
            var repository = CreateTodoRepository(false);
            var usecase = new DeleteTodoItemUseCase(repository);
            var message = new DeleteTodoItemInput { Id = id };
            //---------------Act-------------------
            usecase.Execute(message, presenter);
            //---------------Assert-------------------
            Assert.AreEqual(expected, presenter.ErrorContent.Errors[0]);
        }

        private static ITodoRepository CreateTodoRepository(bool isDeleted)
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.Delete(Arg.Any<Guid>()).Returns(isDeleted);
            return repository;
        }
    }
}
