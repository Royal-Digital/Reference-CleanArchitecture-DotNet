using System;
using NUnit.Framework;
using Todo.Data.EfModels;

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
            var todoEntity = new TodoItemEfModel();
            //---------------Assert-------------------
            Assert.AreNotEqual(Guid.Empty, todoEntity.Id);
        }
    }
}
