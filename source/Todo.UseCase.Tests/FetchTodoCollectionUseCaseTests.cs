using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.Repository;
using Todo.Entities;

namespace Todo.UseCase.Tests
{
    [TestFixture]
    public class FetchTodoCollectionUseCaseTests
    {
        [Test]
        public void Execute_WhenInvoked_ShouldReturnCollectionOfAllItems()
        {
            //---------------Arrange-------------------
            var itemModels = CreateTodoItems();
            var expected = itemModels;
            var usecase = CreateFetchTodoCollectionUseCase(itemModels);
            var presenter = new PropertyPresenter<List<TodoItem>, ErrorOutputMessage>();
            //---------------Act-------------------
            usecase.Execute(presenter);
            //---------------Assert-------------------
            CollectionAssert.AreEquivalent(expected, presenter.SuccessContent);
        }

        private List<TodoItem> CreateTodoItems()
        {
            var itemModels = new List<TodoItem>
            {
                new TodoItem {Id = Guid.NewGuid(), ItemDescription = "task 1", DueDate = DateTime.Today},
                new TodoItem {Id = Guid.NewGuid(), ItemDescription = "task 2", DueDate = DateTime.Today}
            };
            return itemModels;
        }

        private FetchTodoCollectionUseCase CreateFetchTodoCollectionUseCase(List<TodoItem> itemModels)
        {
            var repository = CreateTodoRepository(itemModels);
            var usecase = new FetchTodoCollectionUseCase(repository);
            return usecase;
        }

        private ITodoRepository CreateTodoRepository(List<TodoItem> itemModels)
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.FetchAll().Returns(itemModels);

            return repository;
        }
    }
}
