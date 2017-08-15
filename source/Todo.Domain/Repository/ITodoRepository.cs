using System.Collections.Generic;
using Todo.Domain.Messages;
using Todo.Domain.Model;

namespace Todo.Domain.Repository
{
    public interface ITodoRepository
    {
        TodoItemModel CreateItem(CreateTodoItemInputMessage inputMessage);
        void Update(TodoItemModel todoItemModel);
        void Save();
        List<TodoItemModel> FetchAll();
    }
}