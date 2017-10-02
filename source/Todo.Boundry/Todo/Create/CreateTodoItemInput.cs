using System;

namespace Todo.Boundry.Todo.Create
{
    public class CreateTodoItemInput
    {
        public string ItemDescription { get; set; }
        public DateTime DueDate { get; set; }
    }
}