using System;

namespace Todo.Domain.UseCaseMessages
{
    public class CreateCommentInput
    {
        public Guid TodoItemId { get; set; }
        public string Comment { get; set; }
    }
}