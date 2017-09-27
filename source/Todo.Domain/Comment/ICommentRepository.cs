using System;
using System.Collections.Generic;

namespace Todo.Boundry.Comment
{
    // I should break these interfaces down too ;)
    public interface ICommentRepository
    {
        TodoComment Create(TodoComment domainModel);
        void Save();
        bool Delete(TodoComment domainModel);
        List<TodoComment> FindForItem(Guid itemId);
    }
}