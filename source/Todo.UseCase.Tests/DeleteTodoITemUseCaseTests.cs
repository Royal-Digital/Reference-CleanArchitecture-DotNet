
using System;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.Repository;
using Todo.Domain.UseCaseMessages;
using Todo.TestUtils;

namespace Todo.UseCase.Tests
{
    [TestFixture]
    public class DeleteTodoITemUseCaseTests
    {
        [Test]
        public void Ctor_WhenNullTodoRepository_ShouldThrowArgumentNullException()
        {
            //---------------Arrange-------------------
            //---------------Act-------------------
            var result = Assert.Throws<ArgumentNullException>(() => { new DeleteTodoItemUseCase(null); });
            //---------------Assert-------------------
            Assert.AreEqual("repository", result.ParamName);
        }

        [Test]
        public void Execute_WhenInputMessageContainsValidData_ShouldReturnItemId()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();
            var expected = "deleted item";
            var presenter = new PropertyPresenter<DeleteTodoItemOutput, ErrorOutputMessage>();
            var repository = Substitute.For<ITodoRepository>();
            var usecase = new DeleteTodoItemUseCase(repository);
            var message = new DeleteTodoItemInput {Id = id};
            //---------------Act-------------------
            usecase.Execute(message, presenter);
            //---------------Assert-------------------
            Assert.AreEqual(id, presenter.SuccessContent.Id);
            Assert.AreEqual(expected, presenter.SuccessContent.Message);
        }
    }
}
