using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NUnit.Framework;
using TddBuddy.DateTime.Extensions;
using TddBuddy.SpeedySqlLocalDb;
using TddBuddy.SpeedySqlLocalDb.Attribute;
using TddBuddy.SpeedySqlLocalDb.Construction;
using Todo.Boundary.Comment;
using Todo.Boundary.Comment.Create;
using Todo.Boundary.Todo.Fetch;
using Todo.Data.Comment;
using Todo.Data.Todo;

namespace Todo.Data.Tests.Comment
{
    [Category("Integration")]
    [TestFixture]
    [SharedSpeedyLocalDb(typeof(TodoContext))]
    public class CommentRepositoryTests
    {
        [Test]
        public void Create_WhenValidInputModel_ShouldInsertEntity()
        {
            //---------------Arrange-------------------
            var todoItemId = Guid.NewGuid();

            using (var wrapper = CreateTransactionalWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var assertContext = CreateDbContext(wrapper);
                var comments = CreateCommentRepository(repositoryDbContext);
                var comment = new CreateCommentInput {Comment = "a comment", TodoItemId = todoItemId};

                AddTodoItem(repositoryDbContext, todoItemId);
                //---------------Act-------------------
                comments.Create(comment);
                comments.Persist();
                //---------------Assert-------------------
                var entity = assertContext.Comments.FirstOrDefault();
                Assert.AreNotEqual(Guid.Empty, entity.Id);
            }
        }

        [Test]
        public void Delete_WhenIdPresent_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var todoItemId = Guid.NewGuid();
            var id = Guid.NewGuid();

            using (var wrapper = CreateTransactionalWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var comments = CreateCommentRepository(repositoryDbContext);

                AddTodoItem(repositoryDbContext, todoItemId);
                AddComment(repositoryDbContext, id, todoItemId);
                //---------------Act-------------------
                var result = comments.MarkForDelete(id);
                //---------------Assert-------------------
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void Delete_WhenIdNotPresent_ShouldReturnFalse()
        {
            //---------------Arrange-------------------
            using (var wrapper = CreateTransactionalWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var comments = CreateCommentRepository(repositoryDbContext);
                var id = Guid.NewGuid();
                //---------------Act-------------------
                var result = comments.MarkForDelete(id);
                //---------------Assert-------------------
                Assert.IsFalse(result);
            }
        }

        [Test]
        public void FindForItem_WhenIdHasComments_ShouldReturnAllCommentsInDateAscOrder()
        {
            //---------------Arrange-------------------
            var todoItemId = Guid.NewGuid();

            using (var wrapper = CreateTransactionalWrapper())
            {
                var insertDbContext = CreateDbContext(wrapper);
                
                var repositoryDbContext = CreateDbContext(wrapper);
                var expected = CreateExpectedTodoComments(repositoryDbContext);
                var comments = CreateCommentRepository(repositoryDbContext);

                AddTodoItem(insertDbContext, todoItemId);
                AddManyComments(5, insertDbContext, todoItemId);
                //---------------Act-------------------
                var result = comments.FindForItem(todoItemId);
                //---------------Assert-------------------
                AssertCommentCollectionsMatch(expected, result);
            }
        }

        [Test]
        public void FindForItem_WhenIdHasNoComments_ShouldReturnEmptyList()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();
            using (var wrapper = CreateTransactionalWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var comments = CreateCommentRepository(repositoryDbContext);
                //---------------Act-------------------
                var result = comments.FindForItem(id);
                //---------------Assert-------------------
                CollectionAssert.IsEmpty(result);
            }
        }

        private static ISpeedySqlLocalDbWrapper CreateTransactionalWrapper()
        {
            return new SpeedySqlBuilder().BuildWrapper();
        }

        private void AddTodoItem(TodoContext repositoryDbContext, Guid todoItemId)
        {
            repositoryDbContext.TodoItem.Add(new TodoItemEntityFrameworkModel
            {
                Id = todoItemId,
                DueDate = DateTime.MaxValue,
                ItemDescription = "do stuff"
            });
            repositoryDbContext.SaveChanges();
        }

        private void AssertCommentCollectionsMatch(IList<TodoCommentTo> expected, List<TodoCommentTo> result)
        {
            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, result[i].Id);
                Assert.AreEqual(expected[i].Comment, result[i].Comment);
            }
        }

        private IList<TodoCommentTo> CreateExpectedTodoComments(TodoContext repositoryDbContext)
        {
            var efModels = repositoryDbContext.Comments.ToList().OrderBy(x => x.Created.TimeOfDay).ToList();
            var expected = ConvertEntityFrameworkEntitiesToTransferObjects(efModels);
            return expected;
        }

        private IList<TodoCommentTo> ConvertEntityFrameworkEntitiesToTransferObjects(List<CommentEntityFrameworkModel> efModels)
        {
            var mapper = CreateAutoMapper();
            var result = new List<TodoCommentTo>();

            efModels.ForEach(model =>
            {
                result.Add(mapper.Map<TodoCommentTo>(model));
            });

            return result;
        }

        private void AddManyComments(int total, TodoContext dbContext, Guid todoItemId)
        {
            for(var i = 0; i < total; i++) 
            {
                AddComment(dbContext, Guid.NewGuid(), todoItemId);
            }
        }

        private void AddComment(TodoContext dbContext, Guid id, Guid todoItemId)
        {
            dbContext.Comments.Add(new CommentEntityFrameworkModel
            {
                Id = id,
                TodoItemId = todoItemId,
                Comment = "comment "+id
            });
            dbContext.SaveChanges();
        }

        private TodoContext CreateDbContext(ISpeedySqlLocalDbWrapper wrapper)
        {
            return new TodoContext(wrapper.Connection);
        }

        private ICommentRepository CreateCommentRepository(TodoContext repositoryDbContext)
        {
            var todoItems = new CommentRepository(repositoryDbContext);
            return todoItems;
        }

        private IMapper CreateAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CommentEntityFrameworkModel, TodoCommentTo>()
                    .ForMember(x => x.Created,
                        opt => opt.ResolveUsing(src => src.Created.ConvertTo24HourFormatWithSeconds()));
            });

            return new Mapper(configuration);
        }
    }
}
