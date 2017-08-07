using Todo.Domain.Messages;

namespace Todo.Domain.Repository
{
    public interface ITodoRepository
    {
        string CreateTodoItem(CreateTodoItemInputMessage inputMessage);
    }
}