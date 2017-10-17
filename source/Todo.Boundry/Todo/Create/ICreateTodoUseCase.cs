using TddBuddy.CleanArchitecture.Domain;

namespace Todo.Boundary.Todo.Create
{
    public interface ICreateTodoUseCase : IUseCase<CreateTodoInput, CreateTodoOutput>
    {
    }
}
