using System;

namespace Todo.Domain.Messages
{
    public class DeleteTodoItemOutput
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
