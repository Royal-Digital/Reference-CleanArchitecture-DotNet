using NUnit.Framework;
using Todo.Boundary.Todo.Fetch;

namespace Todo.Boundary.Tests
{
    [TestFixture]
    public class TodoItemToTests
    {
        [Test]
        public void Ctor_ShouldSetCommentsToEmptyList()
        {
            //---------------Arrange-------------------
            //---------------Act-------------------
            var result = new TodoTo();
            //---------------Assert-------------------
            Assert.IsEmpty(result.Comments);
        }
    }
}
