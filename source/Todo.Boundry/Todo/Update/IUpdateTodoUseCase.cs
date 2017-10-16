using TddBuddy.CleanArchitecture.Domain;

namespace Todo.Boundary.Todo.Update
{
    public interface IUpdateTodoUseCase : IResultFreeAction<UpdateTodoInput>
    {
    }
}