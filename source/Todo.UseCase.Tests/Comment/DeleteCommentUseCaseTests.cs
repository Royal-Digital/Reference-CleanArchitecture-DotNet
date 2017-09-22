using System;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;
using Todo.TestUtils;

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

            var usecase = new DeleteCommentUseCaseTestDataBuilder()
                            .WithDeleteResult(true)
                            .Build();
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
            var usecase = new DeleteCommentUseCaseTestDataBuilder()
                            .WithDeleteResult(true)
                            .Build();
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
            var usecase = new DeleteCommentUseCaseTestDataBuilder()
                            .WithDeleteResult(false)
                            .Build();
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
    }
}
