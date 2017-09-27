using System;

namespace Todo.Domain.UseCaseMessages
{
    public class FetchTodoCommentOutput
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
    }
}