using System;
using TddBuddy.CleanArchitecture.Domain;

namespace Todo.Domain.UseCase
{
    public interface IDeleteTodoItemUseCase : IUseCase<Guid,string>
    {
    }
}