using System;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Domain.UseCase;

namespace Todo.UseCase
{
    public class DeleteTodoItemUseCase : IDeleteTodoItemUseCase
    {
        public void Execute(Guid inputTo, IRespondWithSuccessOrError<string, ErrorOutputMessage> presenter)
        {
            presenter.Respond(string.Empty);
        }
    }
}