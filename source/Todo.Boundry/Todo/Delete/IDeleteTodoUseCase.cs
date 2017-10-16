using TddBuddy.CleanArchitecture.Domain;

namespace Todo.Boundary.Todo.Delete
{
    public interface IDeleteTodoUseCase : IResultFreeAction<DeleteTodoInput>
    {
    }
}