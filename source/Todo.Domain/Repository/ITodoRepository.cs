using Todo.Domain.Messages;

namespace Todo.Domain.Repository
{
    public interface ITodoRepository
    {
        void CreateItem(CreateTodoItemInputMessage inputMessage);
    }
}