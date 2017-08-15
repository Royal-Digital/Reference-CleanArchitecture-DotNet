using TddBuddy.CleanArchitecture.Domain;
using Todo.Domain.Model;

namespace Todo.Domain.UseCase
{
    public interface IUpdateTodoItemUseCase : IUseCase<TodoItemModel,string>
    {
    }
}