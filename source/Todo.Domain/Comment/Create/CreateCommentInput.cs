using System;

namespace Todo.Boundry.Comment.Create
{
    public class CreateCommentInput
    {
        public Guid TodoItemId { get; set; }
        public string Comment { get; set; }
    }
}