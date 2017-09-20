using System;
using NUnit.Framework;
using Todo.Data.EfModels;

namespace Todo.Data.Tests.Entities
{
    [TestFixture]
    public class TodoItemEfModelTests
    {
        [Test]
        public void Ctor_WhenConstruting_ShouldSetId()
        {
            //---------------Arrange-------------------
            var expected = Guid.Empty;
            //---------------Act-------------------
            var todoEntity = new TodoItemEfModel();
            //---------------Assert-------------------
            Assert.AreNotEqual(expected, todoEntity.Id);
        }
    }
}
