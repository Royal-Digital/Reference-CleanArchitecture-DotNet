using System;

namespace Todo.Domain.UseCaseMessages
{
    public class CreateCommentOuput
    {
        public Guid TodoItemId { get; set; }
        public string Message { get; set; }
    }
}