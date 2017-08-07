using System;
using System.Collections.Generic;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.Messages;
using Todo.TestUtils;

namespace Todo.UseCase.Tests
{
    [TestFixture]
    public class CreateTodoItemUseCaseTests
    {

        [Test]
        public void Ctor_WhenNullTodoRepository_ShouldThrowArgumentNullException()
        {
            //---------------Set up test pack-------------------
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.Throws<ArgumentNullException>(() => { new CreateTodoItemUseCase(null); });
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Execute_WhenEmptyOrWhitespaceItemDescription_ShouldReturnErrorMessage(string itemDescription)
        {
            //---------------Set up test pack-------------------
            var expected = new List<string> {"ItemDescription cannot be empty or null"};
            var presenter = new PropertyPresenter<CreateTodoItemOuputMessage, ErrorOutputMessage>();
            var usecase = new CreateTodoUseCaseTestDataBuilder().Build();
            var message = CreateTodoItemMessage(itemDescription);
            //---------------Execute Test ----------------------
            usecase.Execute(message, presenter);
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, presenter.ErrorContent.Errors);
        }

        [Test]
        public void Execute_WhenInputMessageContainsValidData_ShouldReturnItemId()
        {
            //---------------Set up test pack-------------------
            var id = "100";
            var expected = "100";
            var presenter = new PropertyPresenter<CreateTodoItemOuputMessage, ErrorOutputMessage>();
            var usecase = new CreateTodoUseCaseTestDataBuilder()
                            .WithCreateId(id)
                            .Build();
            var message = CreateTodoItemMessage("stuff to get done!");
            //---------------Execute Test ----------------------
            usecase.Execute(message, presenter);
            //---------------Test Result -----------------------
            Assert.AreEqual(expected, presenter.SuccessContent.Id);
        }


        private CreateTodoItemInputMessage CreateTodoItemMessage(string itemDescription)
        {
            var message = new CreateTodoItemInputMessage
            {
                ItemDescription = itemDescription,
                CompletionDate = DateTime.Parse("2017-01-01")
            };
            return message;
        }

    }
}
