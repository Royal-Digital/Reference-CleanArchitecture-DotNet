using System;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.Repository;
using Todo.Domain.UseCaseMessages;
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

            var repository = Substitute.For<ICommentRepository>();
            var usecase = new DeleteCommentUseCase(repository);
            var input = new DeleteCommentInput{Id = id};
            var presenter = new PropertyPresenter<DeleteCommentOutput, ErrorOutputMessage>();
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
            var repository = Substitute.For<ICommentRepository>();
            var usecase = new DeleteCommentUseCase(repository);
            var input = new DeleteCommentInput { Id = Guid.Empty };
            var presenter = new PropertyPresenter<DeleteCommentOutput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual("Invalid comment Id", presenter.ErrorContent.Errors[0]);
        }

        //[Test]
        //public void Execute_WhenTodoItemIdNotFound_ShouldReturnError()
        //{
        //    //---------------Arrange-------------------
        //    var usecase = new CreateCommentUseCaseTestDataBuilder().WithTodoItem(null).Build();
        //    var input = new CreateCommentInput { TodoItemId = Guid.NewGuid(), Comment = "a comment" };
        //    var presenter = new PropertyPresenter<CreateCommentOuput, ErrorOutputMessage>();
        //    //---------------Act----------------------
        //    usecase.Execute(input, presenter);
        //    //---------------Assert-----------------------
        //    Assert.IsTrue(presenter.ErrorContent.HasErrors);
        //    Assert.AreEqual("Invalid item Id", presenter.ErrorContent.Errors[0]);
        //}
    }
}
