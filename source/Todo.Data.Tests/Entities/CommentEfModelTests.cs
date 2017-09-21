using System;
using NUnit.Framework;
using Todo.Data.EfModels;

namespace Todo.Data.Tests.Entities
{
    [TestFixture]
    public class CommentEfModelTests
    {
        [Test]
        public void Ctor_WhenConstruting_ShouldSetId()
        {
            //---------------Arrange-------------------
            var expected = Guid.Empty;
            //---------------Act-------------------
            var entity = new CommentEfModel();
            //---------------Assert-------------------
            Assert.AreNotEqual(expected, entity.Id);
        }
    }
}
