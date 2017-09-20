using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;

namespace Todo.UseCase
{
    public class DeleteTodoItemUseCase : IDeleteTodoItemUseCase
    {
        public void Execute(DeleteTodoItemInput inputTo, IRespondWithSuccessOrError<DeleteTodoItemOutput, ErrorOutputMessage> presenter)
        {
            presenter.Respond(new DeleteTodoItemOutput());
        }
    }
}