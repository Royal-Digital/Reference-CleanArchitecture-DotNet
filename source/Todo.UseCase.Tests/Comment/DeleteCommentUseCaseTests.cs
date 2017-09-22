using System;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;
using Todo.Entities;
using Todo.TestUtils;
using Todo.UseCase.Comment;

namespace Todo.UseCase.Tests.Comment
{
    [TestFixture]
    public class DeleteCommentUseCaseTests
    {
        [Test]
        public void Execute_WhenValidTodoItemId_ShouldReturnSuccess()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();

            var usecase = CreateDeleteCommentUseCase(true);
            var input = new DeleteCommentInput{Id = id};
            var presenter = CreatePropertyPresenter();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.AreEqual(id, presenter.SuccessContent.Id);
            Assert.AreEqual("Commented deleted successfuly", presenter.SuccessContent.Message);
        }

        [Test]
        public void Execute_WhenInvalidTodoItemId_ShouldReturnError()
        {
            //---------------Arrange-------------------
            var usecase = CreateDeleteCommentUseCase(true);
            var input = new DeleteCommentInput { Id = Guid.Empty };
            var presenter = CreatePropertyPresenter();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual("Invalid comment Id", presenter.ErrorContent.Errors[0]);
        }

        [Test]
        public void Execute_WhenTodoItemIdNotFound_ShouldReturnError()
        {
            //---------------Arrange-------------------
            var usecase = CreateDeleteCommentUseCase(false);
            var id = Guid.NewGuid();
            var input = new DeleteCommentInput { Id = id };
            var presenter = CreatePropertyPresenter();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual("Could not locate item Id", presenter.ErrorContent.Errors[0]);
        }

        private PropertyPresenter<DeleteCommentOutput, ErrorOutputMessage> CreatePropertyPresenter()
        {
            var presenter = new PropertyPresenter<DeleteCommentOutput, ErrorOutputMessage>();
            return presenter;
        }

        private IDeleteCommentUseCase CreateDeleteCommentUseCase(bool canDelete)
        {
            var repository = Substitute.For<ICommentRepository>();
            repository.Delete(Arg.Any<TodoComment>()).Returns(canDelete);
            var usecase = new DeleteCommentUseCase(repository);
            return usecase;
        }
    }
}
