using TddBuddy.CleanArchitecture.Domain;
using Todo.Domain.UseCaseMessages;

namespace Todo.Boundry.UseCase
{
    public interface IDeleteTodoItemUseCase : IUseCase<DeleteTodoItemInput, DeleteTodoItemOutput>
    {
    }
}