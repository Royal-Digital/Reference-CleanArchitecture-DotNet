using System;

namespace Todo.Domain.UseCaseMessages
{
    public class UpdateTodoItemOutput
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}