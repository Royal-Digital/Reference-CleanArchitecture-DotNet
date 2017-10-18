using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain;

namespace Todo.Boundary.Todo.Fetch.Filtered
{
    public interface IFetchFilteredTodoUseCase : IUseCase<TodoFilterInput, List<TodoTo>>
    {
    }
}