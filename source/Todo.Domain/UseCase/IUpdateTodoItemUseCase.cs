using TddBuddy.CleanArchitecture.Domain;
using Todo.Domain.Messages;

namespace Todo.Domain.UseCase
{
    public interface IUpdateTodoItemUseCase : IUseCase<UpdateTodoItemInput,string>
    {
    }
}