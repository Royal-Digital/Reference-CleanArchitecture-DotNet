using System;

namespace Todo.Boundry.Todo.Update
{
    public class UpdateTodoItemOutput
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}