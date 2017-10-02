using System;
using System.Collections.Generic;
using Todo.Boundry.Comment.Create;
using Todo.Boundry.Todo.Fetch;

namespace Todo.Boundry.Comment
{
    // I should break these interfaces down too ;)
    public interface ICommentRepository
    {
        Guid Create(CreateCommentInput message);
        void Save();
        bool Delete(Guid commentId);
        List<FetchTodoCommentOutput> FindForItem(Guid itemId);
    }
}