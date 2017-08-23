using TddBuddy.CleanArchitecture.Domain;
using Todo.Domain.Messages;
using Todo.Domain.Model;

namespace Todo.Domain.UseCase
{
    public interface IUpdateTodoItemUseCase : IUseCase<UpdateTodoItemInputMessage,string>
    {
    }
}