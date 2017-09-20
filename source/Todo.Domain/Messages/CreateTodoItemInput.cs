using System;

namespace Todo.Domain.Messages
{
    public class CreateTodoItemInput
    {
        public string ItemDescription { get; set; }
        public DateTime DueDate { get; set; }
    }
}