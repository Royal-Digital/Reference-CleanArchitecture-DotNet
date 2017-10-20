using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Boundary.Todo.Create;
using Todo.Domain.Todo.Create;

namespace Todo.Domain.Tests.Todo.Create
{
    [TestFixture]
    public class CreateTodoItemUseCaseTests
    {
        [Test]
        public void Ctor_WhenNullTodoRepository_ShouldThrowArgumentNullException()
        {
            //---------------Arrange-------------------
            var expected = "respository";
            //---------------Act-------------------
            var result = Assert.Throws<ArgumentNullException>(() => { new CreateTodoUseCase(null); });
            //---------------Assert-------------------
            Assert.AreEqual(expected, result.ParamName);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Execute_WhenEmptyOrWhitespaceItemDescription_ShouldReturnErrorMessage(string itemDescription)
        {
            //---------------Arrange-------------------
            var expected = new List<string> {"ItemDescription cannot be empty or null"};
            var presenter = new PropertyPresenter<CreateTodoOutput, ErrorOutputMessage>();
            var testContext = new CreateTodoUseCaseTestDataBuilder().Build();
            var usecase = testContext.UseCase;
            var message = CreateTodoItemMessage(itemDescription);
            //---------------Act-------------------
            usecase.Execute(message, presenter);
            //---------------Assert-------------------
            Assert.AreEqual(expected, presenter.ErrorContent.Errors);
        }

        [Test]
        public void Execute_WhenInputMessageContainsValidData_ShouldReturnItemId()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();
            var expected = id;
            var presenter = new PropertyPresenter<CreateTodoOutput, ErrorOutputMessage>();
            var testContext = new CreateTodoUseCaseTestDataBuilder()
                            .WithTodoItemId(id)
                            .Build();
            var usecase = testContext.UseCase;
            var message = CreateTodoItemMessage("stuff to get done!");
            //---------------Act-------------------
            usecase.Execute(message, presenter);
            //---------------Assert-------------------
            AssertCorrectCommentId(presenter, expected);
            testContext.Repository.Received(1).Persist();
        }

        private void AssertCorrectCommentId(PropertyPresenter<CreateTodoOutput, ErrorOutputMessage> presenter, Guid expected)
        {
            var commentId = presenter.SuccessContent.Id;
            Assert.AreEqual(expected, commentId);
        }

        private CreateTodoInput CreateTodoItemMessage(string itemDescription)
        {
            var message = new CreateTodoInput
            {
                ItemDescription = itemDescription,
                DueDate = DateTime.Parse("2017-01-01")
            };
            return message;
        }

    }
}
