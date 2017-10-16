using System;
using AutoMapper;
using TddBuddy.CleanArchitecture.Domain.Messages;
using TddBuddy.CleanArchitecture.Domain.Output;
using Todo.Boundary.Todo;
using Todo.Boundary.Todo.Create;

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

        public void Execute(CreateTodoInput inputTo, IRespondWithSuccessOrError<CreateTodoOuput, ErrorOutputMessage> presenter)
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

        private void RespondWithSuccess(IRespondWithSuccessOrError<CreateTodoOuput, ErrorOutputMessage> presenter, CreateTodoOuput output)
        {
            var outputMessage = new CreateTodoOuput {Id = output.Id};
            presenter.Respond(outputMessage);
        }

        private CreateTodoOuput Persist(CreateTodoInput model)
        {
            var id = _respository.Create(model);
            _respository.Save();
            return new CreateTodoOuput{Id = id};
        }

        private TodoItem MapInputToDomainEntity(CreateTodoInput input)
        {
            var model = _mapper.Map<TodoItem>(input);
            return model;
        }

        private IMapper CreateAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateTodoInput, TodoItem>();
            });

            return new Mapper(configuration);
        }

        private void RespondWithInvalidItemDescription(IRespondWithSuccessOrError<CreateTodoOuput, ErrorOutputMessage> presenter)
        {
            var errors = new ErrorOutputMessage();
            errors.AddError("ItemDescription cannot be empty or null");
            presenter.Respond(errors);
        }
    }
}
