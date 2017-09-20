using System;

namespace Todo.Domain.UseCaseMessages
{
    public class DeleteTodoItemOutput
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
