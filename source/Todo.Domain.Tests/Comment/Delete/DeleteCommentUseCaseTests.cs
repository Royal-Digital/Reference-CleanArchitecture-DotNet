using System;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Boundary.Comment.Delete;

namespace Todo.Domain.Tests.Comment.Delete
{
    [TestFixture]
    public class DeleteCommentUseCaseTests
    {
        [Test]
        public void Execute_WhenValidTodoItemId_ShouldReturnSuccess()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();

            var testContext = new DeleteCommentUseCaseTestDataBuilder()
                .WithDeleteResult(true)
                .Build();
            var usecase = testContext.UseCase;

            var input = new DeleteCommentInput{Id = id};
            var presenter = CreatePropertyPresenter();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsFalse(presenter.IsErrorResponse());
            testContext.Repository.Received(1).Delete(Arg.Is<Guid>(x=>x == id));
            testContext.Repository.Received(1).Save();
        }

        [Test]
        public void Execute_WhenInvalidTodoItemId_ShouldReturnError()
        {
            //---------------Arrange-------------------

            var testContext = new DeleteCommentUseCaseTestDataBuilder()
                .WithDeleteResult(true)
                .Build();
            var usecase = testContext.UseCase;
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

            var testContext = new DeleteCommentUseCaseTestDataBuilder()
                .WithDeleteResult(false)
                .Build();
            var usecase = testContext.UseCase;
            var id = Guid.NewGuid();
            var input = new DeleteCommentInput { Id = id };
            var presenter = CreatePropertyPresenter();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual("Could not locate item Id", presenter.ErrorContent.Errors[0]);
        }

        private ResultFreePropertyPresenter<ErrorOutputMessage> CreatePropertyPresenter()
        {
            var presenter = new ResultFreePropertyPresenter<ErrorOutputMessage>();
            return presenter;
        }
    }
}
