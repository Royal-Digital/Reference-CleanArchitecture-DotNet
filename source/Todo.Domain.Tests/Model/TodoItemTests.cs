using System;
using NUnit.Framework;
using Todo.Domain.Model;

namespace Todo.Domain.Tests.Model
{
    [TestFixture]
    public class TodoItemTests
    {

        [Test]
        public void IsOverdue_WhenCompleted_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            var todoItem = new TodoItemModel {IsCompleted = true};
            //---------------Execute Test ----------------------
            var result = todoItem.IsOverdue();
            //---------------Test Result -----------------------
            Assert.IsFalse(result);
        }

        [Test]
        public void IsOverdue_WhenIncompleteWithCompletionDate1DayAgo_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var oneDayInThePast = DateTime.Now.Subtract(new TimeSpan(1,0,0,0));
            var todoItem = new TodoItemModel { CompletionDate = oneDayInThePast, IsCompleted = false };
            //---------------Execute Test ----------------------
            var result = todoItem.IsOverdue();
            //---------------Test Result -----------------------
            Assert.IsTrue(result);
        }

        [Test]
        public void IsOverdue_WhenIncompleteWithCompletionDate1DayLeft_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            var oneDayInThePast = DateTime.Now.Add(new TimeSpan(1, 0, 0, 0));
            var todoItem = new TodoItemModel { CompletionDate = oneDayInThePast, IsCompleted = false };
            //---------------Execute Test ----------------------
            var result = todoItem.IsOverdue();
            //---------------Test Result -----------------------
            Assert.IsFalse(result);
        }
    }
}
