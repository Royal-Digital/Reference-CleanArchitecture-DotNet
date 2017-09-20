using System;

namespace Todo.Domain.Messages
{
    public class UpdateTodoItemOutput
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}