using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain;
using Todo.Domain.Model;

namespace Todo.Domain.UseCase
{
    public interface IFetchTodoCollectionUseCase : IAction<List<TodoItemModel>>
    {
       
    }
}