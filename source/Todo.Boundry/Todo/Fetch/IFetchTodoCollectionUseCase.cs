using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain;

namespace Todo.Boundary.Todo.Fetch
{
    public interface IFetchTodoCollectionUseCase : IAction<List<TodoItemTo>>
    {
       
    }
}