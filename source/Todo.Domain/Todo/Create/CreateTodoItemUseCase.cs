using System;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.AutoMapper;
using Todo.Boundry.Todo;
using Todo.Boundry.Todo.Create;

namespace Todo.Domain.Todo.Create
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

        public void Execute(CreateTodoItemInput inputTo, IRespondWithSuccessOrError<CreateTodoItemOuput, ErrorOutputMessage> presenter)
        {
            var domainEntity = MapInputToDomainEntity(inputTo);
            if (InvalidItemDescription(domainEntity))
            {
                RespondWithInvalidItemDescription(presenter);
                return;
            }

            var output = Persist(inputTo);

            RespondWithSuccess(presenter, output);
        }

        private bool InvalidItemDescription(TodoItem model)
        {
            return !model.ItemDescriptionIsValid();
        }

        private void RespondWithSuccess(IRespondWithSuccessOrError<CreateTodoItemOuput, ErrorOutputMessage> presenter, CreateTodoItemOuput output)
        {
            var outputMessage = new CreateTodoItemOuput {Id = output.Id};
            presenter.Respond(outputMessage);
        }

        private CreateTodoItemOuput Persist(CreateTodoItemInput model)
        {
            var id = _respository.Create(model);
            _respository.Save();
            return new CreateTodoItemOuput{Id = id};
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
