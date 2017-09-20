using System;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Domain.Messages;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;

namespace Todo.UseCase
{
    public class CreateTodoItemUseCase : ICreateTodoItemUseCase
    {
        private readonly ITodoRepository _respository;

        public CreateTodoItemUseCase(ITodoRepository respository)
        {
            if (respository == null)
            {
                throw new ArgumentNullException();
            }
            _respository = respository;
        }

        public void Execute(CreateTodoItemInput input, IRespondWithSuccessOrError<CreateTodoItemOuput, ErrorOutputMessage> presenter)
        {
            if (string.IsNullOrWhiteSpace(input.ItemDescription))
            {
                RespondWithInvalidItemDescription(presenter);
                return;
            }

            var todoItemModel = _respository.CreateItem(input);
            _respository.Save();
            var outputMessage = new CreateTodoItemOuput{ Id = todoItemModel.Id.ToString()};
            presenter.Respond(outputMessage);
        }

        private void RespondWithInvalidItemDescription(IRespondWithSuccessOrError<CreateTodoItemOuput, ErrorOutputMessage> presenter)
        {
            var errors = new ErrorOutputMessage();
            errors.AddError("ItemDescription cannot be empty or null");
            presenter.Respond(errors);
        }
    }
}
