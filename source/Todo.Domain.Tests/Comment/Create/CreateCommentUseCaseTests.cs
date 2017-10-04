using System;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Boundry.Comment.Create;
using Todo.Domain.Tests.Todo.Create;

namespace Todo.Domain.Tests.Comment.Create
{
    [TestFixture]
    public class CreateCommentUseCaseTests
    {
        [Test]
        public void Execute_WhenValidTodoItemId_ShouldReturnSuccess()
        {
            //---------------Arrange-------------------
            var commentId = Guid.NewGuid();
            var itemId = Guid.NewGuid();

            var usecase = new CreateCommentUseCaseTestDataBuilder().WithCommentId(commentId).Build();
            var input = new CreateCommentInput {TodoItemId = itemId, Comment = "a comment"};
            var presenter = new PropertyPresenter<CreateCommentOuput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.AreEqual(commentId, presenter.SuccessContent.Id);
        }

        [Test]
        public void Execute_WhenInvalidTodoItemId_ShouldReturnError()
        {
            //---------------Arrange-------------------
            var usecase = new CreateCommentUseCaseTestDataBuilder().Build();
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
            var usecase = new CreateCommentUseCaseTestDataBuilder().Build();
            var input = new CreateCommentInput { TodoItemId = Guid.NewGuid(), Comment = comment };
            var presenter = new PropertyPresenter<CreateCommentOuput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual("Missing comment", presenter.ErrorContent.Errors[0]);
        }

        [Test]
        public void Execute_WhenTodoItemIdNotFound_ShouldReturnError()
        {
            //---------------Arrange-------------------
            var usecase = new CreateCommentUseCaseTestDataBuilder().WithTodoItemOutputTo(null).Build();
            var input = new CreateCommentInput { TodoItemId = Guid.NewGuid(), Comment = "a comment" };
            var presenter = new PropertyPresenter<CreateCommentOuput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual("Invalid item Id", presenter.ErrorContent.Errors[0]);
        }
    }
}
