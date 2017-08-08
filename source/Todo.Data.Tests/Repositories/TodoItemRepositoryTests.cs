using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TddBuddy.SpeedySqlLocalDb;
using TddBuddy.SpeedySqlLocalDb.Attribute;
using TddBuddy.SpeedySqlLocalDb.Construction;
using Todo.Data.Context;
using Todo.Data.Entities;
using Todo.Data.Repositories;
using Todo.Domain.Messages;
using Todo.Domain.Model;

namespace Todo.Data.Tests.Repositories
{
    [Category("Integration")]
    [TestFixture]
    [SharedSpeedyLocalDb(typeof(TodoContext))]
    public class TodoItemRepositoryTests
    {
        [Test]
        public void CreateItem_WhenValidInputModel_ShouldInsertEntity()
        {
            //---------------Set up test pack-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var assertContext = CreateDbContext(wrapper);
                var todoItems = new TodoItemRepository(repositoryDbContext);
                var inputMessage = CreateTodoItemInputMessage("a thing todo!");
                //---------------Execute Test ----------------------
                todoItems.CreateItem(inputMessage);
                todoItems.Save();
                //---------------Test Result -----------------------
                AssertEntityInCorrectState(assertContext, inputMessage.ItemDescription);
            }
        }

        [Test]
        public void FetchAll_WhenNoItems_ShouldReturnEmptyList()
        {
            //---------------Set up test pack-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var expected = new List<TodoItemModel>();
                var repositoryDbContext = CreateDbContext(wrapper);
                var todoItems = new TodoItemRepository(repositoryDbContext);
                //---------------Execute Test ----------------------
                var result = todoItems.FetchAll();
                //---------------Test Result -----------------------
                CollectionAssert.AreEquivalent(expected, result);
            }
        }

        [Test]
        public void FetchAll_WhenManyItems_ShouldReturnAllItems()
        {
            //---------------Set up test pack-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var itemEntities = CreateTodoItemEntities(5);
                var expected = ConvertEntitiesToModel(itemEntities);
                InsertTodoItems(itemEntities, wrapper);

                var repositoryDbContext = CreateDbContext(wrapper);
                var todoItems = new TodoItemRepository(repositoryDbContext);
                //---------------Execute Test ----------------------
                var result = todoItems.FetchAll();
                //---------------Test Result -----------------------
                CollectionAssert.AreEquivalent(expected, result);
            }
        }

        private List<TodoItemModel> ConvertEntitiesToModel(List<TodoItem> items)
        {
            var result = new List<TodoItemModel>();
            
            items.ForEach(item =>
            {
                result.Add(new TodoItemModel
                {
                    Id = item.Id,
                    ItemDescription = item.ItemDescription,
                    CompletionDate = item.CompletionDate,
                    IsCompleted = false
                });
            });

            return result;
        }

        private void InsertTodoItems(List<TodoItem> items, ISpeedySqlLocalDbWrapper wrapper)
        {
            var insertContext = CreateDbContext(wrapper);
            items.ForEach(item =>
            {
                insertContext.TodoItem.Add(item);
            });
            insertContext.SaveChanges();
        }

        private List<TodoItem> CreateTodoItemEntities(int count)
        {
            var result = new List<TodoItem>();

            for (var i = 0; i < count; i++)
            {
                var item = new TodoItem { ItemDescription = $"task #{i+1}", CompletionDate = DateTime.Today };
                result.Add(item);
            }
            
            return result;
        }

        private void AssertEntityInCorrectState(TodoContext assertContext, string expectedDescription)
        {
            var entity = assertContext.TodoItem.FirstOrDefault();
            Assert.AreEqual(expectedDescription, entity?.ItemDescription);
            Assert.IsFalse(entity.IsCompleted);
        }

        private CreateTodoItemInputMessage CreateTodoItemInputMessage(string itemDescription)
        {
            var inputMessage = new CreateTodoItemInputMessage
            {
                ItemDescription = itemDescription,
                CompletionDate = DateTime.Today
            };
            return inputMessage;
        }

        private TodoContext CreateDbContext(ISpeedySqlLocalDbWrapper wrapper)
        {
            return new TodoContext(wrapper.Connection);
        }
    }
}
