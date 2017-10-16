using TddBuddy.CleanArchitecture.Domain;

namespace Todo.Boundary.Todo.Update
{
    public interface IUpdateTodoItemUseCase : IResultFreeAction<UpdateTodoItemInput>
    {
    }
}