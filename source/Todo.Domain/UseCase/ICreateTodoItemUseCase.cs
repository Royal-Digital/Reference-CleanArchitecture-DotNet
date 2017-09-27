using TddBuddy.CleanArchitecture.Domain;
using Todo.Domain.UseCaseMessages;

namespace Todo.Boundry.UseCase
{
    public interface ICreateTodoItemUseCase : IUseCase<CreateTodoItemInput, CreateTodoItemOuput>
    {
    }
}
