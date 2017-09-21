using System;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.UseCaseMessages;

namespace Todo.UseCase.Tests
{
    [TestFixture]
    public class CreateCommentUseCaseTests
    {
        [Test]
        public void Execute_WhenValidTodoItemId_ShouldReturnSuccess()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();
            var usecase = new CreateCommentUseCase();
            var input = new CreateCommentInput {TodoItemId = id, Comment = "a comment"};
            var presenter = new PropertyPresenter<CreateCommentOuput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.AreEqual(id, presenter.SuccessContent.TodoItemId);
            Assert.AreEqual("Successfully added the comment", presenter.SuccessContent.Message);
        }

        [Test]
        public void Execute_WhenInvalidTodoItemId_ShouldReturnError()
        {
            //---------------Arrange-------------------
            var usecase = new CreateCommentUseCase();
            var input = new CreateCommentInput { TodoItemId = Guid.Empty, Comment = "a comment" };
            var presenter = new PropertyPresenter<CreateCommentOuput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual("Invalid item Id", presenter.ErrorContent.Errors[0]);
        }

        [TestCase(" ")]
        [TestCase("")]
        [TestCase(null)]
        public void Execute_WhenInvalidComment_ShouldReturnError(string comment)
        {
            //---------------Arrange-------------------
            var usecase = new CreateCommentUseCase();
            var input = new CreateCommentInput { TodoItemId = Guid.NewGuid(), Comment = comment };
            var presenter = new PropertyPresenter<CreateCommentOuput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual("Missing comment", presenter.ErrorContent.Errors[0]);
        }
    }
}
