using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Domain.Messages;

namespace Todo.Domain.UseCase
{
    public interface ICreateTodoItemUseCase
    {
        void Execute(CreateTodoItemInput input, IRespondWithSuccessOrError<CreateTodoItemOuput, ErrorOutputMessage> presenter);
    }
}
