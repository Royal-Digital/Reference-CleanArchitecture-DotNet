using System;
using NUnit.Framework;
using Todo.Data.Comment;

namespace Todo.Data.Tests.Comment
{
    [TestFixture]
    public class CommentEntityFrameworkModelTests
    {
        [Test]
        public void Ctor_WhenConstruting_ShouldSetId()
        {
            //---------------Arrange-------------------
            var expected = Guid.Empty;
            //---------------Act-------------------
            var entity = new CommentEntityFrameworkModel();
            //---------------Assert-------------------
            Assert.AreNotEqual(expected, entity.Id);
        }
    }
}
