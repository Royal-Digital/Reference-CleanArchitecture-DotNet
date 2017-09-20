using System;
using NUnit.Framework;
using Todo.Data.Entities;

namespace Todo.Data.Tests.Entities
{
    [TestFixture]
    public class TodoItemTests
    {
        [Test]
        public void Ctor_WhenConstruting_ShouldSetId()
        {
            //---------------Arrange-------------------
            //---------------Act-------------------
            var todoEntity = new TodoItem();
            //---------------Assert-------------------
            Assert.AreNotEqual(Guid.Empty, todoEntity.Id);
        }
    }
}
