using TddBuddy.CleanArchitecture.Domain;

namespace Todo.Boundary.Todo.Create
{
    public interface ICreateTodoItemUseCase : IUseCase<CreateTodoInput, CreateTodoOuput>
    {
    }
}
