using System;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Domain.Messages;
using Todo.Domain.Model;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;

namespace Todo.UseCase
{
    public class UpdateTodoItemUseCase : IUpdateTodoItemUseCase
    {
        private readonly ITodoRepository _todoRepository;

        public UpdateTodoItemUseCase(ITodoRepository todoRepository)
        {
            if (todoRepository == null)
            {
                throw new ArgumentNullException();
            }

            _todoRepository = todoRepository;
        }

        public void Execute(UpdateTodoItemInputMessage inputTo, IRespondWithSuccessOrError<string, ErrorOutputMessage> presenter)
        {
            var model = CreateTodoItemModel(inputTo);

            if (InvalidId(model))
            {
                RespondWithError("Id cannot be empty", presenter);
                return;
            }

            if (!model.IsItemDescriptionValid())
            {
                RespondWithError("ItemDescription cannot be null or empty", presenter);
            }

            presenter.Respond("updated");
        }

        private static TodoItemModel CreateTodoItemModel(UpdateTodoItemInputMessage inputTo)
        {
            var model = new TodoItemModel
            {
                Id = inputTo.Id,
                IsCompleted = inputTo.IsCompleted,
                DueDate = inputTo.DueDate,
                ItemDescription = inputTo.ItemDescription
            };
            return model;
        }

        private bool InvalidId(TodoItemModel inputTo)
        {
            return !inputTo.IsIdValid();
        }

        private void RespondWithError(string message, IRespondWithSuccessOrError<string, ErrorOutputMessage> presenter)
        {
            var errorOutputMessage = new ErrorOutputMessage();
            errorOutputMessage.AddError(message);
            presenter.Respond(errorOutputMessage);
        }
    }
}