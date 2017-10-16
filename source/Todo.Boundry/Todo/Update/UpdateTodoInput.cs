using System;

namespace Todo.Boundary.Todo.Update
{
    public class UpdateTodoInput
    {
        public Guid Id { get; set; }
        public string ItemDescription { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
