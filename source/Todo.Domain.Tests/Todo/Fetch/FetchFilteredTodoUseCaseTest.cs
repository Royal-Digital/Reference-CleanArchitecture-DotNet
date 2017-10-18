using System;
using System.Collections.Generic;
using NUnit.Framework;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Presenter;
using TddBuddy.DateTime.Extensions;
using Todo.Boundary.Todo.Fetch;
using System.Linq;
using Todo.Boundary.Todo.Fetch.Filtered;

namespace Todo.Domain.Tests.Todo.Fetch
{
    [TestFixture]
    public class FetchFilteredTodoUseCaseTest
    {
        [Test]
        public void Execute_WhenFilteringIncludedCompletedFalse_ShouldReturnCollectionOfUncompletedItems()
        {
            //---------------Arrange-------------------
            var input = new TodoFilterInput { IncludedCompleted = false };
            var itemModels = CreateTodoItems(10);
            var expected = itemModels.Where(x => x.IsCompleted == false).ToList();
            var testContext = new FetchFilteredTodoUseCaseTestDataBuilder().WithItems(itemModels).Build();
            var usecase = testContext.UseCase;
            var presenter = new PropertyPresenter<List<TodoTo>, ErrorOutputMessage>();
            //---------------Act-------------------
            usecase.Execute(input, presenter);
            //---------------Assert-------------------
            AssertTodoItemsMatchExpected(expected, presenter.SuccessContent);
        }

        [Test]
        public void Execute_WhenNullTodoFilterInput_ShouldReturnEmptyCollection()
        {
            //---------------Arrange-------------------
            var expected = "Null filter object";
            var testContext = new FetchFilteredTodoUseCaseTestDataBuilder().Build();
            var usecase = testContext.UseCase;
            var presenter = new PropertyPresenter<List<TodoTo>, ErrorOutputMessage>();
            //---------------Act-------------------
            usecase.Execute(null, presenter);
            //---------------Assert-------------------
            Assert.IsTrue(presenter.ErrorContent.HasErrors);
            Assert.AreEqual(expected, presenter.ErrorContent.Errors[0]);
        }

        private void AssertTodoItemsMatchExpected(IReadOnlyList<TodoTo> expected, IReadOnlyList<TodoTo> result)
        {
            Assert.AreEqual(expected.Count, result.Count);
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

        private List<TodoTo> CreateTodoItems(int numberOfItems)
        {
            var itemModels = new List<TodoTo>();

            for (var i = 0; i < numberOfItems; i++)
            {
                var taskNumber = i + 1;

                itemModels.Add(new TodoTo
                {
                    Id = Guid.NewGuid(),
                    ItemDescription = "task " + taskNumber,
                    DueDate = DateTime.Today.ConvertTo24HourFormatWithSeconds(),
                    IsCompleted = false
                });
            }

            return itemModels;
        }
    }
}
