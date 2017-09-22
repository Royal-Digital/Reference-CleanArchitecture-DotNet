using System;
using System.Collections.Generic;
using NExpect;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Domain.Repository;
using Todo.Domain.UseCaseMessages;
using Todo.Entities;
using Todo.UseCase.Todo;
using static NExpect.Expectations;

namespace Todo.UseCase.Tests.Todo
{
    [TestFixture]
    public class FetchTodoCollectionUseCaseTests
    {
        [Test]
        public void Execute_WhenInvoked_ShouldReturnCollectionOfAllItems()
        {
            //---------------Arrange-------------------
            var itemModels = CreateTodoItems();
            var expected = CreateTodoOutputItems(itemModels[0].Id, itemModels[1].Id);
            var usecase = CreateFetchTodoCollectionUseCase(itemModels);
            var presenter = new PropertyPresenter<List<FetchTodoItemOutput>, ErrorOutputMessage>();
            //---------------Act-------------------
            usecase.Execute(presenter);
            //---------------Assert-------------------
            Expect(presenter.SuccessContent).To.Be.Deep.Equivalent.To(expected);
        }

        private List<FetchTodoItemOutput> CreateTodoOutputItems(Guid id1, Guid id2)
        {
            var itemModels = new List<FetchTodoItemOutput>
            {
                new FetchTodoItemOutput {Id = id1, ItemDescription = "task 1", DueDate = DateTime.Today.ToString("yyyy-MM-dd")},
                new FetchTodoItemOutput {Id = id2, ItemDescription = "task 2", DueDate = DateTime.Today.ToString("yyyy-MM-dd")}
            };

            return itemModels;
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
