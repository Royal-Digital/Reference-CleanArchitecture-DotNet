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
            var todoItem = CreateTodoItemModel(true);
            //---------------Execute Test ----------------------
            var result = todoItem.IsOverdue();
            //---------------Test Result -----------------------
            Assert.IsFalse(result);
        }

        [Test]
        public void IsOverdue_WhenIncompleteWithCompletionDue1DayOverDue_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var oneDayInThePast = DateTime.Now.Subtract(new TimeSpan(1,0,0,0));
            var todoItem = CreateIncompletedTodoItemModelWithCompletionDue(oneDayInThePast);
            //---------------Execute Test ----------------------
            var result = todoItem.IsOverdue();
            //---------------Test Result -----------------------
            Assert.IsTrue(result);
        }

        [Test]
        public void IsOverdue_WhenIncompleteWithCompletionDue1DayRemaining_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            var oneDayRemaining = DateTime.Now.Add(new TimeSpan(1, 0, 0, 0));
            var todoItem = CreateIncompletedTodoItemModelWithCompletionDue(oneDayRemaining);
            //---------------Execute Test ----------------------
            var result = todoItem.IsOverdue();
            //---------------Test Result -----------------------
            Assert.IsFalse(result);
        }

        [Test]
        public void IsOverdue_WhenIncompleteWithCompletionDueCurrentDate_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var today = DateTime.Now;
            var todoItem = CreateIncompletedTodoItemModelWithCompletionDue(today);
            //---------------Execute Test ----------------------
            var result = todoItem.IsOverdue();
            //---------------Test Result -----------------------
            Assert.IsTrue(result);
        }


        private static TodoItemModel CreateIncompletedTodoItemModelWithCompletionDue(DateTime oneDayInThePast)
        {
            return CreateIncompletedTodoItemModel(oneDayInThePast, false);
        }

        private static TodoItemModel CreateTodoItemModel(bool isCompleted)
        {
            return CreateIncompletedTodoItemModel(DateTime.Now, isCompleted);
        }

        private static TodoItemModel CreateIncompletedTodoItemModel(DateTime oneDayInThePast, bool isCompleted)
        {
            var todoItem = new TodoItemModel {CompletionDate = oneDayInThePast, IsCompleted = isCompleted};
            return todoItem;
        }
    }
}
