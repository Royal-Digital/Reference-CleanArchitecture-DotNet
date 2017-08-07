using System.Collections.Generic;
using Todo.Domain.Messages;
using Todo.Domain.Model;

namespace Todo.Domain.Repository
{
    public interface ITodoRepository
    {
        TodoItemModel CreateItem(CreateTodoItemInputMessage inputMessage);
        void UpdateAudit(TodoItemModel todoItemModel);
        void Save();
        List<TodoItemModel> FetchAll();
    }
}