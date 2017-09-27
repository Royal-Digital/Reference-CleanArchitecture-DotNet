using System;

namespace Todo.Domain.UseCaseMessages
{
    public class DeleteCommentOutput
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}