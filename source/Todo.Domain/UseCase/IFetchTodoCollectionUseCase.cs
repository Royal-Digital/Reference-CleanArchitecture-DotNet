using System.Collections.Generic;
using TddBuddy.CleanArchitecture.Domain;
using Todo.Domain.UseCaseMessages;

namespace Todo.Domain.UseCase
{
    public interface IFetchTodoCollectionUseCase : IAction<List<FetchTodoItemOutput>>
    {
       
    }
}