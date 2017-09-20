using System;
using System.Collections.Generic;
using Todo.Entities;

namespace Todo.Domain.Repository
{
    public interface ITodoRepository
    {
        TodoItem CreateItem(TodoItem item);
        void Update(TodoItem item);
        void Save();
        List<TodoItem> FetchAll();
        bool DeleteItem(Guid id);
    }
}