using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain;

namespace Todo.Boundry.Todo.Fetch
{
    public interface IFetchTodoCollectionUseCase : IAction<List<TodoItemTo>>
    {
       
    }
}