using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;

namespace Todo.UseCase
{
    public class DeleteCommentUseCase : IDeleteCommentUseCase
    {
        public void Execute(DeleteCommentInput inputTo, IRespondWithSuccessOrError<DeleteCommentOutput, ErrorOutputMessage> presenter)
        {
            throw new System.NotImplementedException();
        }
    }
}