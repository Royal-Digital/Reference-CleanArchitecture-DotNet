using System;

namespace Todo.Domain.Messages
{
    public class CreateTodoItemInputMessage
    {
        public string ItemDescription { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}