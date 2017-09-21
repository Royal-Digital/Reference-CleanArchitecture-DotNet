using System;
using System.Collections.Generic;
using System.Linq;
using NExpect;
using NUnit.Framework;
using TddBuddy.SpeedySqlLocalDb;
using TddBuddy.SpeedySqlLocalDb.Attribute;
using TddBuddy.SpeedySqlLocalDb.Construction;
using Todo.Data.Context;
using Todo.Data.EfModels;
using Todo.Data.Repositories;
using Todo.Entities;
using static NExpect.Expectations;

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
                AssertItemWasCreatedSuccessfully(assertContext, inputMessage.ItemDescription);
            }
        }

        [Test]
        public void FetchAll_WhenNoItems_ShouldReturnEmptyList()
        {
            //---------------Arrange-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                //---------------Act-------------------
                var result = todoItems.FetchAll();
                //---------------Assert-------------------
                CollectionAssert.IsEmpty(result);
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
                Expect(result).To.Be.Deep.Equivalent.To(expected);
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
                var model = new TodoItem
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
                var entity = assertContext.TodoItem.First(x => x.Id == id);

                AssertModelMatchesEntity(model, entity);
            }
        }

        [Test]
        public void DeleteItem_WhenIdExist_ShouldDeleteEntity()
        {
            //---------------Arrange-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                var id = InsertNewTodoEntity(repositoryDbContext);
                //---------------Act-------------------
                var result = todoItems.DeleteItem(id);
                //---------------Assert-------------------
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void DeleteItem_WhenIdDoesNotExist_ShouldNotDeleteEntity()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();

            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                //---------------Act-------------------
                var result = todoItems.DeleteItem(id);
                //---------------Assert-------------------
                Assert.IsFalse(result);
            }
        }


        private static void AssertModelMatchesEntity(TodoItem model, TodoItemEfModel entity)
        {
            Assert.AreEqual(model.Id, entity.Id);
            Assert.AreEqual(model.ItemDescription, entity.ItemDescription);
            Assert.AreEqual(model.IsCompleted, entity.IsCompleted);
        }

        private TodoItemRepository CreateTodoItemRepository(TodoContext repositoryDbContext)
        {
            var todoItems = new TodoItemRepository(repositoryDbContext);
            return todoItems;
        }

        private Guid InsertNewTodoEntity(TodoContext repositoryDbContext)
        {
            var todoDbEntity = new TodoItemEfModel
            {
                ItemDescription = "new item",
                DueDate = DateTime.Today,
                IsCompleted = false
            };
            repositoryDbContext.TodoItem.Add(todoDbEntity);
            repositoryDbContext.SaveChanges();

            return todoDbEntity.Id;
        }

        private List<TodoItem> ConvertEntitiesToModel(List<TodoItemEfModel> items)
        {
            var result = new List<TodoItem>();
            
            items.ForEach(item =>
            {
                result.Add(new TodoItem
                {
                    Id = item.Id,
                    ItemDescription = item.ItemDescription,
                    DueDate = item.DueDate,
                    IsCompleted = false
                });
            });

            return result;
        }

        private void InsertTodoItems(List<TodoItemEfModel> items, ISpeedySqlLocalDbWrapper wrapper)
        {
            var insertContext = CreateDbContext(wrapper);
            items.ForEach(item =>
            {
                insertContext.TodoItem.Add(item);
            });
            insertContext.SaveChanges();
        }

        private List<TodoItemEfModel> CreateTodoItemEntities(int count)
        {
            var result = new List<TodoItemEfModel>();

            for (var i = 0; i < count; i++)
            {
                var item = new TodoItemEfModel { ItemDescription = $"task #{i+1}", DueDate = DateTime.Today };
                result.Add(item);
            }
            
            return result;
        }

        private void AssertItemWasCreatedSuccessfully(TodoContext assertContext, string expectedDescription)
        {
            var entity = assertContext.TodoItem.FirstOrDefault();
            Assert.AreEqual(expectedDescription, entity?.ItemDescription);
            Assert.IsFalse(entity.IsCompleted);
            Assert.AreNotEqual(Guid.Empty, entity.Id);
        }

        private TodoItem CreateTodoItemInputMessage(string itemDescription)
        {
            var inputMessage = new TodoItem
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
