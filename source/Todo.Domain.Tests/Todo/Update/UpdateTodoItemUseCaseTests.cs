using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Boundary.Todo.Update;
using Todo.Domain.Todo.Update;

namespace Todo.Domain.Tests.Todo.Update
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
            var result = Assert.Throws<ArgumentNullException>(() => { new UpdateTodoUseCase(null); });
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
            var testContext = new UpdateTodoItemUseCaseTestDataBuilder().Build();
            var usecase = testContext.UseCase;
            var presenter = new ResultFreePropertyPresenter<ErrorOutputMessage>();
            //---------------Act-------------------
            usecase.Execute(itemModel, presenter);
            //---------------Assert-------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual(expected, presenter.ErrorContent.Errors.First());
        }

        [Test]
        public void Execute_WhenInputMessageContainsValidData_ShouldUpdateItem()
        {
            //---------------Arrange-------------------
            var itemModel = CreateValidUpdateMessage("Updated task");
            var testContext = new UpdateTodoItemUseCaseTestDataBuilder().Build();
            var usecase = testContext.UseCase;
            var presenter = new ResultFreePropertyPresenter<ErrorOutputMessage>();
            //---------------Act-------------------
            usecase.Execute(itemModel, presenter);
            //---------------Assert-------------------
            Assert.IsFalse(presenter.IsErrorResponse());
            testContext.Repository.Received(1).Update(Arg.Is<UpdateTodoInput>(x=>x.Id == itemModel.Id));
            testContext.Repository.Received(1).Save();
        }

        private UpdateTodoInput CreateValidUpdateMessage(string itemDescription)
        {
            return new UpdateTodoInput
            {
                Id = Guid.NewGuid(),
                DueDate = DateTime.Today,
                ItemDescription = itemDescription,
                IsCompleted = true
            };
        }
    }
}
