using System;

namespace Todo.Boundary.Todo.Update
{
    public class UpdateTodoItemInput
    {
        public Guid Id { get; set; }
        public string ItemDescription { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
