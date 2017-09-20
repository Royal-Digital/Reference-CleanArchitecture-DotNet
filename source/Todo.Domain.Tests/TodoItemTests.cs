using System;
using NUnit.Framework;

namespace Todo.Entities.Tests
{
    [TestFixture]
    public class TodoItemTests
    {
        [Test]
        public void IsOverdue_WhenCompleted_ShouldReturnFalse()
        {
            //---------------Arrange-------------------
            var todoItem = CreateTodoItem(true);
            //---------------Act-------------------
            var result = todoItem.IsOverdue();
            //---------------Assert-------------------
            Assert.IsFalse(result);
        }

        [Test]
        public void IsOverdue_WhenIncompleteWithCompletionDue1DayOverDue_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var oneDayInThePast = DateTime.Now.Subtract(new TimeSpan(1,0,0,0));
            var todoItem = CreateIncompletedTodoItemWithCompletionDue(oneDayInThePast);
            //---------------Act-------------------
            var result = todoItem.IsOverdue();
            //---------------Assert-------------------
            Assert.IsTrue(result);
        }

        [Test]
        public void IsOverdue_WhenIncompleteWithCompletionDue1DayRemaining_ShouldReturnFalse()
        {
            //---------------Arrange-------------------
            var oneDayRemaining = DateTime.Now.Add(new TimeSpan(1, 0, 0, 0));
            var todoItem = CreateIncompletedTodoItemWithCompletionDue(oneDayRemaining);
            //---------------Act-------------------
            var result = todoItem.IsOverdue();
            //---------------Assert-------------------
            Assert.IsFalse(result);
        }

        [Test]
        public void IsOverdue_WhenIncompleteWithCompletionDueCurrentDate_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var today = DateTime.Now;
            var todoItem = CreateIncompletedTodoItemWithCompletionDue(today);
            //---------------Act-------------------
            var result = todoItem.IsOverdue();
            //---------------Assert-------------------
            Assert.IsTrue(result);
        }

        [Test]
        public void IsIdValid_WhenNotEmptyGuid_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var todoItem =  new TodoItem { Id = Guid.NewGuid() };
            //---------------Act-------------------
            var result = todoItem.IsIdValid();
            //---------------Assert-------------------
            Assert.IsTrue(result);
        }

        [Test]
        public void IsIdValid_WhenEmptyGuid_ShouldReturnFalse()
        {
            //---------------Arrange-------------------
            var todoItem = new TodoItem { Id = Guid.Empty };
            //---------------Act-------------------
            var result = todoItem.IsIdValid();
            //---------------Assert-------------------
            Assert.IsFalse(result);
        }

        [Test]
        public void IsItemDescriptionValid_WhenNotNullOrEmpty_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var todoItem = new TodoItem { ItemDescription = "do stuff" };
            //---------------Act-------------------
            var result = todoItem.ItemDescriptionIsValid();
            //---------------Assert-------------------
            Assert.IsTrue(result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsIdValid_WhenNullOrEmpty_ShouldReturnFalse(string description)
        {
            //---------------Arrange-------------------
            var todoItem = new TodoItem { ItemDescription = description };
            //---------------Act-------------------
            var result = todoItem.IsIdValid();
            //---------------Assert-------------------
            Assert.IsFalse(result);
        }


        private TodoItem CreateIncompletedTodoItemWithCompletionDue(DateTime oneDayInThePast)
        {
            return CreateIncompletedTodoItem(oneDayInThePast, false);
        }

        private TodoItem CreateTodoItem(bool isCompleted)
        {
            return CreateIncompletedTodoItem(DateTime.Now, isCompleted);
        }

        private TodoItem CreateIncompletedTodoItem(DateTime oneDayInThePast, bool isCompleted)
        {
            var todoItem = new TodoItem {DueDate = oneDayInThePast, IsCompleted = isCompleted};
            return todoItem;
        }
    }
}
