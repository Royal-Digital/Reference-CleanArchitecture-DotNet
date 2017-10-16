using System;
using System.Collections.Generic;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using TddBuddy.DateTime.Extensions;
using Todo.Boundary.Todo.Fetch;

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
            var testContext = new FetchTodoCollectionUseCaseTestDataBuilder().WithItems(itemModels).Build();
            var usecase = testContext.UseCase;
            var presenter = new PropertyPresenter<List<TodoTo>, ErrorOutputMessage>();
            //---------------Act-------------------
            usecase.Execute(presenter);
            //---------------Assert-------------------
            AssertTodoItemsMatchExpected(itemModels, presenter.SuccessContent);
        }

        private void AssertTodoItemsMatchExpected(IReadOnlyList<TodoTo> expected, IReadOnlyList<TodoTo> result)
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

        private List<TodoTo> CreateTodoItems()
        {
            var itemModels = new List<TodoTo>();

            for (var i = 0; i < 2; i++)
            {
                var taskNumber = i + 1;
                itemModels.Add(new TodoTo
                {
                    Id = Guid.NewGuid(),
                    ItemDescription = "task "+taskNumber,
                    DueDate = DateTime.Today.ConvertTo24HourFormatWithSeconds()
                });
            }

            return itemModels;
        }

    }
}
