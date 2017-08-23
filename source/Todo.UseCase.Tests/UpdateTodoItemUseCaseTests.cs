using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.Messages;
using Todo.Domain.Model;
using Todo.Domain.Repository;

namespace Todo.UseCase.Tests
{
    [TestFixture]
    public class UpdateTodoItemUseCaseTests
    {

        [Test]
        public void Ctor_WhenNullTodoRepository_ShouldThrowArgumentNullException()
        {
            //---------------Set up test pack-------------------
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.Throws<ArgumentNullException>(() => { new UpdateTodoItemUseCase(null); });
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Execute_WhenEmptyOrWhitespaceItemDescription_ShouldReturnErrorMessage(string itemDescription)
        {
            //---------------Set up test pack-------------------
            var expected = "ItemDescription cannot be null or empty";
            var itemModel = CreateValidUpdateMessage(itemDescription);
            var usecase = CreateUpdateTodoItemUseCase();
            var presenter = new PropertyPresenter<string, ErrorOutputMessage>();
            //---------------Execute Test ----------------------
            usecase.Execute(itemModel, presenter);
            //---------------Test Result -----------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual(expected, presenter.ErrorContent.Errors.First());
        }

        [Test]
        public void Execute_WhenInputMessageContainsValidData_ShouldReturnItemId()
        {
            //---------------Set up test pack-------------------
            var itemModel = CreateValidUpdateMessage("Updated task");
            var usecase = CreateUpdateTodoItemUseCase();
            var presenter = new PropertyPresenter<string, ErrorOutputMessage>();
            //---------------Execute Test ----------------------
            usecase.Execute(itemModel, presenter);
            //---------------Test Result -----------------------
            Assert.AreEqual("updated", presenter.SuccessContent);
        }

        private UpdateTodoItemInputMessage CreateValidUpdateMessage(string itemDescription)
        {
            return new UpdateTodoItemInputMessage
            {
                Id = Guid.NewGuid(),
                DueDate = DateTime.Today,
                ItemDescription = itemDescription,
                IsCompleted = true
            };
        }

        private UpdateTodoItemUseCase CreateUpdateTodoItemUseCase()
        {
            var respository = Substitute.For<ITodoRepository>();
            var usecase = new UpdateTodoItemUseCase(respository);
            return usecase;
        }

    }
}
