using System;
using System.Linq;
using NUnit.Framework;
using TddBuddy.SpeedySqlLocalDb;
using TddBuddy.SpeedySqlLocalDb.Attribute;
using TddBuddy.SpeedySqlLocalDb.Construction;
using Todo.Data.Context;
using Todo.Data.Repositories;
using Todo.Domain.Messages;

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
                var entity = assertContext.TodoItem.FirstOrDefault();
                Assert.AreEqual(inputMessage.ItemDescription, entity.ItemDescription);
                Assert.IsFalse(entity.IsCompleted);
            }
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
