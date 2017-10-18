using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain;
using Todo.Boundary.Todo.Fetch;

namespace Todo.Boundary
{
    public interface IFetchFilteredTodoUseCase : IUseCase<TodoFilterInput, List<TodoTo>>
    {
    }
}