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
            //---------------Set up test pack-------------------
            //---------------Execute Test ----------------------
            var todoEntity = new TodoItem();
            //---------------Test Result -----------------------
            Assert.AreNotEqual(Guid.Empty, todoEntity.Id);
        }
    }
}
