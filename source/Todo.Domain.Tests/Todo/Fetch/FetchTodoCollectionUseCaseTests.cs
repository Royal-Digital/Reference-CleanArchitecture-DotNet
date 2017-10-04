using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Fetch;
using Todo.Domain.Todo.Fetch;
using Todo.Extensions;

namespace Todo.Domain.Tests.Todo.Fetch
{
    [TestFixture]
    public class FetchTodoCollectionUseCaseTests
    {
        [Test]
        public void Execute_WhenInvoked_ShouldReturnCollectionOfAllItems()
        {
            //---------------Arrange-------------------
            var itemModels = CreateTodoItems();
            var usecase = CreateUseCase(itemModels);
            var presenter = new PropertyPresenter<List<TodoItemTo>, ErrorOutputMessage>();
            //---------------Act-------------------
            usecase.Execute(presenter);
            //---------------Assert-------------------
            AssertTodoItemsMatchExpected(itemModels, presenter.SuccessContent);
        }

        private void AssertTodoItemsMatchExpected(IReadOnlyList<TodoItemTo> expected, IReadOnlyList<TodoItemTo> result)
        {
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, result[i].Id);
                var expectedComments = expected[i].Comments;
                for (var z = 0; z < expectedComments.Count; z++)
                {
                    Assert.AreEqual(expected[i].Comments[z].Id, result[i].Comments[z].Id);
                    Assert.AreEqual(expected[i].Comments[z].Comment, result[i].Comments[z].Comment);
                }
            }
        }

        private List<TodoItemTo> CreateTodoItems()
        {
            var itemModels = new List<TodoItemTo>();

            for (var i = 0; i < 2; i++)
            {
                var taskNumber = i + 1;
                itemModels.Add(new TodoItemTo
                {
                    Id = Guid.NewGuid(),
                    ItemDescription = "task "+taskNumber,
                    DueDate = DateTime.Today.ConvertTo24HourFormatWithSeconds()
                });
            }

            return itemModels;
        }

        private FetchTodoCollectionUseCase CreateUseCase(List<TodoItemTo> itemModels)
        {
            var todoRepository = CreateTodoRepository(itemModels);

            return CreateUseCase(todoRepository);
        }

        private FetchTodoCollectionUseCase CreateUseCase(ITodoRepository todoRepository)
        {
            var usecase = new FetchTodoCollectionUseCase(todoRepository);

            return usecase;
        }

        private ITodoRepository CreateTodoRepository(List<TodoItemTo> itemModels)
        {
            var repository = Substitute.For<ITodoRepository>();
            repository.FetchAll().Returns(itemModels);

            return repository;
        }
    }
}
