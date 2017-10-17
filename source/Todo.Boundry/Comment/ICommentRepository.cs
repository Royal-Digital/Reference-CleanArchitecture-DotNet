using System;
using System.Collections.Generic;
using Todo.Boundary.Comment.Create;
using Todo.Boundary.Todo.Fetch;

namespace Todo.Boundary.Comment
{
    public interface ICommentRepository
    {
        Guid Create(CreateCommentInput message);
        void Save();
        bool Delete(Guid commentId);
        List<TodoCommentTo> FindForItem(Guid itemId);
    }
}