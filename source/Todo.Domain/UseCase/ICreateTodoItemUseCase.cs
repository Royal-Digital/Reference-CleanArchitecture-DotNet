using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Domain.Messages;

namespace Todo.Domain.UseCase
{
    public interface ICreateTodoItemUseCase
    {
        void Execute(CreateTodoItemInputMessage inputMessage, IRespondWithSuccessOrError<CreateTodoItemOuputMessage, ErrorOutputMessage> presenter);
    }
}
