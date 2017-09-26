using System;
using System.Collections.Generic;
using Todo.DomainEntities;

namespace Todo.Domain.Repository
{
    public interface ITodoRepository
    {
        TodoItem Create(TodoItem item);
        void Update(TodoItem item);
        void Save();
        List<TodoItem> FetchAll();
        bool Delete(Guid id);
        TodoItem FindById(Guid id);
    }
}