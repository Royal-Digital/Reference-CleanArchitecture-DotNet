using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.Repository;
using Todo.Domain.UseCaseMessages;
using Todo.UseCase.Todo;

namespace Todo.UseCase.Tests.Todo
{
    [TestFixture]
    public class UpdateTodoItemUseCaseTests
    {

        [Test]
        public void Ctor_WhenNullTodoRepository_ShouldThrowArgumentNullException()
        {
            //---------------Arrange-------------------
            var expected = "repository";
            //---------------Act-------------------
            var result = Assert.Throws<ArgumentNullException>(() => { new UpdateTodoItemUseCase(null); });
            //---------------Assert-------------------
            Assert.AreEqual(expected, result.ParamName);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Execute_WhenEmptyOrWhitespaceItemDescription_ShouldReturnErrorMessage(string itemDescription)
        {
            //---------------Arrange-------------------
            var expected = "ItemDescription cannot be null or empty";
            var itemModel = CreateValidUpdateMessage(itemDescription);
            var usecase = CreateUpdateTodoItemUseCase();
            var presenter = new PropertyPresenter<UpdateTodoItemOutput, ErrorOutputMessage>();
            //---------------Act-------------------
            usecase.Execute(itemModel, presenter);
            //---------------Assert-------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual(expected, presenter.ErrorContent.Errors.First());
        }

        [Test]
        public void Execute_WhenInputMessageContainsValidData_ShouldReturnItemId()
        {
            //---------------Arrange-------------------
            var expected = "Item updated";
            var itemModel = CreateValidUpdateMessage("Updated task");
            var usecase = CreateUpdateTodoItemUseCase();
            var presenter = new PropertyPresenter<UpdateTodoItemOutput, ErrorOutputMessage>();
            //---------------Act-------------------
            usecase.Execute(itemModel, presenter);
            //---------------Assert-------------------
            Assert.AreEqual(expected, presenter.SuccessContent.Message);
        }

        private UpdateTodoItemInput CreateValidUpdateMessage(string itemDescription)
        {
            return new UpdateTodoItemInput
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
