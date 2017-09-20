using System;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.AutoMapper;
using Todo.Domain.Repository;
using Todo.Domain.UseCase;
using Todo.Domain.UseCaseMessages;
using Todo.Entities;

namespace Todo.UseCase
{
    public class CreateTodoItemUseCase : ICreateTodoItemUseCase
    {
        private readonly IMapper _mapper;
        private readonly ITodoRepository _respository;

        public CreateTodoItemUseCase(ITodoRepository respository)
        {
            _respository = respository ?? throw new ArgumentNullException(nameof(respository));
            _mapper = new AutoMapperBuilder()
                .WithConfiguration(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CreateTodoItemInput, TodoItem>();
                }))
                .Build();
        }

        public void Execute(CreateTodoItemInput input, IRespondWithSuccessOrError<CreateTodoItemOuput, ErrorOutputMessage> presenter)
        {
            if (IsValidItemDescription(input))
            {
                RespondWithInvalidItemDescription(presenter);
                return;
            }

            var model = _mapper.Map<TodoItem>(input);
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
