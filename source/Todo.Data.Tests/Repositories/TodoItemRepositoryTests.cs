using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TddBuddy.SpeedySqlLocalDb;
using TddBuddy.SpeedySqlLocalDb.Attribute;
using TddBuddy.SpeedySqlLocalDb.Construction;
using Todo.Data.AutoMapper;
using Todo.Data.Context;
using Todo.Data.Entities;
using Todo.Data.Repositories;
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
            //---------------Arrange-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var assertContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                var inputMessage = CreateTodoItemInputMessage("a thing todo!");
                //---------------Act-------------------
                todoItems.CreateItem(inputMessage);
                todoItems.Save();
                //---------------Assert-------------------
                AssertEntityInCorrectState(assertContext, inputMessage.ItemDescription);
            }
        }

        [Test]
        public void FetchAll_WhenNoItems_ShouldReturnEmptyList()
        {
            //---------------Arrange-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var expected = new List<TodoItemModel>();
                var repositoryDbContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                //---------------Act-------------------
                var result = todoItems.FetchAll();
                //---------------Assert-------------------
                CollectionAssert.AreEquivalent(expected, result);
            }
        }

        [Test]
        public void FetchAll_WhenManyItems_ShouldReturnAllItems()
        {
            //---------------Arrange-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var entityCount = 5;
                var itemEntities = CreateTodoItemEntities(entityCount);
                var expected = ConvertEntitiesToModel(itemEntities);
                InsertTodoItems(itemEntities, wrapper);

                var repositoryDbContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                //---------------Act-------------------
                var result = todoItems.FetchAll();
                //---------------Assert-------------------
                CollectionAssert.AreEquivalent(expected, result);
            }
        }

        [Test]
        public void UpdateItem_WhenValidInputModel_ShouldUpdateEntity()
        {
            //---------------Arrange-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var assertContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                var id = InsertNewTodoEntity(repositoryDbContext);
                var model = new TodoItemModel
                {
                    Id = id,
                    ItemDescription = "updated",
                    IsCompleted = true,
                    DueDate = DateTime.Today
                };
                
                //---------------Act-------------------
                todoItems.Update(model);
                todoItems.Save();
                //---------------Assert-------------------
                var entity = assertContext.TodoItem.ToList().First(x => x.Id == id);
                Assert.AreEqual(model.ItemDescription, entity.ItemDescription);
                Assert.AreEqual(model.IsCompleted, entity.IsCompleted);
            }
        }

        private TodoItemRepository CreateTodoItemRepository(TodoContext repositoryDbContext)
        {
            var mapper = new AutoMapperBuilder().Build();
            var todoItems = new TodoItemRepository(repositoryDbContext, mapper);
            return todoItems;
        }

        private Guid InsertNewTodoEntity(TodoContext repositoryDbContext)
        {
            var todoDbEntity = new TodoItem
            {
                ItemDescription = "new item",
                DueDate = DateTime.Today,
                IsCompleted = false
            };
            repositoryDbContext.TodoItem.Add(todoDbEntity);
            repositoryDbContext.SaveChanges();

            return todoDbEntity.Id;
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
                    DueDate = item.DueDate,
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
                var item = new TodoItem { ItemDescription = $"task #{i+1}", DueDate = DateTime.Today };
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

        private TodoItemModel CreateTodoItemInputMessage(string itemDescription)
        {
            var inputMessage = new TodoItemModel
            {
                ItemDescription = itemDescription,
                DueDate = DateTime.Today
            };
            return inputMessage;
        }

        private TodoContext CreateDbContext(ISpeedySqlLocalDbWrapper wrapper)
        {
            return new TodoContext(wrapper.Connection);
        }
    }
}
