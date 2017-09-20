using System;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Data.AutoMapper;
using Todo.Domain.Messages;
using Todo.Domain.Model;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;

namespace Todo.UseCase
{
    public class CreateTodoItemUseCase : ICreateTodoItemUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _respository;

        public CreateTodoItemUseCase(ITodoRepository respository)
        {
            _respository = respository ?? throw new ArgumentNullException(nameof(respository));
            _mapper = new AutoMapperBuilder().Build();
        }

        public void Execute(CreateTodoItemInput input, IRespondWithSuccessOrError<CreateTodoItemOuput, ErrorOutputMessage> presenter)
        {
            if (IsValidItemDescription(input))
            {
                RespondWithInvalidItemDescription(presenter);
                return;
            }

            var model = _mapper.Map<TodoItemModel>(input);
            var todoItemModel = _respository.CreateItem(model);
            _respository.Save();
            var outputMessage = new CreateTodoItemOuput{ Id = todoItemModel.Id};
            presenter.Respond(outputMessage);
        }

        private bool IsValidItemDescription(CreateTodoItemInput input)
        {
            return string.IsNullOrWhiteSpace(input.ItemDescription);
        }

        private void RespondWithInvalidItemDescription(IRespondWithSuccessOrError<CreateTodoItemOuput, ErrorOutputMessage> presenter)
        {
            var errors = new ErrorOutputMessage();
            errors.AddError("ItemDescription cannot be empty or null");
            presenter.Respond(errors);
        }
    }
}
