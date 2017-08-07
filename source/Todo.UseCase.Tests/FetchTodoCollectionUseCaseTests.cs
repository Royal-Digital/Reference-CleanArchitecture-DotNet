using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.Model;
using Todo.Domain.Repository;
using Todo.TestUtils;

namespace Todo.UseCase.Tests
{
    [TestFixture]
    public class FetchTodoCollectionUseCaseTests
    {
        [Test]
        public void Execute_WhenInvoked_ShouldReturnCollectionOfAllItems()
        {
            //---------------Set up test pack-------------------
            var expected = new List<TodoItemModel>
            {
                new TodoItemModel{ItemDescription = "task 1", CompletionDate = DateTime.Today},
                new TodoItemModel{ItemDescription = "task 2", CompletionDate = DateTime.Today}
            };
            var repository = Substitute.For<ITodoRepository>();
            var todoItemModels = new List<TodoItemModel>
            {
                new TodoItemModel{ItemDescription = "task 1", CompletionDate = DateTime.Today},
                new TodoItemModel{ItemDescription = "task 2", CompletionDate = DateTime.Today}
            };
            repository.FetchAll().Returns(todoItemModels);
            var usecase = new FetchTodoCollectionUseCase(repository);
            var presenter = new PropertyPresenter<List<TodoItemModel>, ErrorOutputMessage>();
            //---------------Execute Test ----------------------
            usecase.Execute(presenter);
            //---------------Test Result -----------------------
            AssertEx.PropertyValuesAreEquals(expected, presenter.SuccessContent);
        }
    }
}
