using System;

namespace Todo.Domain.Messages
{
    public class DeleteTodoItemOutputMessage
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
