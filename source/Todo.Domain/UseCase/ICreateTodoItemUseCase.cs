using TddBuddy.CleanArchitecture.Domain;
using Todo.Domain.UseCaseMessages;

namespace Todo.Domain.UseCase
{
    public interface ICreateTodoItemUseCase : IUseCase<CreateTodoItemInput, CreateTodoItemOuput>
    {
    }
}
