using System.Collections.Generic;
using Todo.Entities;

namespace Todo.Domain.Repository
{
    public interface ITodoRepository
    {
        TodoItem CreateItem(TodoItem input);
        void Update(TodoItem todoItemModel);
        void Save();
        List<TodoItem> FetchAll();
    }
}