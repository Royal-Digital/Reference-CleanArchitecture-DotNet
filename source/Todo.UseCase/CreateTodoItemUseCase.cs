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
            _mapper = CreateAutoMapper();
        }

        public void Execute(CreateTodoItemInput input, IRespondWithSuccessOrError<CreateTodoItemOuput, ErrorOutputMessage> presenter)
        {
            var domainEntity = MapInputToDomainEntity(input);
            if (InvalidItemDescription(domainEntity))
            {
                RespondWithInvalidItemDescription(presenter);
                return;
            }

            var persistedEntity = PersistDomainEntity(domainEntity);

            RespondWithSuccess(presenter, persistedEntity);
        }

        private bool InvalidItemDescription(TodoItem model)
        {
            return !model.ItemDescriptionIsValid();
        }

        private void RespondWithSuccess(IRespondWithSuccessOrError<CreateTodoItemOuput, ErrorOutputMessage> presenter, TodoItem todoItem)
        {
            var outputMessage = new CreateTodoItemOuput {Id = todoItem.Id};
            presenter.Respond(outputMessage);
        }

        private TodoItem PersistDomainEntity(TodoItem model)
        {
            var todoItem = _respository.Create(model);
            _respository.Save();
            return todoItem;
        }

        private TodoItem MapInputToDomainEntity(CreateTodoItemInput input)
        {
            var model = _mapper.Map<TodoItem>(input);
            return model;
        }

        private IMapper CreateAutoMapper()
        {
            return new AutoMapperBuilder()
                .WithConfiguration(new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CreateTodoItemInput, TodoItem>();
                }))
                .Build();
        }

        private void RespondWithInvalidItemDescription(IRespondWithSuccessOrError<CreateTodoItemOuput, ErrorOutputMessage> presenter)
        {
            var errors = new ErrorOutputMessage();
            errors.AddError("ItemDescription cannot be empty or null");
            presenter.Respond(errors);
        }
    }
}
