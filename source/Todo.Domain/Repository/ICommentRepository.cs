using System;
using System.Collections.Generic;
using Todo.DomainEntities;

namespace Todo.Domain.Repository
{
    public interface ICommentRepository
    {
        TodoComment Create(TodoComment domainModel);
        void Save();
        bool Delete(TodoComment domainModel);
        List<TodoComment> FindForItem(Guid itemId);
    }
}