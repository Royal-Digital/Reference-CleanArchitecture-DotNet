using System;
using NUnit.Framework;
using Todo.Data.Todo;

namespace Todo.Data.Tests.Todo
{
    [TestFixture]
    public class TodoItemEntityFrameworkModelTests
    {
        [Test]
        public void Ctor_WhenConstruting_ShouldSetId()
        {
            //---------------Arrange-------------------
            var expected = Guid.Empty;
            //---------------Act-------------------
            var todoEntity = new TodoItemEntityFrameworkModel();
            //---------------Assert-------------------
            Assert.AreNotEqual(expected, todoEntity.Id);
        }
    }
}
