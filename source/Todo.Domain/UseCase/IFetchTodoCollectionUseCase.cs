using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain;
using Todo.Entities;

namespace Todo.Domain.UseCase
{
    public interface IFetchTodoCollectionUseCase : IAction<List<TodoItem>>
    {
       
    }
}