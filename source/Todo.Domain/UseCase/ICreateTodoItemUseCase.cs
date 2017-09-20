using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Domain.UseCaseMessages;

namespace Todo.Domain.UseCase
{
    public interface ICreateTodoItemUseCase
    {
        void Execute(CreateTodoItemInput input, IRespondWithSuccessOrError<CreateTodoItemOuput, ErrorOutputMessage> presenter);
    }
}
