﻿using System;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Boundary.Comment.Create;

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

            var testContext = new CreateCommentUseCaseTestDataBuilder().WithCommentId(commentId).Build();
            var usecase = testContext.UseCase;
            var input = new CreateCommentInput {TodoItemId = itemId, Comment = "a comment"};
            var presenter = new PropertyPresenter<CreateCommentOutput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.AreEqual(commentId, presenter.SuccessContent.Id);
            testContext.Repository.Received(1).Persist();
        }

        [Test]
        public void Execute_WhenInvalidTodoItemId_ShouldReturnError()
        {
            //---------------Arrange-------------------
            var testContext = new CreateCommentUseCaseTestDataBuilder().Build();
            var usecase = testContext.UseCase;
            var input = new CreateCommentInput { TodoItemId = Guid.Empty, Comment = "a comment" };
            var presenter = new PropertyPresenter<CreateCommentOutput, ErrorOutputMessage>();
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
            var testContext = new CreateCommentUseCaseTestDataBuilder().Build();
            var usecase = testContext.UseCase;
            var input = new CreateCommentInput { TodoItemId = Guid.NewGuid(), Comment = comment };
            var presenter = new PropertyPresenter<CreateCommentOutput, ErrorOutputMessage>();
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
            var testContext = new CreateCommentUseCaseTestDataBuilder().WithTodoItemTo(null).Build();
            var usecase = testContext.UseCase;
            var input = new CreateCommentInput { TodoItemId = Guid.NewGuid(), Comment = "a comment" };
            var presenter = new PropertyPresenter<CreateCommentOutput, ErrorOutputMessage>();
            //---------------Act----------------------
            usecase.Execute(input, presenter);
            //---------------Assert-----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual("Invalid item Id", presenter.ErrorContent.Errors[0]);
        }
    }
}
