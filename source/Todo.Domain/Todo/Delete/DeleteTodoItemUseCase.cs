using System;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Delete;

namespace Todo.Domain.Todo.Delete
{
    public class DeleteTodoItemUseCase : IDeleteTodoItemUseCase
    {
        private readonly ITodoRepository _repository;

        public DeleteTodoItemUseCase(ITodoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Execute(DeleteTodoInput inputTo, IRespondWithResultFreeSuccessOrError<ErrorOutputMessage> presenter)
        {
            var itemId = inputTo.Id;
            var itemExisted = DeleteItemIfExist(itemId);

            if (!itemExisted)
            {
                RespondWithMissingIdError(itemId, presenter);
                return;
            }

            presenter.Respond();
        }

        private bool DeleteItemIfExist(Guid id)
        {
            var isDeleted = _repository.Delete(id);
            _repository.Save();
            return isDeleted;
        }

        private void RespondWithMissingIdError(Guid id, IRespondWithResultFreeSuccessOrError<ErrorOutputMessage> presenter)
        {
            var errorOutputMessage = new ErrorOutputMessage();
            errorOutputMessage.AddError($"Could not locate item with id [{id}]");
            presenter.Respond(errorOutputMessage);
        }
    }
}