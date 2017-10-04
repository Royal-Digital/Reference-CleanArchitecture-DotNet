using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NUnit.Framework;
using TddBuddy.DateTime.Extensions;
using TddBuddy.SpeedySqlLocalDb;
using TddBuddy.SpeedySqlLocalDb.Attribute;
using TddBuddy.SpeedySqlLocalDb.Construction;
using Todo.Boundry.Todo.Create;
using Todo.Boundry.Todo.Fetch;
using Todo.Boundry.Todo.Update;
using Todo.Data.Context;
using Todo.Data.EfModels;
using Todo.Data.Repositories;

namespace Todo.Data.Tests.Repositories
{
    [Category("Integration")]
    [TestFixture]
    [SharedSpeedyLocalDb(typeof(TodoContext))]
    public class TodoItemRepositoryTests
    {
        [Test]
        public void Create_WhenValidInputModel_ShouldInsertEntity()
        {
            //---------------Arrange-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var assertContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                var todoItem = CreateTodoItem("a thing todo!");
                //---------------Act-------------------
                todoItems.Create(todoItem);
                todoItems.Save();
                //---------------Assert-------------------
                AssertItemWasCreatedSuccessfully(assertContext, todoItem.ItemDescription);
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
                var itemEntities = CreateTodoItemEntityFrameworkEntities(entityCount);
                InsertTodoItems(itemEntities, wrapper);
                InsertComments(itemEntities, wrapper);
                var expected = CreateExpectedFromEntities(wrapper);

                var todoItemRepository = CreateTodoItemRepository(wrapper);
                //---------------Act-------------------
                var result = todoItemRepository.FetchAll();
                //---------------Assert-------------------
                AssertTodoItemsMatchExpected(expected, result);
            }
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

        [Test]
        public void Update_WhenValidInputModel_ShouldUpdateEntity()
        {
            //---------------Arrange-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var assertContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                var id = InsertNewTodoEntity(repositoryDbContext);
                var model = CreateTodoItem(id);
                
                //---------------Act-------------------
                todoItems.Update(model);
                todoItems.Save();
                //---------------Assert-------------------
                var entity = assertContext.TodoItem.First(x => x.Id == id);

                AssertModelMatchesEntity(model, entity);
            }
        }

        [Test]
        public void Delete_WhenIdExist_ShouldDeleteEntity()
        {
            //---------------Arrange-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                var id = InsertNewTodoEntity(repositoryDbContext);
                //---------------Act-------------------
                var result = todoItems.Delete(id);
                //---------------Assert-------------------
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void Delete_WhenIdDoesNotExist_ShouldNotDeleteEntity()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();

            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                //---------------Act-------------------
                var result = todoItems.Delete(id);
                //---------------Assert-------------------
                Assert.IsFalse(result);
            }
        }

        [Test]
        public void FindById_WhenIdExist_ShouldReturnDomainEntity()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();

            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                AddEfEntity(repositoryDbContext, id);
                //---------------Act-------------------
                var result = todoItems.FindById(id);
                //---------------Assert-------------------
                Assert.AreEqual(id, result.Id);
            }
        }

        [Test]
        public void FindById_WhenIdNotExist_ShouldReturnMissingDomainEntity()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();

            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var todoItems = CreateTodoItemRepository(repositoryDbContext);
                //---------------Act-------------------
                var result = todoItems.FindById(id);
                //---------------Assert-------------------
                Assert.IsNull(result);
            }
        }

        private TodoItemRepository CreateTodoItemRepository(ISpeedySqlLocalDbWrapper wrapper)
        {
            var repositoryDbContext = CreateDbContext(wrapper);
            var todoItemRepository = CreateTodoItemRepository(repositoryDbContext);
            return todoItemRepository;
        }

        private void AddEfEntity(TodoContext repositoryDbContext, Guid id)
        {
            repositoryDbContext.TodoItem.Add(CreateEfEntity(id));
            repositoryDbContext.SaveChanges();
        }

        private TodoItemEfModel CreateEfEntity(Guid id)
        {
            return new TodoItemEfModel
            {
                Id = id,
                DueDate = DateTime.Now,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                ItemDescription = "do stuff"
            };
        }

        private UpdateTodoItemInput CreateTodoItem(Guid id)
        {
            var model = new UpdateTodoItemInput
            {
                Id = id,
                ItemDescription = "updated",
                IsCompleted = true,
                DueDate = DateTime.Today
            };
            return model;
        }

        private void AssertModelMatchesEntity(UpdateTodoItemInput model, TodoItemEfModel entity)
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

        private List<TodoItemTo> CreateExpectedFromEntities(ISpeedySqlLocalDbWrapper wrapper)
        {
            var result = new List<TodoItemTo>();
            var fetchContext = CreateDbContext(wrapper);
            var todoItems = fetchContext.TodoItem.Include(x => x.Comments).ToList();

            todoItems.ForEach(item =>
            {
                var comments = ConvertToCommentToCollection(item);
                result.Add(CreateTodoItemTo(item, comments));
            });

            return result;
        }

        private TodoItemTo CreateTodoItemTo(TodoItemEfModel item, List<TodoCommentTo> comments)
        {
            return new TodoItemTo
            {
                Id = item.Id,
                ItemDescription = item.ItemDescription,
                DueDate = item.DueDate.ConvertTo24HourFormatWithSeconds(),
                IsCompleted = false,
                Comments = comments
            };
        }

        private List<TodoCommentTo> ConvertToCommentToCollection(TodoItemEfModel item)
        {
            var comments = new List<TodoCommentTo>();

            item.Comments.ToList().ForEach(c =>
            {
                comments.Add(new TodoCommentTo
                {
                    Id = c.Id,
                    Comment = c.Comment,
                    Created = c.Created.ConvertTo24HourFormatWithSeconds()
                });
            });
            return comments;
        }

        private void InsertComments(List<TodoItemEfModel> itemEntities, ISpeedySqlLocalDbWrapper wrapper)
        {
            var commentCount = new Random().Next(1,5);
            
            var insertContext = CreateDbContext(wrapper);
            itemEntities.ForEach(item =>
            {
                var itemId = item.Id;
                AddCommentsForTodoItem(commentCount, insertContext, itemId);
            });
            insertContext.SaveChanges();
        }

        private void AddCommentsForTodoItem(int commentCount, TodoContext insertContext, Guid itemId)
        {
            for (var i = 0; i < commentCount; i++)
            {
                var commentId = Guid.NewGuid();
                insertContext.Comments.Add(new CommentEfModel
                {
                    Id = Guid.NewGuid(),
                    Comment = "comment_" + i + " " + commentId,
                    TodoItemId = itemId
                });
            }
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

        private List<TodoItemEfModel> CreateTodoItemEntityFrameworkEntities(int count)
        {
            var result = new List<TodoItemEfModel>();

            for (var i = 0; i < count; i++)
            {
                var itemId = Guid.NewGuid();
                var item = new TodoItemEfModel
                {
                    Id = itemId,
                    ItemDescription = $"task #{i+1}",
                    DueDate = DateTime.Today
                };
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

        private CreateTodoItemInput CreateTodoItem(string itemDescription)
        {
            var inputMessage = new CreateTodoItemInput
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
