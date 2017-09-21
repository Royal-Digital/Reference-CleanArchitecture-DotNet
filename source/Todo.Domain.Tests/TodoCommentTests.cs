using System;
using NUnit.Framework;

namespace Todo.Entities.Tests
{
    [TestFixture]
    public class TodoCommentTests
    {
        [Test]
        public void IsCommentValid_WhenNotNullOrWhitespace_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var comment = new TodoComment {Comment = "a comment"};
            //---------------Act----------------------
            var result = comment.IsCommentValid();
            //---------------Assert-----------------------
            Assert.IsTrue(result);
        }

        [TestCase(" ")]
        [TestCase("")]
        [TestCase(null)]
        public void IsCommentValid_WhenNullOrWhitespace_ShouldReturnFalse(string text)
        {
            //---------------Arrange-------------------
            var comment = new TodoComment { Comment = text };
            //---------------Act----------------------
            var result = comment.IsCommentValid();
            //---------------Assert-----------------------
            Assert.IsFalse(result);
        }

        [Test]
        public void IsTodoItemIdValid_WhenNotEmptyGuid_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var comment = new TodoComment { TodoItemId = Guid.NewGuid() };
            //---------------Act----------------------
            var result = comment.IsTodoItemIdValid();
            //---------------Assert-----------------------
            Assert.IsTrue(result);
        }

        [Test]
        public void IsTodoItemIdValid_WhenEmptyGuid_ShouldReturnFalse()
        {
            //---------------Arrange-------------------
            var comment = new TodoComment { TodoItemId = Guid.Empty };
            //---------------Act----------------------
            var result = comment.IsTodoItemIdValid();
            //---------------Assert-----------------------
            Assert.IsFalse(result);
        }

        [Test]
        public void IsIdValid_WhenEmptyGuid_ShouldReturnFalse()
        {
            //---------------Arrange-------------------
            var comment = new TodoComment { Id = Guid.Empty };
            //---------------Act----------------------
            var result = comment.IsIdValid();
            //---------------Assert-----------------------
            Assert.IsFalse(result);
        }

        [Test]
        public void IsIdValid_WhenNonEmptyGuid_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var comment = new TodoComment { Id = Guid.NewGuid() };
            //---------------Act----------------------
            var result = comment.IsIdValid();
            //---------------Assert-----------------------
            Assert.IsTrue(result);
        }
    }
}
