using NUnit.Framework;
using Todo.Boundry.Todo.Fetch;

namespace Todo.Boundry.Tests
{
    [TestFixture]
    public class TodoItemToTests
    {
        [Test]
        public void Ctor_ShouldSetCommentsToEmptyList()
        {
            //---------------Arrange-------------------
            //---------------Act-------------------
            var result = new TodoItemTo();
            //---------------Assert-------------------
            Assert.IsEmpty(result.Comments);
        }
    }
}
