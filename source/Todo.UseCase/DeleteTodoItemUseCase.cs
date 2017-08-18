using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Domain.Messages;
using Todo.Domain.UseCase;

namespace Todo.UseCase
{
    public class DeleteTodoItemUseCase : IDeleteTodoItemUseCase
    {
        public void Execute(DeleteTodoItemInputMessage inputTo, IRespondWithSuccessOrError<DeleteTodoItemOutputMessage, ErrorOutputMessage> presenter)
        {
            presenter.Respond(new DeleteTodoItemOutputMessage());
        }
    }
}