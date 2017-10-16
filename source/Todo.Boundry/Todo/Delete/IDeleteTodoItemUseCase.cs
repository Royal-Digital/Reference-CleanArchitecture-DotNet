using TddBuddy.CleanArchitecture.Domain;

namespace Todo.Boundary.Todo.Delete
{
    public interface IDeleteTodoItemUseCase : IResultFreeAction<DeleteTodoInput>
    {
    }
}