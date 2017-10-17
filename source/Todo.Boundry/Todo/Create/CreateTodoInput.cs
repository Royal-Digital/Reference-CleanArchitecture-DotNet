using System;

namespace Todo.Boundary.Todo.Create
{
    public class CreateTodoInput
    {
        public string ItemDescription { get; set; }
        public DateTime? DueDate { get; set; }
    }
}