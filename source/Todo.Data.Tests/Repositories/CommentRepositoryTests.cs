﻿using System;
using System.Linq;
using NUnit.Framework;
using TddBuddy.SpeedySqlLocalDb;
using TddBuddy.SpeedySqlLocalDb.Attribute;
using TddBuddy.SpeedySqlLocalDb.Construction;
using Todo.Data.Context;
using Todo.Data.EfModels;
using Todo.Data.Repositories;
using Todo.Domain.Repository;
using Todo.Entities;

namespace Todo.Data.Tests.Repositories
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
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var assertContext = CreateDbContext(wrapper);
                var comments = CreateCommentRepository(repositoryDbContext);
                var comment = new TodoComment {Comment = "a comment", TodoItemId = Guid.NewGuid()};
                //---------------Act-------------------
                comments.Create(comment);
                comments.Save();
                //---------------Assert-------------------
                var entity = assertContext.Comments.FirstOrDefault();
                Assert.AreNotEqual(Guid.Empty, entity.Id);
            }
        }

        [Test]
        public void Delete_WhenIdPresent_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var id = Guid.NewGuid();
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var comments = CreateCommentRepository(repositoryDbContext);
                var comment = new TodoComment { Comment = "a comment", Id = id };
                AddComment(repositoryDbContext, id);
                //---------------Act-------------------
                var result = comments.Delete(comment);
                //---------------Assert-------------------
                Assert.IsTrue(result);
            }
        }

        [Test]
        public void Delete_WhenIdNotPresent_ShouldReturnFalse()
        {
            //---------------Arrange-------------------
            using (var wrapper = new SpeedySqlBuilder().BuildWrapper())
            {
                var repositoryDbContext = CreateDbContext(wrapper);
                var comments = CreateCommentRepository(repositoryDbContext);
                var comment = new TodoComment { Comment = "a comment", TodoItemId = Guid.NewGuid() };
                //---------------Act-------------------
                var result = comments.Delete(comment);
                //---------------Assert-------------------
                Assert.IsFalse(result);
            }
        }

        private void AddComment(TodoContext repositoryDbContext, Guid id)
        {
            repositoryDbContext.Comments.Add(new CommentEfModel {Id = id});
            repositoryDbContext.SaveChanges();
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
    }
}
