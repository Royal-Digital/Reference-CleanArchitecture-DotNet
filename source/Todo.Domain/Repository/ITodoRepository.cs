using System.Collections.Generic;
using Todo.Domain.Model;

namespace Todo.Domain.Repository
{
    public interface ITodoRepository
    {
        TodoItemModel CreateItem(TodoItemModel input);
        void Update(TodoItemModel todoItemModel);
        void Save();
        List<TodoItemModel> FetchAll();
    }
}