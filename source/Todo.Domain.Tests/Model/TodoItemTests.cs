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
            //---------------Arrange-------------------
            var todoItem = CreateTodoItemModel(true);
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
            var todoItem = CreateIncompletedTodoItemModelWithCompletionDue(oneDayInThePast);
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
            var todoItem = CreateIncompletedTodoItemModelWithCompletionDue(oneDayRemaining);
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
            var todoItem = CreateIncompletedTodoItemModelWithCompletionDue(today);
            //---------------Act-------------------
            var result = todoItem.IsOverdue();
            //---------------Assert-------------------
            Assert.IsTrue(result);
        }

        [Test]
        public void IsIdValid_WhenNotEmptyGuid_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var todoItem =  new TodoItemModel { Id = Guid.NewGuid() };
            //---------------Act-------------------
            var result = todoItem.IsIdValid();
            //---------------Assert-------------------
            Assert.IsTrue(result);
        }

        [Test]
        public void IsIdValid_WhenEmptyGuid_ShouldReturnFalse()
        {
            //---------------Arrange-------------------
            var todoItem = new TodoItemModel { Id = Guid.Empty };
            //---------------Act-------------------
            var result = todoItem.IsIdValid();
            //---------------Assert-------------------
            Assert.IsFalse(result);
        }

        [Test]
        public void IsItemDescriptionValid_WhenNotNullOrEmpty_ShouldReturnTrue()
        {
            //---------------Arrange-------------------
            var todoItem = new TodoItemModel { ItemDescription = "do stuff" };
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
            var todoItem = new TodoItemModel { ItemDescription = description };
            //---------------Act-------------------
            var result = todoItem.IsIdValid();
            //---------------Assert-------------------
            Assert.IsFalse(result);
        }


        private TodoItemModel CreateIncompletedTodoItemModelWithCompletionDue(DateTime oneDayInThePast)
        {
            return CreateIncompletedTodoItemModel(oneDayInThePast, false);
        }

        private TodoItemModel CreateTodoItemModel(bool isCompleted)
        {
            return CreateIncompletedTodoItemModel(DateTime.Now, isCompleted);
        }

        private TodoItemModel CreateIncompletedTodoItemModel(DateTime oneDayInThePast, bool isCompleted)
        {
            var todoItem = new TodoItemModel {DueDate = oneDayInThePast, IsCompleted = isCompleted};
            return todoItem;
        }
    }
}
