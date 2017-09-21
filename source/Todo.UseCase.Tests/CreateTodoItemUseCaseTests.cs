using System;
using System.Collections.Generic;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.UseCaseMessages;
using Todo.TestUtils;

namespace Todo.UseCase.Tests
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
            var result = Assert.Throws<ArgumentNullException>(() => { new CreateTodoItemUseCase(null); });
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
            var presenter = new PropertyPresenter<CreateTodoItemOuput, ErrorOutputMessage>();
            var usecase = new CreateTodoUseCaseTestDataBuilder().Build();
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
            var presenter = new PropertyPresenter<CreateTodoItemOuput, ErrorOutputMessage>();
            var usecase = new CreateTodoUseCaseTestDataBuilder()
                            .WithModelId(id)
                            .Build();
            var message = CreateTodoItemMessage("stuff to get done!");
            //---------------Act-------------------
            usecase.Execute(message, presenter);
            //---------------Assert-------------------
            AssertCorrectCommentId(presenter, expected);
        }

        private static void AssertCorrectCommentId(PropertyPresenter<CreateTodoItemOuput, ErrorOutputMessage> presenter, Guid expected)
        {
            var commentId = presenter.SuccessContent.Id;
            Assert.AreEqual(expected, commentId);
        }

        private CreateTodoItemInput CreateTodoItemMessage(string itemDescription)
        {
            var message = new CreateTodoItemInput
            {
                ItemDescription = itemDescription,
                DueDate = DateTime.Parse("2017-01-01")
            };
            return message;
        }

    }
}
