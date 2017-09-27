using System;
using System.Collections.Generic;

namespace Todo.Boundry.Todo
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