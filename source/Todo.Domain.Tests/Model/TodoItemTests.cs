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

        [Test]
        public void IsIdValid_WhenNotEmptyGuid_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var todoItem =  new TodoItemModel { Id = Guid.NewGuid() };
            //---------------Execute Test ----------------------
            var result = todoItem.IsIdValid();
            //---------------Test Result -----------------------
            Assert.IsTrue(result);
        }

        [Test]
        public void IsIdValid_WhenEmptyGuid_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            var todoItem = new TodoItemModel { Id = Guid.Empty };
            //---------------Execute Test ----------------------
            var result = todoItem.IsIdValid();
            //---------------Test Result -----------------------
            Assert.IsFalse(result);
        }

        [Test]
        public void IsItemDescriptionValid_WhenNotNullOrEmpty_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var todoItem = new TodoItemModel { ItemDescription = "do stuff" };
            //---------------Execute Test ----------------------
            var result = todoItem.IsItemDescriptionValid();
            //---------------Test Result -----------------------
            Assert.IsTrue(result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void IsIdValid_WhenNullOrEmpty_ShouldReturnFalse(string description)
        {
            //---------------Set up test pack-------------------
            var todoItem = new TodoItemModel { ItemDescription = description };
            //---------------Execute Test ----------------------
            var result = todoItem.IsIdValid();
            //---------------Test Result -----------------------
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
