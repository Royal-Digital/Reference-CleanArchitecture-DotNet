using System;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Delete;

namespace Todo.Domain.Todo.Delete
{
    public class DeleteTodoItemUseCase : IDeleteTodoItemUseCase
    {
        private readonly ITodoRepository _repository;

        public DeleteTodoItemUseCase(ITodoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Execute(DeleteTodoItemInput inputTo, IRespondWithSuccessOrError<DeleteTodoItemOutput, ErrorOutputMessage> presenter)
        {
            var itemExisted = DeleteItemIfExist(inputTo);

            if (itemExisted)
            {
                RespondWithSuccess(inputTo, presenter);
                return;
            }

            RespondWithMissingIdError(inputTo, presenter);
        }

        private bool DeleteItemIfExist(DeleteTodoItemInput inputTo)
        {
            var isDeleted = _repository.Delete(inputTo.Id);
            return isDeleted;
        }

        private void RespondWithSuccess(DeleteTodoItemInput inputTo, IRespondWithSuccessOrError<DeleteTodoItemOutput, ErrorOutputMessage> presenter)
        {
            presenter.Respond(new DeleteTodoItemOutput {Id = inputTo.Id, Message = "Deleted item"});
        }

        private void RespondWithMissingIdError(DeleteTodoItemInput inputTo, IRespondWithSuccessOrError<DeleteTodoItemOutput, ErrorOutputMessage> presenter)
        {
            var errorOutputMessage = new ErrorOutputMessage();
            errorOutputMessage.AddError($"Could not locate item with id [{inputTo.Id}]");
            presenter.Respond(errorOutputMessage);
        }
    }
}