using System;

namespace Todo.Domain.Messages
{
    public class DeleteTodoItemInputMessage
    {
        public Guid Id { get; set; }
    }
}
