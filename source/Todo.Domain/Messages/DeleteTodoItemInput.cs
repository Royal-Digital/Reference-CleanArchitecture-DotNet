using System;

namespace Todo.Domain.Messages
{
    public class DeleteTodoItemInput
    {
        public Guid Id { get; set; }
    }
}
