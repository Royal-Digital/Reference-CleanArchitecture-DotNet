using System;
using System.Collections.Generic;
using Todo.Domain.Entities;

namespace Todo.Boundry.Repository
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